using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlServer.Companion.Enviorment.Windows
{
	/// <summary>
	/// Summary description for TrayIcon.
	/// </summary>
	public class TrayIcon : System.Windows.Forms.Form
	{
		#region Controls
		private System.Windows.Forms.ContextMenu TrayMenu;
		private System.Windows.Forms.NotifyIcon TI;
		private System.Windows.Forms.MenuItem EnviormentMI;
		private System.Windows.Forms.MenuItem ExitMI;
		private System.Windows.Forms.MenuItem Seperator01MI;
		private System.ComponentModel.IContainer components;
		
		#endregion
		#region Generated
		public TrayIcon(){InitializeComponent();}
		protected override void Dispose( bool disposing )
		{
			if(TI.Visible)
				TI.Visible = false;

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TrayIcon));
			this.TrayMenu = new System.Windows.Forms.ContextMenu();
			this.EnviormentMI = new System.Windows.Forms.MenuItem();
			this.Seperator01MI = new System.Windows.Forms.MenuItem();
			this.ExitMI = new System.Windows.Forms.MenuItem();
			this.TI = new System.Windows.Forms.NotifyIcon(this.components);
			// 
			// TrayMenu
			// 
			this.TrayMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.EnviormentMI,
																					 this.Seperator01MI,
																					 this.ExitMI});
			// 
			// EnviormentMI
			// 
			this.EnviormentMI.Index = 0;
			this.EnviormentMI.Text = "Hide/Show Enviorment";
			this.EnviormentMI.Click += new System.EventHandler(this.EnviormentMI_Click);
			// 
			// Seperator01MI
			// 
			this.Seperator01MI.Index = 1;
			this.Seperator01MI.Text = "-";
			// 
			// ExitMI
			// 
			this.ExitMI.Index = 2;
			this.ExitMI.Text = "Exit";
			this.ExitMI.Click += new System.EventHandler(this.ExitMI_Click);
			// 
			// TI
			// 
			this.TI.ContextMenu = this.TrayMenu;
			this.TI.Icon = ((System.Drawing.Icon)(resources.GetObject("TI.Icon")));
			this.TI.Text = "SqlServer Companion";
			// 
			// TrayIcon
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(180, 44);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Location = new System.Drawing.Point(-3000, -3000);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(180, 44);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(180, 44);
			this.Name = "TrayIcon";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "TrayIcon";
			this.TransparencyKey = System.Drawing.SystemColors.Control;
			this.Load += new System.EventHandler(this.TrayIcon_Load);

		}
		#endregion
		#region Load
		private void TrayIcon_Load(object sender, System.EventArgs e)
		{
			#region Splash
			Splash splash_screen = new Splash();
			splash_screen.Show();

			Common.Pause(new TimeSpan(0, 0, 0, 2, 50));

			splash_screen.Dispose();
			splash_screen = null;	
			#endregion

			TI.Visible = true;
			Common.MDI.Visible = true;
		}
		#endregion
		#region Exit
		private void ExitMI_Click(object sender, System.EventArgs e)
		{
			TI.Visible = false;
			this.Dispose();
			Application.Exit();
		}
		#endregion
		#region Show/Hide Enviorment
		private void EnviormentMI_Click(object sender, System.EventArgs e)
		{
			if(Common.MDI.Visible)
				Common.MDI.Visible = false;
			else
				Common.MDI.Visible = true;
		}
		#endregion
		#region Test Events
		private void ProjectPathMI_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(Common.PROJECT_PATH);
		}

		#endregion
	}
}