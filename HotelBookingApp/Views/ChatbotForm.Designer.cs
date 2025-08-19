namespace HotelBookingApp.Views
{
    partial class ChatbotForm
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
            this.txtResponse = new System.Windows.Forms.RichTextBox();
            this.btnAsk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.RichTextBox();
            //this.navigationMenu1 = new HotelBookingApp.NavigationMenu();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(6, 50);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(575, 125);
            this.txtResponse.TabIndex = 1;
            this.txtResponse.Text = "";
            // 
            // btnAsk
            // 
            this.btnAsk.Location = new System.Drawing.Point(277, 223);
            this.btnAsk.Name = "btnAsk";
            this.btnAsk.Size = new System.Drawing.Size(75, 23);
            this.btnAsk.TabIndex = 2;
            this.btnAsk.Text = "Ask";
            this.btnAsk.UseVisualStyleBackColor = true;
            this.btnAsk.Click += new System.EventHandler(this.btnAsk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtResponse);
            this.panel1.Location = new System.Drawing.Point(24, 252);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 185);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(238, 22);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Response Recieved";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblQuestion);
            this.panel2.Controls.Add(this.txtQuestion);
            this.panel2.Location = new System.Drawing.Point(27, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(575, 123);
            this.panel2.TabIndex = 4;
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblQuestion.Location = new System.Drawing.Point(253, 14);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Padding = new System.Windows.Forms.Padding(3);
            this.lblQuestion.Size = new System.Drawing.Size(78, 19);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "Ask Anything!";
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(3, 47);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(569, 63);
            this.txtQuestion.TabIndex = 0;
            this.txtQuestion.Text = "";
            this.txtQuestion.TextChanged += new System.EventHandler(this.txtQuestion_TextChanged);
            // 
            // navigationMenu1
            // 
            //this.navigationMenu1.Location = new System.Drawing.Point(0, -1);
            //this.navigationMenu1.Margin = new System.Windows.Forms.Padding(0);
            //this.navigationMenu1.Name = "navigationMenu1";
            //this.navigationMenu1.Size = new System.Drawing.Size(629, 26);
            //this.navigationMenu1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(247, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 24);
            this.label4.TabIndex = 17;
            this.label4.Text = "Ask Me Anything!";
            // 
            // ChatbotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(630, 464);
            this.Controls.Add(this.label4);
            //this.Controls.Add(this.navigationMenu1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAsk);
            this.Name = "ChatbotForm";
            this.Text = "ChatbotForm";
            this.Load += new System.EventHandler(this.ChatbotForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtResponse;
        private System.Windows.Forms.Button btnAsk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox txtQuestion;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label label1;
        private NavigationMenu navigationMenu1;
        private System.Windows.Forms.Label label4;
    }
}