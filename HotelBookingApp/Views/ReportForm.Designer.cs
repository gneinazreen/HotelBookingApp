namespace HotelBookingApp.Views
{
    partial class ReportForm
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
            this.listViewReport = new System.Windows.Forms.ListView();
            this.day = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.guest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roomType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.request = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExportCSV = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpWeekStart = new System.Windows.Forms.DateTimePicker();
            this.btnLoadWeek = new System.Windows.Forms.Button();
            this.lblWeekStartDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewReport
            // 
            this.listViewReport.BackColor = System.Drawing.Color.Honeydew;
            this.listViewReport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.day,
            this.guest,
            this.roomType,
            this.request});
            this.listViewReport.HideSelection = false;
            this.listViewReport.Location = new System.Drawing.Point(16, 182);
            this.listViewReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewReport.Name = "listViewReport";
            this.listViewReport.Size = new System.Drawing.Size(655, 223);
            this.listViewReport.TabIndex = 0;
            this.listViewReport.UseCompatibleStateImageBehavior = false;
            this.listViewReport.View = System.Windows.Forms.View.Details;
            this.listViewReport.SelectedIndexChanged += new System.EventHandler(this.listViewReport_SelectedIndexChanged);
            // 
            // day
            // 
            this.day.Text = "Day";
            // 
            // guest
            // 
            this.guest.Text = "Guest";
            // 
            // roomType
            // 
            this.roomType.Text = "Room Type";
            this.roomType.Width = 141;
            // 
            // request
            // 
            this.request.Text = "Request";
            this.request.Width = 176;
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Location = new System.Drawing.Point(157, 441);
            this.btnExportCSV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(132, 28);
            this.btnExportCSV.TabIndex = 1;
            this.btnExportCSV.Text = "Export to CSV";
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(392, 441);
            this.btnExportPDF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(132, 28);
            this.btnExportPDF.TabIndex = 2;
            this.btnExportPDF.Text = "Export to PDF";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(124, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(430, 29);
            this.label4.TabIndex = 17;
            this.label4.Text = "Weekly Summary and Export Center";
            // 
            // dtpWeekStart
            // 
            this.dtpWeekStart.Location = new System.Drawing.Point(233, 112);
            this.dtpWeekStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpWeekStart.Name = "dtpWeekStart";
            this.dtpWeekStart.Size = new System.Drawing.Size(265, 22);
            this.dtpWeekStart.TabIndex = 18;
            this.dtpWeekStart.ValueChanged += new System.EventHandler(this.dtpWeekStart_ValueChanged);
            // 
            // btnLoadWeek
            // 
            this.btnLoadWeek.Location = new System.Drawing.Point(527, 108);
            this.btnLoadWeek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoadWeek.Name = "btnLoadWeek";
            this.btnLoadWeek.Size = new System.Drawing.Size(100, 28);
            this.btnLoadWeek.TabIndex = 19;
            this.btnLoadWeek.Text = "Load Week";
            this.btnLoadWeek.UseVisualStyleBackColor = true;
            this.btnLoadWeek.Click += new System.EventHandler(this.btnLoadWeek_Click);
            // 
            // lblWeekStartDate
            // 
            this.lblWeekStartDate.AutoSize = true;
            this.lblWeekStartDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblWeekStartDate.Location = new System.Drawing.Point(16, 116);
            this.lblWeekStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeekStartDate.Name = "lblWeekStartDate";
            this.lblWeekStartDate.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblWeekStartDate.Size = new System.Drawing.Size(170, 20);
            this.lblWeekStartDate.TabIndex = 20;
            this.lblWeekStartDate.Text = "Select Week Starting Date";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(688, 508);
            this.Controls.Add(this.lblWeekStartDate);
            this.Controls.Add(this.btnLoadWeek);
            this.Controls.Add(this.dtpWeekStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.btnExportCSV);
            this.Controls.Add(this.listViewReport);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewReport;
        private System.Windows.Forms.Button btnExportCSV;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.ColumnHeader day;
        private System.Windows.Forms.ColumnHeader guest;
        private System.Windows.Forms.ColumnHeader roomType;
        private System.Windows.Forms.ColumnHeader request;
        //private NavigationMenu navigationMenu1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpWeekStart;
        private System.Windows.Forms.Button btnLoadWeek;
        private System.Windows.Forms.Label lblWeekStartDate;
    }
}