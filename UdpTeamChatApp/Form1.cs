using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary;
using Newtonsoft.Json;
using ChatLibrary.Models;
using Message = ChatLibrary.Models.Message;
using System.Threading.Tasks;

namespace UdpTeamChatApp
{
    public partial class Form1 : Form
    {

        private UdpClient client;
        public int localPort;
        public int activeChat = 1;
        public Dictionary<int, string> chats = new Dictionary<int, string>();
        int currentUserId;
        private GroupBox groupBoxContacts = null!;
        private ListBox listBoxContacts = null!;
        private TextBox textBoxContactUserId = null!;
        private Button buttonAddContact = null!;
        private Button buttonRemoveContact = null!;
        private Button buttonBlockContact = null!;
        private Button buttonUnblockContact = null!;
        private Button buttonUseContact = null!;

        public Form1()
        {
            InitializeComponent();
            InitializeContactControls();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "10000";
            label5.Text = "User Id";
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
                    if (packet.Type == PacketType.IncomingMessage || packet.Type == PacketType.IncomingPrivateMessage)
                    {
                        var incomingMsg = packet.GetPayload<IncomingMessagePayload>();
                        if (packet.Type == PacketType.IncomingMessage)
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
                        else if (packet.Type == PacketType.IncomingPrivateMessage)
                        {
                            displayTemplate = $"[PRIVATE from User {incomingMsg.SenderId}]: {incomingMsg.Text}";
                            TextUpdate(textBox8, displayTemplate);
                        }
                    }
                    else if (packet.Type == PacketType.CreateChatResponse)
                    {
                        comboBox1.BeginInvoke(new Action(async () =>
                        {
                            await LoadChatsAsync();
                        }));
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
                SenderId = currentUserId,
                ChatId = comboBox1.SelectedIndex + 1,
                Text = textBox4.Text
            });
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
            if (!int.TryParse(textBox6.Text, out int targetId))
            {
                MessageBox.Show("Enter user's Id!");
                return;
            }
            var packet = Packet.Create(PacketType.SendPrivateMessage, new SendPrivateMessagePayload
            {
                SenderId = currentUserId,
                RecipientUserId = targetId,
                Text = textBox7.Text
            });
            var bytes = packet.ToBytes();
            await client.SendAsync(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));
            TextUpdate(textBox8, $"[PRIVATE to User {targetId}]: {textBox7.Text}");
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
                var packet = Packet.Create(PacketType.Connect, new ConnectPayload { UserId = currentUserId });
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
                var packet = Packet.Create(PacketType.Disconnect, new ConnectPayload { UserId = currentUserId });
                var bytes = packet.ToBytes();
                client.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));
                Thread.Sleep(100);
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
                currentUserId = data.UserId;
                MessageBox.Show(data.Message);
                panelLogin.Visible = false;
                panelChat.Visible = true;
                await LoadChatsAsync();
                await LoadContactsAsync();
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

        public async Task LoadChatsAsync()
        {
            var payload = new GetChatsPayload { UserId = currentUserId };
            var packet = Packet.Create(PacketType.GetChats, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<ChatsResponsePayload>();

            comboBox1.Items.Clear();
            foreach (var item in data.Chats)
            {
                comboBox1.Items.Add(item.Name);
            }
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        private async void buttonCreateChat_Click(object sender, EventArgs e)
        {
            var form = new FormAddChat();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var packet = Packet.Create(PacketType.CreateChat, new CreateChatPayload
                {
                    Name = form.ChatName,
                    CreatorId = localPort,
                });
                var bytes = packet.ToBytes();
                await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

                var result = await _udpClient.ReceiveAsync();
                var response = Packet.FromBytes(result.Buffer);
                var data = response.GetPayload<CreateChatResponsePayload>();
                if (data.Success)
                {
                    MessageBox.Show($"Chat '{form.ChatName}' created");
                    await LoadChatsAsync();
                }
            }
        }

        private void InitializeContactControls()
        {
            groupBoxContacts = new GroupBox
            {
                Name = "groupBoxContacts",
                Text = "Contacts / blacklist",
                Location = new Point(833, 785),
                Size = new Size(769, 160)
            };

            listBoxContacts = new ListBox
            {
                Name = "listBoxContacts",
                Location = new Point(12, 30),
                Size = new Size(280, 110)
            };

            textBoxContactUserId = new TextBox
            {
                Name = "textBoxContactUserId",
                Location = new Point(315, 30),
                Size = new Size(120, 27)
            };

            Label labelContactUserId = new Label
            {
                AutoSize = true,
                Text = "User Id",
                Location = new Point(315, 7)
            };

            buttonAddContact = new Button
            {
                Name = "buttonAddContact",
                Text = "Add",
                Location = new Point(450, 28),
                Size = new Size(75, 31)
            };
            buttonAddContact.Click += async (_, _) => await RunContactAction(PacketType.AddContact);

            buttonRemoveContact = new Button
            {
                Name = "buttonRemoveContact",
                Text = "Remove",
                Location = new Point(540, 28),
                Size = new Size(85, 31)
            };
            buttonRemoveContact.Click += async (_, _) => await RunContactAction(PacketType.RemoveContact, true);

            buttonBlockContact = new Button
            {
                Name = "buttonBlockContact",
                Text = "Block",
                Location = new Point(450, 70),
                Size = new Size(75, 31)
            };
            buttonBlockContact.Click += async (_, _) => await RunContactAction(PacketType.BlockContact, true);

            buttonUnblockContact = new Button
            {
                Name = "buttonUnblockContact",
                Text = "Unblock",
                Location = new Point(540, 70),
                Size = new Size(85, 31)
            };
            buttonUnblockContact.Click += async (_, _) => await RunContactAction(PacketType.UnblockContact, true);

            buttonUseContact = new Button
            {
                Name = "buttonUseContact",
                Text = "Use",
                Location = new Point(640, 49),
                Size = new Size(90, 31)
            };
            buttonUseContact.Click += (_, _) =>
            {
                if (GetSelectedContactUserId(out int userId))
                    textBox6.Text = userId.ToString();
            };

            groupBoxContacts.Controls.Add(labelContactUserId);
            groupBoxContacts.Controls.Add(listBoxContacts);
            groupBoxContacts.Controls.Add(textBoxContactUserId);
            groupBoxContacts.Controls.Add(buttonAddContact);
            groupBoxContacts.Controls.Add(buttonRemoveContact);
            groupBoxContacts.Controls.Add(buttonBlockContact);
            groupBoxContacts.Controls.Add(buttonUnblockContact);
            groupBoxContacts.Controls.Add(buttonUseContact);
            panelChat.Controls.Add(groupBoxContacts);
        }

        private async Task LoadContactsAsync()
        {
            if (currentUserId == 0)
                return;

            var packet = Packet.Create(PacketType.GetContacts, new GetContactsPayload { UserId = currentUserId });
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<ContactsResponsePayload>();
            if (data == null)
                return;

            listBoxContacts.Items.Clear();
            foreach (ContactInfo contact in data.Contacts)
            {
                listBoxContacts.Items.Add(new ContactListItem(contact));
            }
        }

        private async Task RunContactAction(PacketType action, bool allowSelection = false)
        {
            if (!TryGetContactActionUserId(allowSelection, out int contactUserId))
                return;

            var packet = Packet.Create(action, new ContactActionPayload
            {
                OwnerId = currentUserId,
                ContactUserId = contactUserId
            });
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<ContactActionResponsePayload>();
            if (data == null)
                return;

            MessageBox.Show(data.Message);
            if (data.Success)
                await LoadContactsAsync();
        }

        private bool TryGetContactActionUserId(bool allowSelection, out int userId)
        {
            if (allowSelection && GetSelectedContactUserId(out userId))
                return true;

            if (int.TryParse(textBoxContactUserId.Text, out userId))
                return true;

            MessageBox.Show("Enter or select a user id.");
            return false;
        }

        private bool GetSelectedContactUserId(out int userId)
        {
            if (listBoxContacts.SelectedItem is ContactListItem contact)
            {
                userId = contact.UserId;
                return true;
            }

            userId = 0;
            return false;
        }

        private class ContactListItem
        {
            public int UserId { get; }
            private readonly string _displayText;

            public ContactListItem(ContactInfo contact)
            {
                UserId = contact.UserId;
                string status = contact.IsBlacklisted ? "blocked" : "contact";
                _displayText = $"{contact.Username} (Id {contact.UserId}) - {status}";
            }

            public override string ToString()
            {
                return _displayText;
            }
        }
    }
}
