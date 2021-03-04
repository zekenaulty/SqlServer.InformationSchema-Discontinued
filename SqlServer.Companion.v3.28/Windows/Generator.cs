using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlServer.Companion.Windows
{
	/// <summary>
	/// Summary description for Generator.
	/// </summary>
	public class Generator : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl ObjectGenTB;
		private System.Windows.Forms.Panel CtrlPNL;
		private System.Windows.Forms.TabPage DirPathTBP;
		private System.Windows.Forms.TabPage QueryTBP;
		private System.Windows.Forms.TabPage OneToOneTBP;
		private System.Windows.Forms.TabPage OneToManyTBP;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Generator()
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
			this.ObjectGenTB = new System.Windows.Forms.TabControl();
			this.DirPathTBP = new System.Windows.Forms.TabPage();
			this.QueryTBP = new System.Windows.Forms.TabPage();
			this.OneToOneTBP = new System.Windows.Forms.TabPage();
			this.OneToManyTBP = new System.Windows.Forms.TabPage();
			this.CtrlPNL = new System.Windows.Forms.Panel();
			this.ObjectGenTB.SuspendLayout();
			this.SuspendLayout();
			// 
			// ObjectGenTB
			// 
			this.ObjectGenTB.Controls.Add(this.DirPathTBP);
			this.ObjectGenTB.Controls.Add(this.OneToOneTBP);
			this.ObjectGenTB.Controls.Add(this.QueryTBP);
			this.ObjectGenTB.Controls.Add(this.OneToManyTBP);
			this.ObjectGenTB.Location = new System.Drawing.Point(4, 4);
			this.ObjectGenTB.Name = "ObjectGenTB";
			this.ObjectGenTB.Padding = new System.Drawing.Point(12, 12);
			this.ObjectGenTB.SelectedIndex = 0;
			this.ObjectGenTB.Size = new System.Drawing.Size(572, 436);
			this.ObjectGenTB.TabIndex = 0;
			// 
			// DirPathTBP
			// 
			this.DirPathTBP.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.DirPathTBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DirPathTBP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DirPathTBP.Location = new System.Drawing.Point(4, 22);
			this.DirPathTBP.Name = "DirPathTBP";
			this.DirPathTBP.Size = new System.Drawing.Size(564, 410);
			this.DirPathTBP.TabIndex = 0;
			this.DirPathTBP.Text = "   Directory/Path Information   ";
			// 
			// QueryTBP
			// 
			this.QueryTBP.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.QueryTBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.QueryTBP.Location = new System.Drawing.Point(4, 22);
			this.QueryTBP.Name = "QueryTBP";
			this.QueryTBP.Size = new System.Drawing.Size(575, 422);
			this.QueryTBP.TabIndex = 1;
			this.QueryTBP.Text = "   Queries   ";
			// 
			// OneToOneTBP
			// 
			this.OneToOneTBP.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.OneToOneTBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OneToOneTBP.Location = new System.Drawing.Point(4, 22);
			this.OneToOneTBP.Name = "OneToOneTBP";
			this.OneToOneTBP.Size = new System.Drawing.Size(575, 422);
			this.OneToOneTBP.TabIndex = 2;
			this.OneToOneTBP.Text = "   1 to 1 Object/Table Relations   ";
			// 
			// OneToManyTBP
			// 
			this.OneToManyTBP.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.OneToManyTBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OneToManyTBP.Location = new System.Drawing.Point(4, 22);
			this.OneToManyTBP.Name = "OneToManyTBP";
			this.OneToManyTBP.Size = new System.Drawing.Size(575, 422);
			this.OneToManyTBP.TabIndex = 3;
			this.OneToManyTBP.Text = "   1 to * Object/Table Relations   ";
			// 
			// CtrlPNL
			// 
			this.CtrlPNL.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.CtrlPNL.Location = new System.Drawing.Point(4, 444);
			this.CtrlPNL.Name = "CtrlPNL";
			this.CtrlPNL.Size = new System.Drawing.Size(574, 48);
			this.CtrlPNL.TabIndex = 1;
			// 
			// Generator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(582, 496);
			this.Controls.Add(this.CtrlPNL);
			this.Controls.Add(this.ObjectGenTB);
			this.DockPadding.All = 4;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Generator";
			this.Text = "Object\\Stored Procedure Generator [%tbl%]";
			this.ObjectGenTB.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
