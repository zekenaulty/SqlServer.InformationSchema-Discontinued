using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlServer.Companion.Windows
{
	/// <summary>
	/// Summary description for AddServer.
	/// </summary>
	public class AddServer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel ControlPNL;
		private System.Windows.Forms.CheckBox ShowPasswordCHK;
		private System.Windows.Forms.GroupBox ConnStrGRP;
		private System.Windows.Forms.TextBox ConnStrTXT;
		private System.Windows.Forms.Button CloseBTN;
		private System.Windows.Forms.Button OpenBTN;
		private System.Windows.Forms.Button TestBTN;
		private System.Windows.Forms.GroupBox ConnectionInformationGRP;
		private System.Windows.Forms.CheckBox IntergratedSecurityCHK;
		private System.Windows.Forms.TextBox ServerTXT;
		private System.Windows.Forms.Label ServerLBL;
		private System.Windows.Forms.Panel UserPNL;
		private System.Windows.Forms.TextBox PasswordTXT;
		private System.Windows.Forms.TextBox UserTXT;
		private System.Windows.Forms.Label PasswordLBL;
		private System.Windows.Forms.Label UserLBL;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddServer()
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
			this.ControlPNL = new System.Windows.Forms.Panel();
			this.ShowPasswordCHK = new System.Windows.Forms.CheckBox();
			this.ConnStrGRP = new System.Windows.Forms.GroupBox();
			this.ConnStrTXT = new System.Windows.Forms.TextBox();
			this.CloseBTN = new System.Windows.Forms.Button();
			this.OpenBTN = new System.Windows.Forms.Button();
			this.TestBTN = new System.Windows.Forms.Button();
			this.ConnectionInformationGRP = new System.Windows.Forms.GroupBox();
			this.IntergratedSecurityCHK = new System.Windows.Forms.CheckBox();
			this.ServerTXT = new System.Windows.Forms.TextBox();
			this.ServerLBL = new System.Windows.Forms.Label();
			this.UserPNL = new System.Windows.Forms.Panel();
			this.PasswordTXT = new System.Windows.Forms.TextBox();
			this.UserTXT = new System.Windows.Forms.TextBox();
			this.PasswordLBL = new System.Windows.Forms.Label();
			this.UserLBL = new System.Windows.Forms.Label();
			this.ControlPNL.SuspendLayout();
			this.ConnStrGRP.SuspendLayout();
			this.ConnectionInformationGRP.SuspendLayout();
			this.UserPNL.SuspendLayout();
			this.SuspendLayout();
			// 
			// ControlPNL
			// 
			this.ControlPNL.BackColor = System.Drawing.SystemColors.Control;
			this.ControlPNL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ControlPNL.Controls.Add(this.ShowPasswordCHK);
			this.ControlPNL.Controls.Add(this.ConnStrGRP);
			this.ControlPNL.Controls.Add(this.CloseBTN);
			this.ControlPNL.Controls.Add(this.OpenBTN);
			this.ControlPNL.Controls.Add(this.TestBTN);
			this.ControlPNL.Controls.Add(this.ConnectionInformationGRP);
			this.ControlPNL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ControlPNL.Location = new System.Drawing.Point(0, 0);
			this.ControlPNL.Name = "ControlPNL";
			this.ControlPNL.Size = new System.Drawing.Size(342, 271);
			this.ControlPNL.TabIndex = 12;
			// 
			// ShowPasswordCHK
			// 
			this.ShowPasswordCHK.Enabled = false;
			this.ShowPasswordCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ShowPasswordCHK.Location = new System.Drawing.Point(180, 212);
			this.ShowPasswordCHK.Name = "ShowPasswordCHK";
			this.ShowPasswordCHK.TabIndex = 16;
			this.ShowPasswordCHK.Text = "Show Password";
			// 
			// ConnStrGRP
			// 
			this.ConnStrGRP.Controls.Add(this.ConnStrTXT);
			this.ConnStrGRP.Location = new System.Drawing.Point(8, 136);
			this.ConnStrGRP.Name = "ConnStrGRP";
			this.ConnStrGRP.Size = new System.Drawing.Size(328, 76);
			this.ConnStrGRP.TabIndex = 15;
			this.ConnStrGRP.TabStop = false;
			this.ConnStrGRP.Text = "Connection String:";
			// 
			// ConnStrTXT
			// 
			this.ConnStrTXT.BackColor = System.Drawing.SystemColors.Window;
			this.ConnStrTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ConnStrTXT.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ConnStrTXT.Enabled = false;
			this.ConnStrTXT.Location = new System.Drawing.Point(3, 16);
			this.ConnStrTXT.Multiline = true;
			this.ConnStrTXT.Name = "ConnStrTXT";
			this.ConnStrTXT.Size = new System.Drawing.Size(322, 57);
			this.ConnStrTXT.TabIndex = 0;
			this.ConnStrTXT.Text = "";
			// 
			// CloseBTN
			// 
			this.CloseBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CloseBTN.Location = new System.Drawing.Point(180, 236);
			this.CloseBTN.Name = "CloseBTN";
			this.CloseBTN.Size = new System.Drawing.Size(24, 24);
			this.CloseBTN.TabIndex = 14;
			this.CloseBTN.Text = "x";
			// 
			// OpenBTN
			// 
			this.OpenBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OpenBTN.Location = new System.Drawing.Point(272, 236);
			this.OpenBTN.Name = "OpenBTN";
			this.OpenBTN.Size = new System.Drawing.Size(60, 24);
			this.OpenBTN.TabIndex = 13;
			this.OpenBTN.Text = "&Open";
			// 
			// TestBTN
			// 
			this.TestBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TestBTN.Location = new System.Drawing.Point(208, 236);
			this.TestBTN.Name = "TestBTN";
			this.TestBTN.Size = new System.Drawing.Size(60, 24);
			this.TestBTN.TabIndex = 12;
			this.TestBTN.Text = "&Test";
			// 
			// ConnectionInformationGRP
			// 
			this.ConnectionInformationGRP.Controls.Add(this.IntergratedSecurityCHK);
			this.ConnectionInformationGRP.Controls.Add(this.ServerTXT);
			this.ConnectionInformationGRP.Controls.Add(this.ServerLBL);
			this.ConnectionInformationGRP.Controls.Add(this.UserPNL);
			this.ConnectionInformationGRP.Location = new System.Drawing.Point(4, 8);
			this.ConnectionInformationGRP.Name = "ConnectionInformationGRP";
			this.ConnectionInformationGRP.Size = new System.Drawing.Size(332, 120);
			this.ConnectionInformationGRP.TabIndex = 11;
			this.ConnectionInformationGRP.TabStop = false;
			this.ConnectionInformationGRP.Text = "Connection Information";
			// 
			// IntergratedSecurityCHK
			// 
			this.IntergratedSecurityCHK.Checked = true;
			this.IntergratedSecurityCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.IntergratedSecurityCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.IntergratedSecurityCHK.Location = new System.Drawing.Point(116, 40);
			this.IntergratedSecurityCHK.Name = "IntergratedSecurityCHK";
			this.IntergratedSecurityCHK.Size = new System.Drawing.Size(120, 24);
			this.IntergratedSecurityCHK.TabIndex = 13;
			this.IntergratedSecurityCHK.Text = "Integrated Security";
			// 
			// ServerTXT
			// 
			this.ServerTXT.BackColor = System.Drawing.SystemColors.Window;
			this.ServerTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ServerTXT.Location = new System.Drawing.Point(56, 16);
			this.ServerTXT.Name = "ServerTXT";
			this.ServerTXT.Size = new System.Drawing.Size(272, 20);
			this.ServerTXT.TabIndex = 11;
			this.ServerTXT.Text = "";
			// 
			// ServerLBL
			// 
			this.ServerLBL.AutoSize = true;
			this.ServerLBL.Location = new System.Drawing.Point(16, 20);
			this.ServerLBL.Name = "ServerLBL";
			this.ServerLBL.Size = new System.Drawing.Size(41, 16);
			this.ServerLBL.TabIndex = 9;
			this.ServerLBL.Text = "Server:";
			// 
			// UserPNL
			// 
			this.UserPNL.Controls.Add(this.PasswordTXT);
			this.UserPNL.Controls.Add(this.UserTXT);
			this.UserPNL.Controls.Add(this.PasswordLBL);
			this.UserPNL.Controls.Add(this.UserLBL);
			this.UserPNL.Enabled = false;
			this.UserPNL.Location = new System.Drawing.Point(3, 68);
			this.UserPNL.Name = "UserPNL";
			this.UserPNL.Size = new System.Drawing.Size(321, 48);
			this.UserPNL.TabIndex = 18;
			// 
			// PasswordTXT
			// 
			this.PasswordTXT.BackColor = System.Drawing.SystemColors.Window;
			this.PasswordTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PasswordTXT.Location = new System.Drawing.Point(112, 24);
			this.PasswordTXT.Name = "PasswordTXT";
			this.PasswordTXT.Size = new System.Drawing.Size(208, 20);
			this.PasswordTXT.TabIndex = 21;
			this.PasswordTXT.Text = "";
			// 
			// UserTXT
			// 
			this.UserTXT.BackColor = System.Drawing.SystemColors.Window;
			this.UserTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.UserTXT.Location = new System.Drawing.Point(112, 0);
			this.UserTXT.Name = "UserTXT";
			this.UserTXT.Size = new System.Drawing.Size(208, 20);
			this.UserTXT.TabIndex = 20;
			this.UserTXT.Text = "";
			// 
			// PasswordLBL
			// 
			this.PasswordLBL.AutoSize = true;
			this.PasswordLBL.Location = new System.Drawing.Point(52, 28);
			this.PasswordLBL.Name = "PasswordLBL";
			this.PasswordLBL.Size = new System.Drawing.Size(57, 16);
			this.PasswordLBL.TabIndex = 19;
			this.PasswordLBL.Text = "Password:";
			// 
			// UserLBL
			// 
			this.UserLBL.AutoSize = true;
			this.UserLBL.Location = new System.Drawing.Point(76, 4);
			this.UserLBL.Name = "UserLBL";
			this.UserLBL.Size = new System.Drawing.Size(31, 16);
			this.UserLBL.TabIndex = 18;
			this.UserLBL.Text = "User:";
			// 
			// AddServer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(342, 271);
			this.Controls.Add(this.ControlPNL);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AddServer";
			this.Text = "Add a Server";
			this.ControlPNL.ResumeLayout(false);
			this.ConnStrGRP.ResumeLayout(false);
			this.ConnectionInformationGRP.ResumeLayout(false);
			this.UserPNL.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
