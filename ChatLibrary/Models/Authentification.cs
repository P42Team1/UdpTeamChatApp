using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatLibrary.Models
{
    public class RegisterPayload
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class DataEncryptor
    {
        public static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            var newHash = HashPassword(password);
            return newHash == hash;
        }
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
