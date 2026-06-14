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
        public int activeChat = 1;
        public Dictionary<int, string> chats = new Dictionary<int, string>();

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "10000";
            button1.Enabled = false;
            button4.Enabled = false;
            comboBox1.SelectedIndex = 0;
            panelPayAttention.Visible = false;
            panelChat.Visible = false;
            panelRegistrate.Visible = false;
            panelLogin.Visible = true;
        }
        UdpClient _udpClient = new UdpClient();
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000);

        private void StartListening()
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    byte[] buff = client.Receive(ref remoteEP);
                    var packet = Packet.FromBytes(buff);
                    string displayTemplate;
                    if (packet.Type == PacketType.IncomingMessage)
                    {
                        var incomingMsg = packet.GetPayload<IncomingMessagePayload>();
                        if (incomingMsg.ChatId >= 1 && incomingMsg.ChatId <= 100)
                        {
                            displayTemplate = $"[GENERAL CHAT_{incomingMsg.ChatId}] User {incomingMsg.SenderId}: {incomingMsg.Text}\r\n";

                            textBox5.BeginInvoke(new Action(() =>
                            {
                                if (!chats.ContainsKey(incomingMsg.ChatId))
                                {
                                    chats[incomingMsg.ChatId] = "";
                                }

                                chats[incomingMsg.ChatId] += displayTemplate;

                                if (incomingMsg.ChatId == activeChat)
                                {
                                    textBox5.AppendText(displayTemplate);
                                }
                            }));
                        }
                        else
                        {
                            displayTemplate = $"[PRIVATE from User {incomingMsg.SenderId}]: {incomingMsg.Text}";
                            TextUpdate(textBox8, displayTemplate);
                        }
                    }
                }
                catch (SocketException ex)
                {
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }

        }

        private void TextUpdate(TextBox targetTextBox, string text)
        {
            targetTextBox.BeginInvoke(new Action(() => targetTextBox.AppendText(text + Environment.NewLine)));
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var packet = Packet.Create(PacketType.SendGroupMessage, new SendGroupMessagePayload
            {
                ChatId = comboBox1.SelectedIndex + 1,
                Text = textBox4.Text
            });
            packet.UserId = localPort;
            var bytes = packet.ToBytes();
            await client.SendAsync(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));
            string msg = $"[YOU to GENERAL CHAT_{activeChat}]: {textBox4.Text}\r\n";
            if (!chats.ContainsKey(activeChat))
            {
                chats[activeChat] = "";
            }
            chats[activeChat] += msg;
            textBox5.AppendText(msg);
            textBox4.Clear();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox6.Text, out int targetPort))
            {
                MessageBox.Show("Enter user's Port!");
                return;
            }
            var packet = Packet.Create(PacketType.SendPrivateMessage, new SendPrivateMessagePayload
            {
                RecipientUserId = targetPort,
                Text = textBox7.Text
            });
            packet.UserId = localPort;
            var bytes = packet.ToBytes();
            await client.SendAsync(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));
            TextUpdate(textBox8, $"[PRIVATE to User {targetPort}]: {textBox7.Text}");
            textBox7.Clear();
        }

        //private void SendToServer(Message msgLog)
        //{
        //    try
        //    {
        //        string jsonMessage = JsonConvert.SerializeObject(msgLog);
        //        byte[] buff = Encoding.UTF8.GetBytes(jsonMessage);
        //        IPAddress serverAddress = IPAddress.Parse(textBox1.Text);
        //        int serverPort = int.Parse(textBox2.Text);
        //        client.Send(buff, buff.Length, new IPEndPoint(serverAddress, serverPort));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

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
                client = new UdpClient(localPort);
                Task.Run(() => StartListening());
                var packet = Packet.Create(PacketType.Connect, new ConnectPayload { UserId = localPort });
                var bytes = packet.ToBytes();
                client.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));
                button1.Enabled = true;
                button4.Enabled = true;
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
                var packet = Packet.Create(PacketType.Disconnect, new ConnectPayload { UserId = localPort });
                var bytes = packet.ToBytes();
                client.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeChat = comboBox1.SelectedIndex + 1;
            textBox5.Clear();
            if (!chats.ContainsKey(activeChat))
            {
                chats[activeChat] = "";
            }
            textBox5.Text = chats[activeChat];
        }

        private async void buttonRegistrate_Click(object sender, EventArgs e)
        {
            var payload = new RegisterPayload
            {
                Username = textBoxUsername_Reg.Text,
                Password = DataEncryptor.HashPassword(textBoxPassword_Reg.Text),
                Email = textBoxEmail_Reg.Text
            };

            var packet = Packet.Create(PacketType.Register, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<AuthResponsePayload>();

            if (data.Success)
            {
                MessageBox.Show(data.Message);
                panelRegistrate.Visible = false;
                panelLogin.Visible = true;
            }
            else
            {
                MessageBox.Show($"Registration failed: {data.Message}");
            }
        }

        private void buttonGotoRegistrate_Click(object sender, EventArgs e)
        {
            panelLogin.Visible = false;
            panelRegistrate.Visible = true;
        }

        private async void buttonLogIn_Click(object sender, EventArgs e)
        {
            var payload = new LoginPayload
            {
                Username = textBoxUsername_Log.Text,
                Password = DataEncryptor.HashPassword(textBoxPassword_Log.Text)
            };

            var packet = Packet.Create(PacketType.Login, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<AuthResponsePayload>();

            if (data.Success)
            {
                MessageBox.Show(data.Message);
                panelLogin.Visible = false;
                panelChat.Visible = true;
            }
            else
            {
                MessageBox.Show($"Log In failed: {data.Message}");
            }
        }

        private void buttonReturnToLogIn_Click(object sender, EventArgs e)
        {
            panelRegistrate.Visible = false;
            //panelLogin.Visible = true;
        }
    }
}
