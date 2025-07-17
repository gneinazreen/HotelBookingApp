namespace HotelBookingApp.Views
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBookings = new System.Windows.Forms.Button();
            this.btnRooms = new System.Windows.Forms.Button();
            this.btnRequests = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnChatbot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBookings
            // 
            this.btnBookings.Location = new System.Drawing.Point(49, 133);
            this.btnBookings.Name = "btnBookings";
            this.btnBookings.Size = new System.Drawing.Size(116, 45);
            this.btnBookings.TabIndex = 0;
            this.btnBookings.Text = "Manage Bookings";
            this.btnBookings.UseVisualStyleBackColor = true;
            this.btnBookings.Click += new System.EventHandler(this.btnBookings_Click);
            // 
            // btnRooms
            // 
            this.btnRooms.Location = new System.Drawing.Point(196, 131);
            this.btnRooms.Name = "btnRooms";
            this.btnRooms.Size = new System.Drawing.Size(96, 45);
            this.btnRooms.TabIndex = 1;
            this.btnRooms.Text = "Manage Rooms";
            this.btnRooms.UseVisualStyleBackColor = true;
            this.btnRooms.Click += new System.EventHandler(this.btnRooms_Click);
            // 
            // btnRequests
            // 
            this.btnRequests.Location = new System.Drawing.Point(314, 131);
            this.btnRequests.Name = "btnRequests";
            this.btnRequests.Size = new System.Drawing.Size(118, 47);
            this.btnRequests.TabIndex = 2;
            this.btnRequests.Text = "Manage Requests";
            this.btnRequests.UseVisualStyleBackColor = true;
            this.btnRequests.Click += new System.EventHandler(this.btnRequests_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(102, 203);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(106, 41);
            this.btnReports.TabIndex = 2;
            this.btnReports.Text = "Weekly Report";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnChatbot
            // 
            this.btnChatbot.Location = new System.Drawing.Point(243, 203);
            this.btnChatbot.Name = "btnChatbot";
            this.btnChatbot.Size = new System.Drawing.Size(100, 41);
            this.btnChatbot.TabIndex = 2;
            this.btnChatbot.Text = "Chatbot";
            this.btnChatbot.UseVisualStyleBackColor = true;
            this.btnChatbot.Click += new System.EventHandler(this.btnChatbot_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 290);
            this.Controls.Add(this.btnChatbot);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnRequests);
            this.Controls.Add(this.btnRooms);
            this.Controls.Add(this.btnBookings);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBookings;
        private System.Windows.Forms.Button btnRooms;
        private System.Windows.Forms.Button btnRequests;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnChatbot;
    }
}