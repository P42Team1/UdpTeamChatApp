using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary;
using Newtonsoft.Json;
using ChatLibrary.Models;
using Message = ChatLibrary.Models.Message;

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
            button4.Enabled = false;
        }

        private void StartListening()
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    byte[] buff = client.Receive(ref remoteEP);
                    string receivedMessageJson = Encoding.UTF8.GetString(buff);
                    Message incomingMsg = JsonConvert.DeserializeObject<Message>(receivedMessageJson);
                    string displayTemplate;
                    if (incomingMsg.ChatId == 1)
                    {
                        displayTemplate = $"[GENERAL] User {incomingMsg.AuthorId}: {incomingMsg.Text}";
                        TextUpdate(textBox5, displayTemplate);
                    }
                    else
                    {
                        displayTemplate = $"[PRIVATE from User {incomingMsg.AuthorId}]: {incomingMsg.Text}";
                        TextUpdate(textBox8, displayTemplate);
                    }
                }
                catch(SocketException ex)
                {
                    break;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                        
            }

        }

        private void TextUpdate(TextBox targetTextBox, string text)
        {
            targetTextBox.BeginInvoke(new Action(() => targetTextBox.AppendText(text + Environment.NewLine)) );
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Message msgLog = new Message()
            {
                Id = 0, // тимчасові заглушки до підключення БД
                AuthorId = localPort,
                Text = textBox4.Text,
                Time = DateTime.Now,
                ChatId = 1,
                Status = MessageStatus.NotReceived,
            };

            SendToServer(msgLog);
            TextUpdate(textBox5, $"[YOU to GENERAL]: {textBox4.Text}");
            textBox4.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(!int.TryParse(textBox6.Text, out int targetPort))
            {
                MessageBox.Show("Enter user's Port!");
                return;
            }
            Message msgLog = new ChatLibrary.Models.Message()
            {
                Id = 0, 
                AuthorId = localPort,
                Text = textBox7.Text,
                Time = DateTime.Now,
                ChatId = targetPort,
                Status = MessageStatus.NotReceived,
            };
            SendToServer(msgLog);
            TextUpdate(textBox8, $"[PRIVATE to User {targetPort}]: {textBox7.Text}");
            textBox7.Clear();
        }

        private void SendToServer(Message msgLog)
        {
            try
            {
                string jsonMessage = JsonConvert.SerializeObject(msgLog);
                byte[] buff = Encoding.UTF8.GetBytes(jsonMessage);
                IPAddress serverAddress = IPAddress.Parse(textBox1.Text);
                int serverPort = int.Parse(textBox2.Text);
                client.Send(buff, buff.Length, new IPEndPoint(serverAddress, serverPort));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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
                button4.Enabled = true;
                client = new UdpClient(localPort);
                Task.Run(() => StartListening());
                textBox3.Enabled = false;
                MessageBox.Show($"Connected to port {localPort}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Disconnect()
        {
            if (client == null) return;
            try
            {
                Message msgLog = new Message()
                {
                    Id = 0,
                    AuthorId = localPort,
                    Text = "/disconnect",
                    Time = DateTime.Now,
                    ChatId = 1,
                    Status = MessageStatus.NotReceived,
                };

                string jsonMessage = JsonConvert.SerializeObject(msgLog);

                byte[] buff = Encoding.UTF8.GetBytes(jsonMessage);
                IPAddress serverAddress = IPAddress.Parse(textBox1.Text);
                int serverPort = int.Parse(textBox2.Text);
                client.Send(buff, buff.Length, new IPEndPoint(serverAddress, serverPort));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                client?.Close();
                client = null;

                button1.Enabled = false;
                button4.Enabled = false;
                button2.Enabled = true;
                textBox3.Enabled = true;

                MessageBox.Show("Disconnected");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
