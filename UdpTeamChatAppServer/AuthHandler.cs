using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary;
using ChatLibrary.Models;
using ChatLibrary.Data;
using System.Net.Http.Headers;

namespace UdpTeamChatAppServer
{
    public class Handler
    {
        private UdpClient _udpServer;
        private Service _service;
        private List<User> _onlineUsers = new List<User>();
        public Handler(UdpClient udpServer, Service service, List<User> onlineUsers)
        {
            _udpServer = udpServer ?? throw new ArgumentNullException(nameof(udpServer));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _onlineUsers = onlineUsers ?? throw new ArgumentNullException(nameof(onlineUsers));
        }
        public Handler(UdpClient udpServer, Service service)
        {
            _udpServer = udpServer ?? throw new ArgumentNullException(nameof(udpServer));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public async Task HandleRegister(Packet packet, IPEndPoint client)
        {
            var payload = packet.GetPayload<RegisterPayload>();
            var user = new UserLoginData(payload.Username, payload.Password, payload.Email);
            try
            {
                bool success = await _service.RegisterUserAsync(user);
                if (!success)
                {
                    await Send(client, new AuthResponsePayload
                    {
                        Success = false,
                        Message = "Username already exists"
                    });
                    return;
                }
                var response = new AuthResponsePayload
                {
                    Success = true,
                    Message = "Registration successful",
                    UserId = user.Id,
                    Username = user.Username
                };
                await Send(client, response);

                Console.WriteLine($"Registered new user: {user.Username} with email: {user.Email}. Hashed password: {user.Password}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
        public async Task HandleLogin(Packet packet, IPEndPoint client)
        {
            var payload = packet.GetPayload<LoginPayload>();
            var user = new UserLoginData(payload.Username, payload.Password);

            var loginData = await _service.UserLoginAsync(payload.Username, payload.Password);

            if(loginData == null)
            {
                await Send(client, new AuthResponsePayload
                {
                    Success = false,
                    Message = "Account doesnt exist"
                });
                return;
            }
            Console.WriteLine($"User {loginData.Username} logged in successfully.");
            var response = new AuthResponsePayload
            {
                Success = true,
                Message = "Log In successful",
                UserId = loginData.Id,
                Username = loginData.Username
            };
            await Send(client, response);

        }

        public async Task HandleConnect(Packet packet, IPEndPoint clientEP)
        {
            ConnectPayload payload = packet.GetPayload<ConnectPayload>();
            var sender = _onlineUsers.FirstOrDefault(user => user.Id == payload.UserId);
            if (sender is null)
            {
                sender = new User
                {
                    Id = payload.UserId,
                    IPAddress = clientEP.Address.ToString(),
                    Port = clientEP.Port,
                    Status = UserStatus.Online,
                };
                _onlineUsers.Add(sender);
                Console.WriteLine($"===New user {sender.Id} added===");
            }
            else
            {
                sender.IPAddress = clientEP.Address.ToString();
                sender.Port = clientEP.Port;
            }       
        }
        public async Task HandleDisconnect(Packet packet)
        {
            var userToRemove = _onlineUsers.FirstOrDefault(user => user.Id == packet.UserId);
            if (userToRemove is not null)
            {
                _onlineUsers.Remove(userToRemove);
                Console.WriteLine($"=== User {packet.UserId} disconnected and removed from list ===");
            }
            Console.WriteLine($"Users online: {_onlineUsers.Count}");
        }
        public async Task HandleGroupMessage(Packet packet)
        {
            SendGroupMessagePayload payload = packet.GetPayload<SendGroupMessagePayload>();
            var message = new Message
            {
                Id = 0,
                AuthorId = packet.UserId,
                ChatId = payload.ChatId,
                Text = payload.Text,
                Time = DateTime.Now,
                Status = MessageStatus.NotReceived
            };

            Packet pushPacket = Packet.Create(PacketType.IncomingMessage, new IncomingMessagePayload
            {
                SenderId = packet.UserId,
                ChatId = payload.ChatId,
                Text = payload.Text,
                Time = DateTime.Now
            });
            byte[] bytes = pushPacket.ToBytes();
            foreach (var user in _onlineUsers)
            {
                if (user.Id != packet.UserId)
                {
                    try
                    {
                        IPAddress targetIP = IPAddress.Parse(user.IPAddress);
                        int targetPort = user.Port;
                        IPEndPoint targetEP = new IPEndPoint(targetIP, targetPort);
                        _udpServer.SendAsync(bytes, bytes.Length, targetEP);
                        Console.WriteLine($"Forwarded to User {user.Id} on port {targetPort}");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }

                }
            }
            Console.WriteLine($"[GENERAL CHAT_{payload.ChatId}] from User {packet.UserId}");
            Console.WriteLine($"Sent to {_onlineUsers.Count - 1} users");
        }
        public async Task HandlePrivateMessage(Packet packet)
        {
            SendPrivateMessagePayload payload = packet.GetPayload<SendPrivateMessagePayload>();
            var targetUser = _onlineUsers.FirstOrDefault(user => user.Id == payload.RecipientUserId);

            Console.WriteLine($"[PRIVATE] From User {packet.UserId} to User {payload.RecipientUserId}");

            if (targetUser != null)
            {
                var message = new Message
                {
                    Id = 0,
                    AuthorId = packet.UserId,
                    ChatId = payload.RecipientUserId,
                    Text = payload.Text,
                    Time = DateTime.Now,
                    Status = MessageStatus.NotReceived
                };
                Packet pushPacket = Packet.Create(PacketType.IncomingMessage, new IncomingMessagePayload
                {
                    SenderId = packet.UserId,
                    ChatId = payload.RecipientUserId,
                    Text = payload.Text,
                    Time = DateTime.Now
                });
                byte[] bytes = pushPacket.ToBytes();
                IPAddress targetUserIp = IPAddress.Parse(targetUser.IPAddress);
                int targetUserPort = targetUser.Port;
                IPEndPoint targetUserEP = new IPEndPoint(targetUserIp, targetUserPort);
                await _udpServer.SendAsync(bytes, bytes.Length, targetUserEP);
                Console.WriteLine($"Forwarded to User {targetUser.Id} on port {targetUserPort}");
            }
        }
        private async Task Send<T>(IPEndPoint to, T data)
        {
            var packet = Packet.Create(PacketType.AuthResponse, data);
            var bytes = packet.ToBytes();
            await _udpServer.SendAsync(bytes, bytes.Length, to);
        }

        public async Task HandleGetChats(IPEndPoint client)
        {
            var chats = await _service.GetAllChatsAsync();
            var response = new ChatsResponsePayload
            {
                Chats = chats.Select(c => new ChatInfo { Id = c.Id, Name = c.Name }).ToList()
            };
            var packet = Packet.Create(PacketType.ChatsResponse, response);
            var bytes = packet.ToBytes();
            await _udpServer.SendAsync(bytes, bytes.Length, client);
        }

        public async Task HandleCreateChat(Packet packet, IPEndPoint clientEP)
        {
            var payload = packet.GetPayload<CreateChatPayload>();
            var chat = await _service.CreateChatAsync(payload.Name);
            var response = new CreateChatResponsePayload
            {
                Success = true,
                Message = "Chat created",
                ChatId = chat.Id,
            };
            var responsePacket = Packet.Create(PacketType.CreateChatResponse, response);
            var bytes = responsePacket.ToBytes();
            await _udpServer.SendAsync(bytes, bytes.Length, clientEP);

            foreach(var user in _onlineUsers)
            {
                if (user.Port != clientEP.Port)
                {
                    IPEndPoint userEP = new IPEndPoint(IPAddress.Parse(user.IPAddress), user.Port);
                    await _udpServer.SendAsync(bytes, bytes.Length, userEP);
                }
            }
        }
    }
}
