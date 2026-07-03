using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary;
using ChatLibrary.Data;
using ChatLibrary.Models;
using UdpTeamChatAppServer;

int port = 10000;
UdpClient udpServer = new UdpClient(port);
ChatContextFactory chatContextFactory = new ChatContextFactory();
using ChatContext context = chatContextFactory.CreateDbContext(args);
Service service = new Service(context);
await service.ResetAllUsersOffline();
List<User> onlineUsers = await service.GetOnlineUsers() ?? new List<User>();
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
            Console.WriteLine($"[RECEIVED] {packet.Type} from {remoteEP} | {packet.Payload}");


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
                    Console.WriteLine($"[DEBUG] Disconnect received from {remoteEP}");
                    await handlers.HandleDisconnect(packet);
                    break;
                case PacketType.SendPrivateMessage:
                    await handlers.HandlePrivateMessage(packet);
                    break;
                case PacketType.SendGroupMessage:
                    await handlers.HandleGroupMessage(packet);
                    break;
                case PacketType.GetChats:
                    var payload = packet.GetPayload<GetChatsPayload>();
                    await handlers.HandleGetChats(remoteEP, payload.UserId);
                    break;
                case PacketType.CreateChat:
                    await handlers.HandleCreateChat(packet, remoteEP);
                    break;
                case PacketType.GetContacts:
                    await handlers.HandleGetContacts(packet, remoteEP);
                    break;
                case PacketType.AddContact:
                case PacketType.RemoveContact:
                case PacketType.BlockContact:
                case PacketType.UnblockContact:
                    await handlers.HandleContactAction(packet, remoteEP);
                    break;
                default:
                    Console.WriteLine("Unknown packet type received");
                    break;
            }         
        }
        catch (Exception ex) { Console.WriteLine(ex); }
        
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally { 
    udpServer?.Close(); 
}
