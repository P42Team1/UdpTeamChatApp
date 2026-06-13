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

namespace UdpTeamChatAppServer
{
    public class AuthHandler
    {
        private UdpClient _udpServer;
        private Service _service;
        public AuthHandler(UdpClient udpServer, Service service)
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
        private async Task Send<T>(IPEndPoint to, T data)
        {
            var packet = Packet.Create(PacketType.AuthResponse, data);
            var bytes = packet.ToBytes();
            await _udpServer.SendAsync(bytes, bytes.Length, to);
        }
    }
}
