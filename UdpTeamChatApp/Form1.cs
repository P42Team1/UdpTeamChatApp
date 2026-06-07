using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpTeamChatApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox3.Text;
            if (string.IsNullOrEmpty(message) )
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
    }
}
