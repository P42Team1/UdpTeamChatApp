using System.Net;
using System.Net.Sockets;
using System.Text;
using CharLibrary;
using UdpTeamChatAppServer;

int port = 10000;
UdpClient udpServer = new UdpClient(port);
AuthHandler authHandler = new AuthHandler(udpServer);
try
{
    //IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
    while (true)
    {
        //byte[] buff = udpServer.Receive(ref remoteEP);
        //string receivedText = Encoding.UTF8.GetString(buff);
        //Console.WriteLine(receivedText);
        var result = await udpServer.ReceiveAsync();
        var packet = Packet.FromBytes(result.Buffer);
        var clientEP = result.RemoteEndPoint;
        Console.WriteLine($"{packet.Type} | {packet.UserId} | {packet.Payload}");

        switch (packet.Type)
        {
            case PacketType.Register:
                await authHandler.HandleRegister(packet, clientEP);
                break;
            case PacketType.Login:
                await authHandler.HandleLogin(packet, clientEP);
                break;
            default:
                Console.WriteLine("Unknown packet type");
                break;
        }
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally { udpServer?.Close(); }