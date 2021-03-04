using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlServer.Companion.Enviorment.Windows
{
	/// <summary>
	/// Summary description for Splash.
	/// </summary>
	public class Splash : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel FramePNL;
		private System.Windows.Forms.PictureBox WindowsIMG;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Splash()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.FramePNL = new System.Windows.Forms.Panel();
            this.WindowsIMG = new System.Windows.Forms.PictureBox();
            this.FramePNL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsIMG)).BeginInit();
            this.SuspendLayout();
            // 
            // FramePNL
            // 
            this.FramePNL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FramePNL.Controls.Add(this.WindowsIMG);
            this.FramePNL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FramePNL.Location = new System.Drawing.Point(0, 0);
            this.FramePNL.Name = "FramePNL";
            this.FramePNL.Size = new System.Drawing.Size(432, 336);
            this.FramePNL.TabIndex = 0;
            // 
            // WindowsIMG
            // 
            this.WindowsIMG.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("WindowsIMG.BackgroundImage")));
            this.WindowsIMG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WindowsIMG.Location = new System.Drawing.Point(0, 0);
            this.WindowsIMG.Name = "WindowsIMG";
            this.WindowsIMG.Size = new System.Drawing.Size(430, 334);
            this.WindowsIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WindowsIMG.TabIndex = 0;
            this.WindowsIMG.TabStop = false;
            // 
            // Splash
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(432, 336);
            this.ControlBox = false;
            this.Controls.Add(this.FramePNL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.TopMost = true;
            this.FramePNL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WindowsIMG)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
