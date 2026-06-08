using System.Net;
using System.Net.Sockets;
using System.Text;
using UdpTeamChatApp.Data;

namespace UdpTeamChatApp
{
    public partial class Form1 : Form
    {
        private Context context; 
        public Form1()
        {
            InitializeComponent();
            ContextFactory contextFactory = new ContextFactory();
            context = contextFactory.CreateDbContext(args);
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
