using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary;
using ChatLibrary.Models;
using UdpTeamChatAppServer;

List<User> onlineUsers = new List<User>(); // тимчасова заміна БД поки не підключимо її

int port = 10000;
UdpClient udpServer = new UdpClient(port);
AuthHandler authHandler = new AuthHandler(udpServer);

int tempClientPort = 10020;


try
{
    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
    while (true)
    {
        try
        {
            byte[] buff = udpServer.Receive(ref remoteEP);
            string receivedJson = Encoding.UTF8.GetString(buff);
            Message incomingMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(receivedJson);
            Console.WriteLine($"[RECEIVED] User {incomingMsg.AuthorId}: {incomingMsg.Text} (Chat {incomingMsg.ChatId})");

            if (incomingMsg.Text == "/connect")
            {
                var sender = onlineUsers.FirstOrDefault(user => user.Id == incomingMsg.AuthorId);
                if (sender is null)
                {
                    sender = new User
                    {
                        Id = incomingMsg.AuthorId,
                        IPAddress = remoteEP.Address.ToString(),
                        Port = remoteEP.Port,
                        Status = UserStatus.Online,
                    };
                    onlineUsers.Add(sender);
                    Console.WriteLine("===New user added===");
                }
                else
                {
                    sender.IPAddress = remoteEP.Address.ToString();
                    sender.Port = remoteEP.Port;
                }
                continue;
            }

            if (incomingMsg.Text == "/disconnect")
            {
                var userToRemove = onlineUsers.FirstOrDefault(user=>user.Id == incomingMsg.AuthorId);
                if (userToRemove is not null)
                {
                    onlineUsers.Remove(userToRemove);
                    Console.WriteLine($"=== User {incomingMsg.AuthorId} disconnected and removed from list ===");
                }
                Console.WriteLine($"Users online: {onlineUsers.Count}");
                continue;
            }

            if (incomingMsg.ChatId >= 1 && incomingMsg.ChatId <= 100) // поки що максимум 100 групових чатів, як зробити їх нескінченну кількість ще не придумав
            {
                Console.WriteLine($"[GENERAL CHAT_{incomingMsg.ChatId}] from User {incomingMsg.AuthorId}");
                foreach (var user in onlineUsers)
                {
                    if (user.Id != incomingMsg.AuthorId)
                    {
                        try
                        {
                            IPAddress targetIP = IPAddress.Parse(user.IPAddress);
                            int targetPort = user.Port;
                            IPEndPoint targetEP = new IPEndPoint(targetIP, targetPort);
                            buff = Encoding.UTF8.GetBytes(receivedJson);
                            udpServer.Send(buff, buff.Length, targetEP);
                            Console.WriteLine($"Forwarded to User {user.Id} on port {targetPort}");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        
                    }
                }
                Console.WriteLine($"Sent to {onlineUsers.Count - 1} users");
            }
            else
            {
                int targetUserId = incomingMsg.ChatId;
                Console.WriteLine($"[PRIVATE] From User {incomingMsg.AuthorId} to User {targetUserId}");
                var targetUser = onlineUsers.FirstOrDefault(user => user.Id == targetUserId);
                if (targetUser != null)
                {
                    IPAddress targetUserIp = IPAddress.Parse(targetUser.IPAddress);
                    int targetUserPort = targetUser.Port;
                    IPEndPoint targetUserEP = new IPEndPoint(targetUserIp, targetUserPort);
                    buff = Encoding.UTF8.GetBytes(receivedJson);
                    udpServer.Send(buff, buff.Length, targetUserEP);
                    Console.WriteLine($"Forwarded to User {targetUser.Id} on port {targetUserPort}");
                }
            }

            
            
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally { 
    udpServer?.Close(); 
}
