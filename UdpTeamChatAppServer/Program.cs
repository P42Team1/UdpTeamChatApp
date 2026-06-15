using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary;
using ChatLibrary.Data;
using ChatLibrary.Models;
using UdpTeamChatAppServer;

List<User> onlineUsers = new List<User>(); // тимчасова заміна БД поки не підключимо її
int port = 10000;
UdpClient udpServer = new UdpClient(port);
ChatContextFactory chatContextFactory = new ChatContextFactory();
using ChatContext context = chatContextFactory.CreateDbContext(args);
Service service = new Service(context);
Handler handlers = new Handler(udpServer, service, onlineUsers);
int tempClientPort = 10020;


try
{
    while (true)
    {
        try
        {
            var result = await udpServer.ReceiveAsync();
            var packet = Packet.FromBytes(result.Buffer);
            var remoteEP = result.RemoteEndPoint;
            try
            {
                var payload = packet.GetPayload<IncomingMessagePayload>();
                Console.WriteLine(packet.Payload);
                Console.WriteLine($"[RECEIVED] User {packet.UserId}: {payload.Text} (Chat {payload.ChatId})");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[RECEIVED] Packet type: {packet.Type} from {remoteEP}");
            }
            

            switch (packet.Type)
            {
                case PacketType.Register:
                    await handlers.HandleRegister(packet, remoteEP);
                    break;
                case PacketType.Login:
                    await handlers.HandleLogin(packet, remoteEP);
                    break;
                case PacketType.Connect:
                    await handlers.HandleConnect(packet, remoteEP);
                    break;
                case PacketType.Disconnect:
                    await handlers.HandleDisconnect(packet);
                    break;
                case PacketType.SendPrivateMessage:
                    await handlers.HandlePrivateMessage(packet);
                    break;
                case PacketType.SendGroupMessage:
                    await handlers.HandleGroupMessage(packet);
                    break;
                case PacketType.GetChats:
                    await handlers.HandleGetChats(remoteEP);
                    break;
                case PacketType.CreateChat:
                    await handlers.HandleCreateChat(packet, remoteEP);
                    break;
                default:
                    Console.WriteLine("Unknown packet type received");
                    break;
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
