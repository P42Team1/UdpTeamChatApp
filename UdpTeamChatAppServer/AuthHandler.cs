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

namespace UdpTeamChatAppServer
{
    public class AuthHandler
    {
        private UdpClient _udpServer;
        public AuthHandler(UdpClient udpServer)
        {
            _udpServer = udpServer ?? throw new ArgumentNullException(nameof(udpServer));
        }

        public async Task HandleRegister(Packet packet, IPEndPoint client)
        {
            var payload = packet.GetPayload<RegisterPayload>();
            string hashedPassword = DataEncryptor.HashPassword(payload.Password);
            var user = new UserLoginData(payload.Username, hashedPassword, payload.Email);
            Console.WriteLine($"Registered new user: {user.Username} with email: {user.Email}. Hashed password: {hashedPassword}");
            var response = new AuthResponsePayload
            {
                Success = true,
                Message = "Registration successful",
                UserId = user.Id,
                Username = user.Username
            };
            await Send(client, response);

        }
        public async Task HandleLogin(Packet packet, IPEndPoint client)
        {
            var payload = packet.GetPayload<LoginPayload>();
            var user = new UserLoginData(payload.Username, payload.Password);

            var response = new AuthResponsePayload
            {
                Success = true,
                Message = "Log In successful",
                UserId = user.Id,
                Username = user.Username
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
