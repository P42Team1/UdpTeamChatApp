using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary.Models;
using Microsoft.VisualBasic.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace UdpTeamChatApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        UdpClient _udpClient = new UdpClient();
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000);

        private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox3.Text;
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Enter message first");
                return;
            }

            using (UdpClient udpClient = new UdpClient())
            {
                try
                {
                    byte[] buff = Encoding.UTF8.GetBytes(message);
                    IPAddress address = IPAddress.Parse(textBox1.Text);
                    int port = int.Parse(textBox2.Text);
                    udpClient.Send(buff, buff.Length, new IPEndPoint(address, port));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelLogin.Visible = true;
            panelRegistrate.Visible = false;
            panelServer.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelLogin.Visible = false;
            panelServer.Visible = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            panelLogin.Visible = false;
            panelRegistrate.Visible = true;
        }

        private async void buttonRegistrate_Click(object sender, EventArgs e)
        {
            //Data from register page
            var payload = new RegisterPayload
            {
                Username = textBoxLogin_Reg.Text,
                Password = textBoxPassword_Reg.Text,
                Email = textBoxEmail_Reg.Text
            };
            //Create packet and send to server
            var packet = Packet.Create(PacketType.Register, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);
            //Wait for response
            var result = await _udpClient.ReceiveAsync();
            var responsePacket = Packet.FromBytes(result.Buffer);
            var data = responsePacket.GetPayload<AuthResponsePayload>();

            if (data.Success)
            {
                MessageBox.Show(data.Message);
                panelRegistrate.Visible = false;
                panelLogin.Visible = true;
            }
            else
                MessageBox.Show($"Registration failed: {data.Message}");
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            //Data from login page
            var payload = new LoginPayload
            {
                Username = textBoxLogin_Log.Text,
                Password = textBoxPassword_Log.Text,
            };
            //Create packet and send to server
            var packet = Packet.Create(PacketType.Login, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);
            //Wait for response
            var result = await _udpClient.ReceiveAsync();
            var responsePacket = Packet.FromBytes(result.Buffer);
            var data = responsePacket.GetPayload<AuthResponsePayload>();

            if (data.Success)
            {
                MessageBox.Show(data.Message);
                panelLogin.Visible = false;
                panelServer.Visible = true;
            }
            else
                MessageBox.Show($"Login failed: {data.Message}");
        }
    }
}
