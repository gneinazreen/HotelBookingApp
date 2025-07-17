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
            this.navigationMenu1 = new HotelBookingApp.NavigationMenu();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewReport
            // 
            this.listViewReport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.day,
            this.guest,
            this.roomType,
            this.request});
            this.listViewReport.HideSelection = false;
            this.listViewReport.Location = new System.Drawing.Point(12, 93);
            this.listViewReport.Name = "listViewReport";
            this.listViewReport.Size = new System.Drawing.Size(492, 237);
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
            this.btnExportCSV.Location = new System.Drawing.Point(118, 358);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(99, 23);
            this.btnExportCSV.TabIndex = 1;
            this.btnExportCSV.Text = "Export to CSV";
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(294, 358);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(99, 23);
            this.btnExportPDF.TabIndex = 2;
            this.btnExportPDF.Text = "Export to PDF";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // navigationMenu1
            // 
            this.navigationMenu1.Location = new System.Drawing.Point(0, 0);
            this.navigationMenu1.Name = "navigationMenu1";
            this.navigationMenu1.Size = new System.Drawing.Size(516, 28);
            this.navigationMenu1.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(93, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(347, 24);
            this.label4.TabIndex = 17;
            this.label4.Text = "Weekly Summary and Export Center";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(516, 413);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.navigationMenu1);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.btnExportCSV);
            this.Controls.Add(this.listViewReport);
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
        private NavigationMenu navigationMenu1;
        private System.Windows.Forms.Label label4;
    }
}