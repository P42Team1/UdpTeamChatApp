using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary;
using Newtonsoft.Json;

namespace UdpTeamChatApp
{
    public partial class Form1 : Form
    {

        private UdpClient client;
        public int localPort;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "10000";
            button1.Enabled = false;
        }

        private void StartListening()
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                while (true)
                {
                    byte[] buff = client.Receive(ref remoteEP);
                    string receivedMessageJson = Encoding.UTF8.GetString(buff);
                    ChatLibrary.Message incomingMsg = JsonConvert.DeserializeObject<ChatLibrary.Message>(receivedMessageJson);
                    string displayTemplate = $"User {incomingMsg.AuthorId}: {incomingMsg.Text}";
                    TextUpdate(displayTemplate);
                }
            }
            catch (Exception ex) { }
        }

        private void TextUpdate(string text)
        {
            StringBuilder sb = new StringBuilder(textBox5.Text);
            sb.AppendLine(text);
            sb.AppendLine();
            textBox5.BeginInvoke(() => textBox5.Text = sb.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ChatLibrary.Message msgLog = new ChatLibrary.Message()
            {
                Id = 0, // тимчасові заглушки до підключення БД
                AuthorId = localPort,
                Text = textBox4.Text,
                Time = DateTime.Now,
                ChatId = 1,
                Status = StatusDelivered.NotReceived,
            };

            string jsonMessage = JsonConvert.SerializeObject(msgLog);

            byte[] buff = Encoding.UTF8.GetBytes(jsonMessage);
            IPAddress serverAddress = IPAddress.Parse(textBox1.Text);
            int serverPort = int.Parse(textBox2.Text);
            client.Send(buff, buff.Length, new IPEndPoint(serverAddress, serverPort));
            textBox4.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                MessageBox.Show("Already connected!");
                return;
            }
            if (!int.TryParse(textBox3.Text, out localPort))
                localPort = 10020;

            try
            {
                button1.Enabled = true;
                client = new UdpClient(localPort);
                Task.Run(() => StartListening());
                textBox3.Enabled = false;
                MessageBox.Show($"Connected to port {localPort}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
