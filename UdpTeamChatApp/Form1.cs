using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatLibrary;
using Newtonsoft.Json;
using ChatLibrary.Models;
using Message = ChatLibrary.Models.Message;
using System.Threading.Tasks;
using ChatLibrary.Data;

namespace UdpTeamChatApp
{
    public partial class Form1 : Form
    {
        private Service _service;
        private UdpClient client;
        public int localPort;
        public int activeChat = 1;
        public Dictionary<int, string> chats = new Dictionary<int, string>();
        public List<ContactInfo> contactsList = new List<ContactInfo>();
        public Dictionary<int, string> privateChats = new Dictionary<int, string>();
        int activeContactId = 0;
        int currentUserId;
        string currentUserName;
        public List<ChatInfo> chatsList = new List<ChatInfo>();
        public Form1(Service service)
        {
            InitializeComponent();
            _service = service;
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

        private async void StartListening()
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
                        var SenderName = await _service.GetUsernameById(incomingMsg.SenderId);
                        if (packet.Type == PacketType.IncomingMessage)
                        {
                            displayTemplate = $"[GENERAL CHAT_{incomingMsg.ChatId}] User {currentUserName}: {incomingMsg.Text}\r\n";

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
                            var privMsg = packet.GetPayload<IncomingPrivateMessagePayload>();
                            displayTemplate = $"[PRIVATE from User {SenderName.Username}]: {privMsg.Text}\r\n";

                            textBox8.BeginInvoke(new Action(() =>
                            {
                                if (!privateChats.ContainsKey(privMsg.SenderId))
                                    privateChats[privMsg.SenderId] = "";
                                privateChats[privMsg.SenderId] += displayTemplate;

                                if (privMsg.SenderId == activeContactId)
                                    textBox8.AppendText(displayTemplate);
                            }));
                        }
                    }
                    else if (packet.Type == PacketType.CreateChatResponse)
                    {
                        comboBox1.BeginInvoke(new Action(async () =>
                        {
                            await LoadChatsAsync();
                        }));
                    }
                    else if (packet.Type == PacketType.ChatHistoryResponse)
                    {
                        var data = packet.GetPayload<ChatHistoryResponsePayload>();
                        foreach (var msg in data.Messages)
                        {
                            string who = msg.SenderId == currentUserId ? "YOU" : $"User {msg.SenderId}";
                            if (msg.IsGroup)
                            {
                                string line = $"[GENERAL CHAT_{msg.ChatId}] {who}: {msg.Text}\r\n";
                                comboBox1.BeginInvoke(new Action(() =>
                                {
                                    if (!chats.ContainsKey(msg.ChatId)) chats[msg.ChatId] = "";
                                    chats[msg.ChatId] += line;
                                    var activeInfo = chatsList.ElementAtOrDefault(comboBox1.SelectedIndex);
                                    if (activeInfo != null && activeInfo.Id == msg.ChatId)
                                    {
                                        textBox5.Text = chats[msg.ChatId];
                                    }
                                }));
                            }
                            else
                            {
                                int contactId = msg.SenderId == currentUserId ? msg.OtherUserId : msg.SenderId;
                                string line = $"[PRIVATE, {who}]: {msg.Text}\r\n";

                                textBox8.BeginInvoke(new Action(() =>
                                {
                                    if (!privateChats.ContainsKey(contactId)) privateChats[contactId] = "";
                                    privateChats[contactId] += line;

                                    if (contactId == activeContactId)
                                        textBox8.AppendText(line);
                                }));
                            }
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
            var selected = chatsList.ElementAtOrDefault(comboBox1.SelectedIndex);
            if (selected == null)
            {
                MessageBox.Show("¬ибер≥ть чат!");
                return;
            }

            var packet = Packet.Create(PacketType.SendGroupMessage, new SendGroupMessagePayload
            {
                SenderId = currentUserId,
                ChatId = selected.Id,
                Text = textBox4.Text
            });
            var bytes = packet.ToBytes();
            await client.SendAsync(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));

            string msg = $"[YOU to {selected.Name}]: {textBox4.Text}\r\n";
            if (!chats.ContainsKey(selected.Id)) chats[selected.Id] = "";
            chats[selected.Id] += msg;
            textBox5.AppendText(msg);
            textBox4.Clear();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (checkedListBoxContacts.SelectedIndex < 0)
            {
                MessageBox.Show("Select a contact!");
                return;
            }
            var selectedContact = contactsList[checkedListBoxContacts.SelectedIndex];
            var packet = Packet.Create(PacketType.SendPrivateMessage, new SendPrivateMessagePayload
            {
                SenderId = currentUserId,
                RecipientUserName = selectedContact.Username,
                Text = textBox7.Text
            });
            var bytes = packet.ToBytes();
            await client.SendAsync(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));

            string msg = $"[YOU to {selectedContact.Username}]: {textBox7.Text}\r\n";
            if (!privateChats.ContainsKey(selectedContact.UserId))
                privateChats[selectedContact.UserId] = "";
            privateChats[selectedContact.UserId] += msg;

            if (activeContactId == selectedContact.UserId)
                textBox8.AppendText(msg);

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
            var selected = chatsList.ElementAtOrDefault(comboBox1.SelectedIndex);
            if (selected == null) return;

            activeChat = selected.Id;
            textBox5.Clear();
            if (!chats.ContainsKey(activeChat))
                chats[activeChat] = "";
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
                currentUserName = data.Username;
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

            chatsList = data.Chats.Where(c => c.IsGroup).ToList();
            comboBox1.Items.Clear();
            foreach (var chat in chatsList)
            {
                comboBox1.Items.Add(chat.Name);
            }
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        private async void buttonCreateChat_Click(object sender, EventArgs e)
        {
            var form = new FormAddChat();
            form.AvailableContacts = contactsList.Select(c => c.Username).ToList();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var packet = Packet.Create(PacketType.CreateChat, new CreateChatPayload
                {
                    Name = form.ChatName,
                    CreatorId = currentUserId,
                    MemberUsernames = form.SelectedMembers
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
        public async Task LoadContactsAsync()
        {
            var payload = new GetContactsPayload { UserId = currentUserId };
            var packet = Packet.Create(PacketType.GetContacts, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<ContactsResponsePayload>();

            contactsList = data.Contacts;
            checkedListBoxContacts.Items.Clear();
            foreach (var contact in contactsList)
            {
                string label = contact.Username + (contact.IsBlacklisted ? " [blocked]" : "");
                checkedListBoxContacts.Items.Add(label);
            }
        }

        private async void buttonAddContact_Click(object sender, EventArgs e)
        {
            if (textBoxContactUsername.Text == string.Empty)
            {
                MessageBox.Show("Enter username!");
                return;
            }

            var payload = new ContactActionPayload
            {
                OwnerId = currentUserId,
                ContactUsername = textBoxContactUsername.Text
            };
            var packet = Packet.Create(PacketType.AddContact, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<ContactActionResponsePayload>();

            MessageBox.Show(data.Message);
            if (data.Success)
            {
                textBoxContactUsername.Clear();
                await LoadContactsAsync();
            }
        }

        private async void buttonRemoveContact_Click(object sender, EventArgs e)
        {
            if (checkedListBoxContacts.SelectedIndex < 0) return;
            var selected = contactsList[checkedListBoxContacts.SelectedIndex];

            var payload = new ContactActionPayload
            {
                OwnerId = currentUserId,
                ContactUsername = selected.Username
            };
            var packet = Packet.Create(PacketType.RemoveContact, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<ContactActionResponsePayload>();

            MessageBox.Show(data.Message);
            if (data.Success) await LoadContactsAsync();
        }

        private async void buttonToggleBlock_Click(object sender, EventArgs e)
        {
            if (checkedListBoxContacts.SelectedIndex < 0) return;
            var selected = contactsList[checkedListBoxContacts.SelectedIndex];

            var payload = new ContactActionPayload
            {
                OwnerId = currentUserId,
                ContactUsername = selected.Username
            };
            var type = selected.IsBlacklisted ? PacketType.UnblockContact : PacketType.BlockContact;
            var packet = Packet.Create(type, payload);
            var bytes = packet.ToBytes();
            await _udpClient.SendAsync(bytes, bytes.Length, serverEndPoint);

            var result = await _udpClient.ReceiveAsync();
            var response = Packet.FromBytes(result.Buffer);
            var data = response.GetPayload<ContactActionResponsePayload>();

            MessageBox.Show(data.Message);
            if (data.Success) await LoadContactsAsync();
        }

        private void checkedListBoxContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxContacts.SelectedIndex < 0) return;
            var contact = contactsList[checkedListBoxContacts.SelectedIndex];
            activeContactId = contact.UserId;

            textBox8.Clear();
            textBox8.Text = privateChats.TryGetValue(activeContactId, out var history) ? history : "";
        }
    }
}
