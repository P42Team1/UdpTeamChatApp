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
    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
    while (true)
    {
        byte[] buff = udpServer.Receive(ref remoteEP);
        string receivedText = Encoding.UTF8.GetString(buff);
        Console.WriteLine(receivedText);
        await authHandler.HandleRegister(Packet.FromBytes(buff), remoteEP);
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally { udpServer?.Close(); }