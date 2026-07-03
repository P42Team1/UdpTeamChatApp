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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
        public async Task HandleLogin(Packet packet, IPEndPoint client)
        {
            var payload = packet.GetPayload<LoginPayload>();
            var user = new UserLoginData(payload.Username, payload.Password);

            var loginData = await _service.GetUserLoginAsync(payload.Username, payload.Password);

            if (loginData == null)
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
            }
            else
            {
                sender.IPAddress = clientEP.Address.ToString();
                sender.Port = clientEP.Port;
                sender.Status = UserStatus.Online;
            }

            await _service.SetUserOnline(sender);
            Console.WriteLine($"=== User connected: Id={payload.UserId}, Port={clientEP.Port} ===");

            var history = await _service.GetChatHistoryAsync(payload.UserId);
            var response = new ChatHistoryResponsePayload
            {
                Messages = history.Select(m => new HistoryMessage
                {
                    SenderId = m.AuthorId,
                    ChatId = m.ChatId,
                    ChatName = m.Chat?.Name ?? "",
                    IsGroup = m.Chat?.IsGroup ?? false,
                    OtherUserId = (m.Chat != null && !m.Chat.IsGroup)
                        ? m.Chat.Members.FirstOrDefault(mem => mem.Id != payload.UserId)?.Id ?? 0
                        : 0,
                    Text = m.Text,
                    Time = m.Time
                }).ToList()
            };
            await SendPacket(clientEP, PacketType.ChatHistoryResponse, response);
        }
        public async Task HandleDisconnect(Packet packet)
        {
            var payload = packet.GetPayload<ConnectPayload>();
            var userToRemove = _onlineUsers.FirstOrDefault(user => user.Id == payload.UserId);
            if (userToRemove is not null)
            {
                _onlineUsers.Remove(userToRemove);
                await _service.SetUserOffline(userToRemove);
                Console.WriteLine($"=== User {payload.UserId} disconnected and removed from list ===");
            }
            Console.WriteLine($"Users online: {_onlineUsers.Count}");
        }
        public async Task HandleGroupMessage(Packet packet)
        {
            SendGroupMessagePayload payload = packet.GetPayload<SendGroupMessagePayload>();
            var message = new Message
            {
                AuthorId = payload.SenderId,
                ChatId = payload.ChatId,
                Text = payload.Text,
                Time = DateTime.Now,
                Status = MessageStatus.NotReceived
            };
            await _service.AddObjects(message);

            Packet pushPacket = Packet.Create(PacketType.IncomingMessage, new IncomingMessagePayload
            {
                SenderId = payload.SenderId,
                ChatId = payload.ChatId,
                Text = payload.Text,
                Time = DateTime.Now
            });
            byte[] bytes = pushPacket.ToBytes();
            foreach (var user in _onlineUsers)
            {
                if (user.Id != payload.SenderId)
                {
                    try
                    {
                        IPAddress targetIP = IPAddress.Parse(user.IPAddress);
                        int targetPort = user.Port;
                        IPEndPoint targetEP = new IPEndPoint(targetIP, targetPort);
                        await _udpServer.SendAsync(bytes, bytes.Length, targetEP);
                        Console.WriteLine($"Forwarded to User {user.Id} on port {targetPort}");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }

                }
            }
            Console.WriteLine($"[GENERAL CHAT_{payload.ChatId}] from User {payload.SenderId}");
            Console.WriteLine($"Sent to {_onlineUsers.Count - 1} users");
        }
        public async Task HandlePrivateMessage(Packet packet)
        {
            SendPrivateMessagePayload payload = packet.GetPayload<SendPrivateMessagePayload>();
            var recUser = await _service.GetUserIdByUsername(payload.RecipientUserName);
            if (recUser == null)
            {
                Console.WriteLine($"Recipient username '{payload.RecipientUserName}' not found.");
                return;
            }

            if (await _service.IsUserBlacklistedAsync(recUser.Id, payload.SenderId))
            {
                Console.WriteLine($"[PRIVATE BLOCKED] User {recUser.Id} has blacklisted User {payload.SenderId}");
                return;
            }

            var targetUser = _onlineUsers.FirstOrDefault(user => user.Id == recUser.Id);
            Chat chat = await _service.GetOrCreatePrivateChatAsync(payload.SenderId, recUser.Id);

            if (targetUser != null)
            {
                var message = new Message
                {
                    AuthorId = payload.SenderId,
                    ChatId = chat.Id,
                    Text = payload.Text,
                    Time = DateTime.Now,
                    Status = MessageStatus.NotReceived
                };
                await _service.AddObjects(message);
                Packet pushPacket = Packet.Create(PacketType.IncomingPrivateMessage, new IncomingPrivateMessagePayload
                {
                    SenderId = payload.SenderId,
                    RecepientId = recUser.Id,
                    Text = payload.Text,
                    Time = DateTime.Now,
                });
                byte[] bytes = pushPacket.ToBytes();
                IPEndPoint targetUserEP = new IPEndPoint(IPAddress.Parse(targetUser.IPAddress), targetUser.Port);
                await _udpServer.SendAsync(bytes, bytes.Length, targetUserEP);
                Console.WriteLine($"Forwarded to User {targetUser.Id} on port {targetUser.Port}");
            }
        }
        private async Task Send<T>(IPEndPoint to, T data)
        {
            var packet = Packet.Create(PacketType.AuthResponse, data);
            var bytes = packet.ToBytes();
            await _udpServer.SendAsync(bytes, bytes.Length, to);
        }
        private async Task SendPacket<T>(IPEndPoint to, PacketType type, T data)
        {
            var packet = Packet.Create(type, data);
            var bytes = packet.ToBytes();
            await _udpServer.SendAsync(bytes, bytes.Length, to);
        }

        public async Task HandleGetChats(IPEndPoint client, int userId)
        {
            var chats = await _service.GetAllChatsOfUser(new User { Id = userId});
            var response = new ChatsResponsePayload
            {
                Chats = chats.Select(c => new ChatInfo { Id = c.Id, Name = c.Name, IsGroup = c.IsGroup }).ToList()
            };
            var packet = Packet.Create(PacketType.ChatsResponse, response);
            var bytes = packet.ToBytes();
            await _udpServer.SendAsync(bytes, bytes.Length, client);
        }

        public async Task HandleCreateChat(Packet packet, IPEndPoint clientEP)
        {
            var payload = packet.GetPayload<CreateChatPayload>();
            var memberIds = new List<int> { payload.CreatorId };

            foreach (var username in payload.MemberUsernames)
            {
                var user = await _service.GetUserIdByUsername(username);
                if (user != null) memberIds.Add(user.Id);
            }

            var chat = await _service.CreateChatAsync(payload.Name, memberIds.Distinct().ToList());
            var response = new CreateChatResponsePayload
            {
                Success = true,
                Message = "Chat created",
                ChatId = chat.Id,
            };
            var responsePacket = Packet.Create(PacketType.CreateChatResponse, response);
            var bytes = responsePacket.ToBytes();
            await _udpServer.SendAsync(bytes, bytes.Length, clientEP);

            foreach (var user in _onlineUsers)
            {
                if (user.Port != clientEP.Port)
                {
                    IPEndPoint userEP = new IPEndPoint(IPAddress.Parse(user.IPAddress), user.Port);
                    await _udpServer.SendAsync(bytes, bytes.Length, userEP);
                }
            }
        }
        public async Task HandleGetContacts(Packet packet, IPEndPoint clientEP)
        {
            var payload = packet.GetPayload<GetContactsPayload>();
            var contacts = await _service.GetContactsAsync(payload.UserId);
            var response = new ContactsResponsePayload
            {
                Contacts = contacts.Select(c => new ContactInfo
                {
                    UserId = c.ContactUserId,
                    Username = c.ContactUser?.LoginData?.Username ?? $"User {c.ContactUserId}",
                    IsBlacklisted = c.IsBlacklisted
                }).ToList()
            };

            await SendPacket(clientEP, PacketType.ContactsResponse, response);
        }

        public async Task HandleContactAction(Packet packet, IPEndPoint clientEP)
        {
            var payload = packet.GetPayload<ContactActionPayload>();
            var contactUser = await _service.GetUserIdByUsername(payload.ContactUsername);
            if (contactUser == null)
            {
                await SendPacket(clientEP, PacketType.ContactActionResponse, new ContactActionResponsePayload
                {
                    Success = false,
                    Message = "User not found"
                });
                return;
            }

            (bool Success, string Message) result = packet.Type switch
            {
                PacketType.AddContact => await _service.AddContactAsync(payload.OwnerId, contactUser.Id),
                PacketType.RemoveContact => await _service.RemoveContactAsync(payload.OwnerId, contactUser.Id),
                PacketType.BlockContact => await _service.SetContactBlacklistAsync(payload.OwnerId, contactUser.Id, true),
                PacketType.UnblockContact => await _service.SetContactBlacklistAsync(payload.OwnerId, contactUser.Id, false),
                _ => (false, "Unsupported contact action")
            };

            await SendPacket(clientEP, PacketType.ContactActionResponse, new ContactActionResponsePayload
            {
                Success = result.Success,
                Message = result.Message
            });
        }

    }
}
