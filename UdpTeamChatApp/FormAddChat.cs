using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpTeamChatApp
{
    public partial class FormAddChat : Form
    {
        public FormAddChat()
        {
            InitializeComponent();
        }
        public string ChatName => textBox1.Text;
        public List<string> AvailableContacts
        {
            set
            {
                checkedListBox1.Items.Clear();
                if (value == null) return;
                foreach (var name in value)
                    checkedListBox1.Items.Add(name, false);
            }
        }
        public List<string> SelectedMembers => checkedListBox1.CheckedItems.Cast<string>().ToList();

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ChatName))
            {
                MessageBox.Show("Enter chat name!");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
