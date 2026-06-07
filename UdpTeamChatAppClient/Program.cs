using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary;

List<User> onlineUsers = new List<User>(); // тимчасова заміна БД поки не підключимо її

int port = 10000;
UdpClient udpServer = new UdpClient(port);


int tempClientPort = 10020;


try
{
    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
    while (true)
    {
        byte[] buff = udpServer.Receive(ref remoteEP);
        string receivedJson = Encoding.UTF8.GetString(buff);
        Message incomingMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(receivedJson);
        Console.WriteLine($"[RECEIVED] User {incomingMsg.AuthorId}: {incomingMsg.Text} (Chat {incomingMsg.ChatId})");

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

        foreach (var user in onlineUsers)
        {
            if (user.Id != incomingMsg.AuthorId)
            {
                IPAddress targetIP = IPAddress.Parse(user.IPAddress);
                int targetPort = user.Port;
                IPEndPoint targetEP = new IPEndPoint(targetIP, targetPort);
                string sendMessage = $"[From User {incomingMsg.AuthorId}]: {incomingMsg.Text}";
                buff = Encoding.UTF8.GetBytes(receivedJson);
                udpServer.Send(buff, buff.Length, targetEP);
                Console.WriteLine($"Forwarded to User {user.Id} on port {targetPort}");
            }

        }
        Console.WriteLine($"Sent to {onlineUsers.Count - 1} users");
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally { udpServer?.Close(); }