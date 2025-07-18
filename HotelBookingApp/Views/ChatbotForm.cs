using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingApp.Views
{
    public partial class ChatbotForm : Form
    {
        public ChatbotForm()
        {
            InitializeComponent();
            this.Name = "ChatbotForm";
        }

        private void ChatbotForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAsk_Click(object sender, EventArgs e)
        {
            //string question = txtQuestion.Text.ToLower();
            //string response = GetChatbotResponse(question);
            //txtResponse.Text = response;
            string question = txtQuestion.Text;
            string response = ChatbotService.GetResponse(question);
            txtResponse.Text = response;

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
