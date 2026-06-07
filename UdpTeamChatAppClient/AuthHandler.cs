using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CharLibrary;

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
            //--------------------Checking if user is already in use--------------------
            //bool exists = await _db.Users
            //    .AnyAsync(u => u.Username == payload.Username || u.Email == payload.Email);
            //if(exists) {
            //    var err = new AuthResponsePayload
            //    {
            //        Success = false,
            //        Message = "Username or email already in use"
            //    };
            //    await Send(from, err);
            //    return;

            //}
            var user = new User(payload.Username, payload.Password, payload.Email);
            //await _db.Users.AddAsync(user);

            var response = new AuthResponsePayload
            {
                Success = true,
                Message = "Registration successful",
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
