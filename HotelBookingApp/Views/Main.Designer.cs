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
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBookings
            // 
            this.btnBookings.BackColor = System.Drawing.Color.Snow;
            this.btnBookings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBookings.Location = new System.Drawing.Point(37, 112);
            this.btnBookings.Name = "btnBookings";
            this.btnBookings.Size = new System.Drawing.Size(116, 45);
            this.btnBookings.TabIndex = 0;
            this.btnBookings.Text = "Manage Bookings";
            this.btnBookings.UseVisualStyleBackColor = false;
            this.btnBookings.Click += new System.EventHandler(this.btnBookings_Click);
            // 
            // btnRooms
            // 
            this.btnRooms.Location = new System.Drawing.Point(186, 112);
            this.btnRooms.Name = "btnRooms";
            this.btnRooms.Size = new System.Drawing.Size(96, 45);
            this.btnRooms.TabIndex = 1;
            this.btnRooms.Text = "Manage Rooms";
            this.btnRooms.UseVisualStyleBackColor = true;
            this.btnRooms.Click += new System.EventHandler(this.btnRooms_Click);
            // 
            // btnRequests
            // 
            this.btnRequests.Location = new System.Drawing.Point(313, 112);
            this.btnRequests.Name = "btnRequests";
            this.btnRequests.Size = new System.Drawing.Size(118, 47);
            this.btnRequests.TabIndex = 2;
            this.btnRequests.Text = "Manage Requests";
            this.btnRequests.UseVisualStyleBackColor = true;
            this.btnRequests.Click += new System.EventHandler(this.btnRequests_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(109, 180);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(106, 41);
            this.btnReports.TabIndex = 2;
            this.btnReports.Text = "Weekly Report";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnChatbot
            // 
            this.btnChatbot.Location = new System.Drawing.Point(254, 180);
            this.btnChatbot.Name = "btnChatbot";
            this.btnChatbot.Size = new System.Drawing.Size(100, 41);
            this.btnChatbot.TabIndex = 2;
            this.btnChatbot.Text = "Chatbot";
            this.btnChatbot.UseVisualStyleBackColor = true;
            this.btnChatbot.Click += new System.EventHandler(this.btnChatbot_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(141, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 24);
            this.label4.TabIndex = 17;
            this.label4.Text = "My Hotel Dashboard";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(465, 290);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnChatbot);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnRequests);
            this.Controls.Add(this.btnRooms);
            this.Controls.Add(this.btnBookings);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBookings;
        private System.Windows.Forms.Button btnRooms;
        private System.Windows.Forms.Button btnRequests;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnChatbot;
        private System.Windows.Forms.Label label4;
    }
}