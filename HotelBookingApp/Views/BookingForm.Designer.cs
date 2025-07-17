namespace HotelBookingApp.Views.Booking
{
    partial class BookingForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkRecurring = new System.Windows.Forms.CheckBox();
            this.txtSpecialRequest = new System.Windows.Forms.TextBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.checkOutDate = new System.Windows.Forms.DateTimePicker();
            this.checkInDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.checkOutTxt = new System.Windows.Forms.Label();
            this.checkInTxt = new System.Windows.Forms.Label();
            this.cmbRecPattern = new System.Windows.Forms.ComboBox();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LName = new System.Windows.Forms.Label();
            this.FName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listViewBookings = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.firstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roomType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.requests = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.checkRecurring);
            this.panel1.Controls.Add(this.txtSpecialRequest);
            this.panel1.Controls.Add(this.txtFName);
            this.panel1.Controls.Add(this.txtLName);
            this.panel1.Controls.Add(this.checkOutDate);
            this.panel1.Controls.Add(this.checkInDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.checkOutTxt);
            this.panel1.Controls.Add(this.checkInTxt);
            this.panel1.Controls.Add(this.cmbRecPattern);
            this.panel1.Controls.Add(this.cmbRoomType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.LName);
            this.panel1.Controls.Add(this.FName);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 399);
            this.panel1.TabIndex = 13;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(245, 336);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 46;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(36, 336);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 45;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Recurrence Pattern";
            // 
            // checkRecurring
            // 
            this.checkRecurring.AutoSize = true;
            this.checkRecurring.Location = new System.Drawing.Point(35, 253);
            this.checkRecurring.Name = "checkRecurring";
            this.checkRecurring.Size = new System.Drawing.Size(72, 17);
            this.checkRecurring.TabIndex = 43;
            this.checkRecurring.Text = "Recurring";
            this.checkRecurring.UseVisualStyleBackColor = true;
            // 
            // txtSpecialRequest
            // 
            this.txtSpecialRequest.Location = new System.Drawing.Point(146, 214);
            this.txtSpecialRequest.Name = "txtSpecialRequest";
            this.txtSpecialRequest.Size = new System.Drawing.Size(100, 20);
            this.txtSpecialRequest.TabIndex = 42;
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(146, 34);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(121, 20);
            this.txtFName.TabIndex = 41;
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(146, 69);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(121, 20);
            this.txtLName.TabIndex = 40;
            // 
            // checkOutDate
            // 
            this.checkOutDate.Location = new System.Drawing.Point(146, 178);
            this.checkOutDate.Name = "checkOutDate";
            this.checkOutDate.Size = new System.Drawing.Size(200, 20);
            this.checkOutDate.TabIndex = 39;
            // 
            // checkInDate
            // 
            this.checkInDate.Location = new System.Drawing.Point(146, 139);
            this.checkInDate.Name = "checkInDate";
            this.checkInDate.Size = new System.Drawing.Size(200, 20);
            this.checkInDate.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Special Requests";
            // 
            // checkOutTxt
            // 
            this.checkOutTxt.AutoSize = true;
            this.checkOutTxt.Location = new System.Drawing.Point(33, 185);
            this.checkOutTxt.Name = "checkOutTxt";
            this.checkOutTxt.Size = new System.Drawing.Size(84, 13);
            this.checkOutTxt.TabIndex = 36;
            this.checkOutTxt.Text = "Check-Out Date";
            // 
            // checkInTxt
            // 
            this.checkInTxt.AutoSize = true;
            this.checkInTxt.Location = new System.Drawing.Point(33, 146);
            this.checkInTxt.Name = "checkInTxt";
            this.checkInTxt.Size = new System.Drawing.Size(76, 13);
            this.checkInTxt.TabIndex = 35;
            this.checkInTxt.Text = "Check-In Date";
            // 
            // cmbRecPattern
            // 
            this.cmbRecPattern.FormattingEnabled = true;
            this.cmbRecPattern.Items.AddRange(new object[] {
            "None",
            "Weekly",
            "Monthly"});
            this.cmbRecPattern.Location = new System.Drawing.Point(146, 281);
            this.cmbRecPattern.Name = "cmbRecPattern";
            this.cmbRecPattern.Size = new System.Drawing.Size(121, 21);
            this.cmbRecPattern.TabIndex = 34;
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Items.AddRange(new object[] {
            "Single",
            "Double",
            "Suite"});
            this.cmbRoomType.Location = new System.Drawing.Point(146, 103);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(121, 21);
            this.cmbRoomType.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Room Type";
            // 
            // LName
            // 
            this.LName.AutoSize = true;
            this.LName.Location = new System.Drawing.Point(33, 76);
            this.LName.Name = "LName";
            this.LName.Size = new System.Drawing.Size(58, 13);
            this.LName.TabIndex = 32;
            this.LName.Text = "Last Name";
            // 
            // FName
            // 
            this.FName.AutoSize = true;
            this.FName.Location = new System.Drawing.Point(32, 41);
            this.FName.Name = "FName";
            this.FName.Size = new System.Drawing.Size(57, 13);
            this.FName.TabIndex = 30;
            this.FName.Text = "First Name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listViewBookings);
            this.panel2.Location = new System.Drawing.Point(384, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 397);
            this.panel2.TabIndex = 14;
            // 
            // listViewBookings
            // 
            this.listViewBookings.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listViewBookings.CheckBoxes = true;
            this.listViewBookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.firstName,
            this.lastName,
            this.roomType,
            this.checkIn,
            this.checkOut,
            this.requests});
            this.listViewBookings.Dock = System.Windows.Forms.DockStyle.Right;
            this.listViewBookings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listViewBookings.FullRowSelect = true;
            this.listViewBookings.GridLines = true;
            this.listViewBookings.HideSelection = false;
            this.listViewBookings.Location = new System.Drawing.Point(-3, 0);
            this.listViewBookings.Name = "listViewBookings";
            this.listViewBookings.Size = new System.Drawing.Size(534, 397);
            this.listViewBookings.TabIndex = 13;
            this.listViewBookings.TileSize = new System.Drawing.Size(2, 2);
            this.listViewBookings.UseCompatibleStateImageBehavior = false;
            this.listViewBookings.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 40;
            // 
            // firstName
            // 
            this.firstName.Text = "First Name";
            this.firstName.Width = 88;
            // 
            // lastName
            // 
            this.lastName.Text = "Last Name";
            this.lastName.Width = 85;
            // 
            // roomType
            // 
            this.roomType.Text = "Room Type";
            this.roomType.Width = 97;
            // 
            // checkIn
            // 
            this.checkIn.Text = "Check-In";
            this.checkIn.Width = 68;
            // 
            // checkOut
            // 
            this.checkOut.Text = "Check-Out";
            this.checkOut.Width = 77;
            // 
            // requests
            // 
            this.requests.Text = "Requests";
            this.requests.Width = 77;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(146, 336);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 47;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // BookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(917, 399);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "BookingForm";
            this.Text = "BookingForm";
            this.Load += new System.EventHandler(this.BookingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkRecurring;
        private System.Windows.Forms.TextBox txtSpecialRequest;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.DateTimePicker checkOutDate;
        private System.Windows.Forms.DateTimePicker checkInDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label checkOutTxt;
        private System.Windows.Forms.Label checkInTxt;
        private System.Windows.Forms.ComboBox cmbRecPattern;
        private System.Windows.Forms.ComboBox cmbRoomType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LName;
        private System.Windows.Forms.Label FName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listViewBookings;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader firstName;
        private System.Windows.Forms.ColumnHeader lastName;
        private System.Windows.Forms.ColumnHeader roomType;
        private System.Windows.Forms.ColumnHeader checkIn;
        private System.Windows.Forms.ColumnHeader checkOut;
        private System.Windows.Forms.ColumnHeader requests;
        private System.Windows.Forms.Button btnUpdate;
    }
}