using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace ChatLibrary.Models
{
    // Get Chats ------

    public class GetChatsPayload
    {
        public int UserId { get; set; }
    }

    public class ChatsResponsePayload
    {
        public List<ChatInfo> Chats { get; set; } = new();
    }

    public class ChatInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateChatPayload
    {
        public string Name { get; set; }
        public int CreatorId { get; set; }
    }

    public class CreateChatResponsePayload
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ChatId { get; set; }

    }

    // ---------------

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
    public class ConnectPayload
    {
        public int UserId { get; set; }
    }
    public class SendPrivateMessagePayload
    {
        public int SenderId { get; set; }
        public int RecipientUserId { get; set; }
        public string Text { get; set; }
    }
    public class SendGroupMessagePayload
    {
        public int SenderId { get; set; }
        public int ChatId { get; set; }
        public string Text { get; set; }
        

    }
    public class IncomingMessagePayload
    {
        public int SenderId { get; set; }
        public int ChatId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
    public class IncomingPrivateMessagePayload
    {
        public int SenderId { get; set; }
        public int RecepientId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
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
        AuthResponse,

        Connect,
        Disconnect,
        UserOnline,
        UserOffline,

        SendPrivateMessage,
        SendGroupMessage,
        IncomingMessage,
        IncomingPrivateMessage,

        Error,

        GetChats,
        ChatsResponse,

        CreateChat,
        CreateChatResponse,
    }



}
