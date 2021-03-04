using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlServer.Companion.Windows
{
	/// <summary>
	/// Summary description for NewProject.
	/// </summary>
	public class NewProject : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label Instructions01LBL;
		private System.Windows.Forms.GroupBox PathsGRP;
		private System.Windows.Forms.TextBox WorkDirTXT;
		private System.Windows.Forms.Button WorkingDirBtn;
		private System.Windows.Forms.TextBox DataDirTXT;
		private System.Windows.Forms.TextBox BusinessDirTXT;
		private System.Windows.Forms.TextBox SqlDirTXT;
		private System.Windows.Forms.GroupBox NamespaceGRP;
		private System.Windows.Forms.CheckBox UseFoldersCHK;
		private System.Windows.Forms.TextBox BusinessNsTXT;
		private System.Windows.Forms.TextBox DataNsTXT;
		private System.Windows.Forms.Button CreateBTN;
		private System.Windows.Forms.Button CancelBTN;
		private System.Windows.Forms.FolderBrowserDialog BrowseFDLG;
		private System.Windows.Forms.Label SqlDirLBL;
		private System.Windows.Forms.Label label3BusinessDirLBL;
		private System.Windows.Forms.Label DataDirLBL;
		private System.Windows.Forms.Label WorkingDirLBL;
		private System.Windows.Forms.Label BusinessNsLBL;
		private System.Windows.Forms.Label DataNsLBL;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewProject()
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
			this.Instructions01LBL = new System.Windows.Forms.Label();
			this.PathsGRP = new System.Windows.Forms.GroupBox();
			this.SqlDirTXT = new System.Windows.Forms.TextBox();
			this.SqlDirLBL = new System.Windows.Forms.Label();
			this.BusinessDirTXT = new System.Windows.Forms.TextBox();
			this.label3BusinessDirLBL = new System.Windows.Forms.Label();
			this.DataDirTXT = new System.Windows.Forms.TextBox();
			this.DataDirLBL = new System.Windows.Forms.Label();
			this.WorkingDirBtn = new System.Windows.Forms.Button();
			this.WorkDirTXT = new System.Windows.Forms.TextBox();
			this.WorkingDirLBL = new System.Windows.Forms.Label();
			this.NamespaceGRP = new System.Windows.Forms.GroupBox();
			this.BusinessNsTXT = new System.Windows.Forms.TextBox();
			this.BusinessNsLBL = new System.Windows.Forms.Label();
			this.DataNsTXT = new System.Windows.Forms.TextBox();
			this.DataNsLBL = new System.Windows.Forms.Label();
			this.UseFoldersCHK = new System.Windows.Forms.CheckBox();
			this.CreateBTN = new System.Windows.Forms.Button();
			this.CancelBTN = new System.Windows.Forms.Button();
			this.PathsGRP.SuspendLayout();
			this.NamespaceGRP.SuspendLayout();
			this.SuspendLayout();
			// 
			// Instructions01LBL
			// 
			this.Instructions01LBL.Location = new System.Drawing.Point(3, -4);
			this.Instructions01LBL.Name = "Instructions01LBL";
			this.Instructions01LBL.Size = new System.Drawing.Size(444, 36);
			this.Instructions01LBL.TabIndex = 0;
			this.Instructions01LBL.Text = "Complete the following information to create a new project.";
			// 
			// PathsGRP
			// 
			this.PathsGRP.Controls.Add(this.SqlDirTXT);
			this.PathsGRP.Controls.Add(this.SqlDirLBL);
			this.PathsGRP.Controls.Add(this.BusinessDirTXT);
			this.PathsGRP.Controls.Add(this.label3BusinessDirLBL);
			this.PathsGRP.Controls.Add(this.DataDirTXT);
			this.PathsGRP.Controls.Add(this.DataDirLBL);
			this.PathsGRP.Controls.Add(this.WorkingDirBtn);
			this.PathsGRP.Controls.Add(this.WorkDirTXT);
			this.PathsGRP.Controls.Add(this.WorkingDirLBL);
			this.PathsGRP.Location = new System.Drawing.Point(4, 20);
			this.PathsGRP.Name = "PathsGRP";
			this.PathsGRP.Size = new System.Drawing.Size(420, 116);
			this.PathsGRP.TabIndex = 1;
			this.PathsGRP.TabStop = false;
			this.PathsGRP.Text = "Paths:";
			// 
			// SqlDirTXT
			// 
			this.SqlDirTXT.Location = new System.Drawing.Point(112, 88);
			this.SqlDirTXT.Name = "SqlDirTXT";
			this.SqlDirTXT.Size = new System.Drawing.Size(300, 20);
			this.SqlDirTXT.TabIndex = 9;
			this.SqlDirTXT.Text = "SQL\\";
			// 
			// SqlDirLBL
			// 
			this.SqlDirLBL.AutoSize = true;
			this.SqlDirLBL.Location = new System.Drawing.Point(40, 92);
			this.SqlDirLBL.Name = "SqlDirLBL";
			this.SqlDirLBL.Size = new System.Drawing.Size(72, 16);
			this.SqlDirLBL.TabIndex = 8;
			this.SqlDirLBL.Text = "Sql Directory:";
			// 
			// BusinessDirTXT
			// 
			this.BusinessDirTXT.Location = new System.Drawing.Point(112, 64);
			this.BusinessDirTXT.Name = "BusinessDirTXT";
			this.BusinessDirTXT.Size = new System.Drawing.Size(300, 20);
			this.BusinessDirTXT.TabIndex = 7;
			this.BusinessDirTXT.Text = "Business\\";
			// 
			// label3BusinessDirLBL
			// 
			this.label3BusinessDirLBL.AutoSize = true;
			this.label3BusinessDirLBL.Location = new System.Drawing.Point(8, 68);
			this.label3BusinessDirLBL.Name = "label3BusinessDirLBL";
			this.label3BusinessDirLBL.Size = new System.Drawing.Size(102, 16);
			this.label3BusinessDirLBL.TabIndex = 6;
			this.label3BusinessDirLBL.Text = "Buisness Directory:";
			// 
			// DataDirTXT
			// 
			this.DataDirTXT.Location = new System.Drawing.Point(112, 40);
			this.DataDirTXT.Name = "DataDirTXT";
			this.DataDirTXT.Size = new System.Drawing.Size(300, 20);
			this.DataDirTXT.TabIndex = 4;
			this.DataDirTXT.Text = "Data\\";
			// 
			// DataDirLBL
			// 
			this.DataDirLBL.AutoSize = true;
			this.DataDirLBL.Location = new System.Drawing.Point(32, 44);
			this.DataDirLBL.Name = "DataDirLBL";
			this.DataDirLBL.Size = new System.Drawing.Size(80, 16);
			this.DataDirLBL.TabIndex = 3;
			this.DataDirLBL.Text = "Data Directory:";
			// 
			// WorkingDirBtn
			// 
			this.WorkingDirBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.WorkingDirBtn.Location = new System.Drawing.Point(384, 16);
			this.WorkingDirBtn.Name = "WorkingDirBtn";
			this.WorkingDirBtn.Size = new System.Drawing.Size(28, 20);
			this.WorkingDirBtn.TabIndex = 2;
			this.WorkingDirBtn.Text = "...";
			this.WorkingDirBtn.Click += new System.EventHandler(this.WorkingDirBtn_Click);
			// 
			// WorkDirTXT
			// 
			this.WorkDirTXT.Location = new System.Drawing.Point(112, 16);
			this.WorkDirTXT.Name = "WorkDirTXT";
			this.WorkDirTXT.Size = new System.Drawing.Size(272, 20);
			this.WorkDirTXT.TabIndex = 1;
			this.WorkDirTXT.Text = "";
			// 
			// WorkingDirLBL
			// 
			this.WorkingDirLBL.AutoSize = true;
			this.WorkingDirLBL.Location = new System.Drawing.Point(12, 20);
			this.WorkingDirLBL.Name = "WorkingDirLBL";
			this.WorkingDirLBL.Size = new System.Drawing.Size(97, 16);
			this.WorkingDirLBL.TabIndex = 0;
			this.WorkingDirLBL.Text = "Working Directory:";
			// 
			// NamespaceGRP
			// 
			this.NamespaceGRP.Controls.Add(this.BusinessNsTXT);
			this.NamespaceGRP.Controls.Add(this.BusinessNsLBL);
			this.NamespaceGRP.Controls.Add(this.DataNsTXT);
			this.NamespaceGRP.Controls.Add(this.DataNsLBL);
			this.NamespaceGRP.Controls.Add(this.UseFoldersCHK);
			this.NamespaceGRP.Location = new System.Drawing.Point(4, 140);
			this.NamespaceGRP.Name = "NamespaceGRP";
			this.NamespaceGRP.Size = new System.Drawing.Size(420, 100);
			this.NamespaceGRP.TabIndex = 2;
			this.NamespaceGRP.TabStop = false;
			this.NamespaceGRP.Text = "Namespaces:";
			// 
			// BusinessNsTXT
			// 
			this.BusinessNsTXT.Enabled = false;
			this.BusinessNsTXT.Location = new System.Drawing.Point(132, 68);
			this.BusinessNsTXT.Name = "BusinessNsTXT";
			this.BusinessNsTXT.Size = new System.Drawing.Size(252, 20);
			this.BusinessNsTXT.TabIndex = 11;
			this.BusinessNsTXT.Text = "Business";
			// 
			// BusinessNsLBL
			// 
			this.BusinessNsLBL.AutoSize = true;
			this.BusinessNsLBL.Location = new System.Drawing.Point(12, 72);
			this.BusinessNsLBL.Name = "BusinessNsLBL";
			this.BusinessNsLBL.Size = new System.Drawing.Size(117, 16);
			this.BusinessNsLBL.TabIndex = 10;
			this.BusinessNsLBL.Text = "Buisness Namespace:";
			// 
			// DataNsTXT
			// 
			this.DataNsTXT.Enabled = false;
			this.DataNsTXT.Location = new System.Drawing.Point(132, 44);
			this.DataNsTXT.Name = "DataNsTXT";
			this.DataNsTXT.Size = new System.Drawing.Size(252, 20);
			this.DataNsTXT.TabIndex = 9;
			this.DataNsTXT.Text = "Data";
			// 
			// DataNsLBL
			// 
			this.DataNsLBL.AutoSize = true;
			this.DataNsLBL.Location = new System.Drawing.Point(36, 48);
			this.DataNsLBL.Name = "DataNsLBL";
			this.DataNsLBL.Size = new System.Drawing.Size(95, 16);
			this.DataNsLBL.TabIndex = 8;
			this.DataNsLBL.Text = "Data Namespace:";
			// 
			// UseFoldersCHK
			// 
			this.UseFoldersCHK.Checked = true;
			this.UseFoldersCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.UseFoldersCHK.Location = new System.Drawing.Point(132, 20);
			this.UseFoldersCHK.Name = "UseFoldersCHK";
			this.UseFoldersCHK.Size = new System.Drawing.Size(216, 20);
			this.UseFoldersCHK.TabIndex = 0;
			this.UseFoldersCHK.Text = "Use paths for namespaces..?";
			// 
			// CreateBTN
			// 
			this.CreateBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CreateBTN.Location = new System.Drawing.Point(264, 252);
			this.CreateBTN.Name = "CreateBTN";
			this.CreateBTN.Size = new System.Drawing.Size(75, 28);
			this.CreateBTN.TabIndex = 3;
			this.CreateBTN.Text = "C&reate";
			// 
			// CancelBTN
			// 
			this.CancelBTN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CancelBTN.Location = new System.Drawing.Point(344, 252);
			this.CancelBTN.Name = "CancelBTN";
			this.CancelBTN.Size = new System.Drawing.Size(75, 28);
			this.CancelBTN.TabIndex = 4;
			this.CancelBTN.Text = "&Cancel";
			this.CancelBTN.Click += new System.EventHandler(this.CancelBTN_Click);
			// 
			// NewProject
			// 
			this.AcceptButton = this.CreateBTN;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.CancelBTN;
			this.ClientSize = new System.Drawing.Size(426, 288);
			this.Controls.Add(this.CancelBTN);
			this.Controls.Add(this.CreateBTN);
			this.Controls.Add(this.NamespaceGRP);
			this.Controls.Add(this.PathsGRP);
			this.Controls.Add(this.Instructions01LBL);
			this.DockPadding.All = 3;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "NewProject";
			this.Text = "New Project";
			this.PathsGRP.ResumeLayout(false);
			this.NamespaceGRP.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void CancelBTN_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}

		private void WorkingDirBtn_Click(object sender, System.EventArgs e)
		{
			BrowseFDLG = new FolderBrowserDialog();
			BrowseFDLG.SelectedPath = WorkDirTXT.Text;
			if(BrowseFDLG.ShowDialog(Common.MDI) == DialogResult.OK)
				WorkDirTXT.Text = BrowseFDLG.SelectedPath + "\\";

			//set tooltips for namespaces and working folders
		}
	}
}
