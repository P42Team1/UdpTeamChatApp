using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 10000;
UdpClient udpServer = new UdpClient(port);

try
{
    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
    while (true)
    {
        byte[] buff = udpServer.Receive(ref remoteEP);
        string receivedText = Encoding.UTF8.GetString(buff);
        Console.WriteLine(receivedText);
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally { udpServer?.Close(); }