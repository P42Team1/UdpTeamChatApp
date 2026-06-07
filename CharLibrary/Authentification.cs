using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CharLibrary
{
    public class RegisterPayload
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class LoginPayload
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Packet
    {
        public PacketType Type { get; set; }
        public int UserId { get; set; }
        public string Payload { get; set; }

        public byte[] ToBytes()
        {
            var json = JsonSerializer.Serialize(this);
            return Encoding.UTF8.GetBytes(json);
        }

        public static Packet? FromBytes(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<Packet>(json);
        }
        public static Packet Create<T>(PacketType type, T payload)
        {
            return new Packet
            {
                Type = type,
                Payload = JsonSerializer.Serialize(payload)
            };
        }
        public T? GetPayload<T>()
        {
            return JsonSerializer.Deserialize<T>(Payload);
        }
    }
    public class AuthResponsePayload
    {
        public bool Success { get; set; }
        public string Message { get; set; } 
        public int UserId { get; set; } 
        public string Username { get; set; }
    }
    public enum PacketType
    {
        Register,
        Login,
        Logout,
        SendMessage,
        AuthResponse
    }
}
