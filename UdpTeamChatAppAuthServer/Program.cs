using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary.Data;
using ChatLibrary.Models;
using UdpTeamChatAppServer;
int port = 10001;
UdpClient udpServer = new UdpClient(port);
ChatContextFactory chatContextFactory = new ChatContextFactory();
using ChatContext context = chatContextFactory.CreateDbContext(args);
Service service = new Service(context);
AuthHandler authHandler = new AuthHandler(udpServer, service);

try
{
    while (true)
    {
        var result = await udpServer.ReceiveAsync();
        var packet = Packet.FromBytes(result.Buffer);
        var remoteEP = result.RemoteEndPoint;
        Console.WriteLine(packet.Payload);
        switch (packet.Type)
        {
            case PacketType.Register:
                await authHandler.HandleRegister(packet, remoteEP);
                break;
            case PacketType.Login:
                await authHandler.HandleLogin(packet, remoteEP);
                break;
            default:
                Console.WriteLine("Unknown packet type received");
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally { udpServer?.Close(); }