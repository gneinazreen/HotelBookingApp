using System.Windows.Forms;

namespace HotelBookingApp
{
    partial class NavigationMenu
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip1;
        


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(300, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // NavigationMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.menuStrip1);
            this.Name = "NavigationMenu";
            this.Size = new System.Drawing.Size(300, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
