using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


using SqlServer.Companion.Windows;

namespace SqlServer.Companion
{
	/// <summary>
	/// Summary description for MDIMain.
	/// </summary>
	public class MDIMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel pnlMenuToolbar;
		private System.Windows.Forms.Panel pnlStatus;
		private System.Windows.Forms.Panel pnlToolbox;
		private System.Windows.Forms.Panel pnlToolboxTop;
		private System.Windows.Forms.Panel pnlToolbaxBottom;
		private System.Windows.Forms.Panel pnlToolboxTree;
		private System.Windows.Forms.TreeView DbTree;
		private System.Windows.Forms.MainMenu MainMenu;
		private System.Windows.Forms.ToolBar CommonTB;
		private System.Windows.Forms.ToolTip TipProvider;
		private System.Windows.Forms.ImageList CommonToolBarImgs;
		private System.Windows.Forms.ImageList DataImgs;
		private System.Windows.Forms.ToolBarButton AddServerBTN;
		private System.Windows.Forms.Label ServerLBL;
		private System.Windows.Forms.ToolBarButton GenerateTableBTN;
		private System.Windows.Forms.MenuItem WindowMNU;
		private System.Windows.Forms.MenuItem FileMNU;
		private System.Windows.Forms.MenuItem HelpMNU;
		private System.Windows.Forms.MenuItem HelpAboutMI;
		private System.Windows.Forms.MenuItem LanguageMNU;
		private System.Windows.Forms.MenuItem FileAddServerMI;
		private System.Windows.Forms.MenuItem FileSep01MI;
		private System.Windows.Forms.MenuItem FileExitMI;
		private System.Windows.Forms.MenuItem CSharpMI;
		private System.Windows.Forms.MenuItem VBNetMI;
		private System.Windows.Forms.MenuItem WindowsMinimizeMI;
		private System.Windows.Forms.MenuItem WindowsSep01MI;
		private System.Windows.Forms.MenuItem WindowsCascadeMI;
		private System.Windows.Forms.MenuItem WindowsMinDocumentsMI;
		private System.Windows.Forms.MenuItem AddFormTestMI;
		private System.Windows.Forms.ToolBarButton GenerateDbBatchBTN;
		private System.Windows.Forms.ToolBarButton RefreshTreeBTN;
		private System.Windows.Forms.MenuItem FileOpenProjectMI;
		private System.Windows.Forms.MenuItem FileNewProjectMI;
		private System.Windows.Forms.MenuItem FileSep02MI;
		private System.Windows.Forms.MenuItem FileSaveProjectMI;
		private System.Windows.Forms.MenuItem FileSep03MI;
		private System.ComponentModel.IContainer components;

		public MDIMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Width = 1024;
			this.Height = 768;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Servers");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIMain));
            this.pnlMenuToolbar = new System.Windows.Forms.Panel();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.FileMNU = new System.Windows.Forms.MenuItem();
            this.FileOpenProjectMI = new System.Windows.Forms.MenuItem();
            this.FileNewProjectMI = new System.Windows.Forms.MenuItem();
            this.FileSep01MI = new System.Windows.Forms.MenuItem();
            this.FileAddServerMI = new System.Windows.Forms.MenuItem();
            this.FileSep02MI = new System.Windows.Forms.MenuItem();
            this.FileSaveProjectMI = new System.Windows.Forms.MenuItem();
            this.FileSep03MI = new System.Windows.Forms.MenuItem();
            this.FileExitMI = new System.Windows.Forms.MenuItem();
            this.LanguageMNU = new System.Windows.Forms.MenuItem();
            this.CSharpMI = new System.Windows.Forms.MenuItem();
            this.VBNetMI = new System.Windows.Forms.MenuItem();
            this.WindowMNU = new System.Windows.Forms.MenuItem();
            this.WindowsMinimizeMI = new System.Windows.Forms.MenuItem();
            this.WindowsSep01MI = new System.Windows.Forms.MenuItem();
            this.WindowsCascadeMI = new System.Windows.Forms.MenuItem();
            this.WindowsMinDocumentsMI = new System.Windows.Forms.MenuItem();
            this.HelpMNU = new System.Windows.Forms.MenuItem();
            this.HelpAboutMI = new System.Windows.Forms.MenuItem();
            this.AddFormTestMI = new System.Windows.Forms.MenuItem();
            this.pnlToolbox = new System.Windows.Forms.Panel();
            this.pnlToolboxTree = new System.Windows.Forms.Panel();
            this.DbTree = new System.Windows.Forms.TreeView();
            this.DataImgs = new System.Windows.Forms.ImageList(this.components);
            this.pnlToolbaxBottom = new System.Windows.Forms.Panel();
            this.CommonTB = new System.Windows.Forms.ToolBar();
            this.AddServerBTN = new System.Windows.Forms.ToolBarButton();
            this.GenerateTableBTN = new System.Windows.Forms.ToolBarButton();
            this.GenerateDbBatchBTN = new System.Windows.Forms.ToolBarButton();
            this.RefreshTreeBTN = new System.Windows.Forms.ToolBarButton();
            this.CommonToolBarImgs = new System.Windows.Forms.ImageList(this.components);
            this.pnlToolboxTop = new System.Windows.Forms.Panel();
            this.ServerLBL = new System.Windows.Forms.Label();
            this.TipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.pnlToolbox.SuspendLayout();
            this.pnlToolboxTree.SuspendLayout();
            this.pnlToolbaxBottom.SuspendLayout();
            this.pnlToolboxTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenuToolbar
            // 
            this.pnlMenuToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenuToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlMenuToolbar.Name = "pnlMenuToolbar";
            this.pnlMenuToolbar.Size = new System.Drawing.Size(592, 8);
            this.pnlMenuToolbar.TabIndex = 2;
            // 
            // pnlStatus
            // 
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 318);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(592, 24);
            this.pnlStatus.TabIndex = 3;
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileMNU,
            this.LanguageMNU,
            this.WindowMNU,
            this.HelpMNU,
            this.AddFormTestMI});
            // 
            // FileMNU
            // 
            this.FileMNU.Index = 0;
            this.FileMNU.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileOpenProjectMI,
            this.FileNewProjectMI,
            this.FileSep01MI,
            this.FileAddServerMI,
            this.FileSep02MI,
            this.FileSaveProjectMI,
            this.FileSep03MI,
            this.FileExitMI});
            this.FileMNU.Text = "&File";
            // 
            // FileOpenProjectMI
            // 
            this.FileOpenProjectMI.Index = 0;
            this.FileOpenProjectMI.Text = "&Open Project";
            // 
            // FileNewProjectMI
            // 
            this.FileNewProjectMI.Index = 1;
            this.FileNewProjectMI.Text = "&New Project";
            this.FileNewProjectMI.Click += new System.EventHandler(this.FileNewProjectMI_Click);
            // 
            // FileSep01MI
            // 
            this.FileSep01MI.Index = 2;
            this.FileSep01MI.Text = "-";
            // 
            // FileAddServerMI
            // 
            this.FileAddServerMI.Index = 3;
            this.FileAddServerMI.Text = "&Add Server";
            // 
            // FileSep02MI
            // 
            this.FileSep02MI.Index = 4;
            this.FileSep02MI.Text = "-";
            // 
            // FileSaveProjectMI
            // 
            this.FileSaveProjectMI.Index = 5;
            this.FileSaveProjectMI.Text = "&Save Project";
            // 
            // FileSep03MI
            // 
            this.FileSep03MI.Index = 6;
            this.FileSep03MI.Text = "-";
            // 
            // FileExitMI
            // 
            this.FileExitMI.Index = 7;
            this.FileExitMI.Text = "E&xit";
            // 
            // LanguageMNU
            // 
            this.LanguageMNU.Index = 1;
            this.LanguageMNU.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CSharpMI,
            this.VBNetMI});
            this.LanguageMNU.Text = "Language";
            // 
            // CSharpMI
            // 
            this.CSharpMI.Checked = true;
            this.CSharpMI.Index = 0;
            this.CSharpMI.Text = "C#";
            // 
            // VBNetMI
            // 
            this.VBNetMI.Index = 1;
            this.VBNetMI.Text = "VB.Net";
            // 
            // WindowMNU
            // 
            this.WindowMNU.Index = 2;
            this.WindowMNU.MdiList = true;
            this.WindowMNU.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.WindowsMinimizeMI,
            this.WindowsSep01MI,
            this.WindowsCascadeMI,
            this.WindowsMinDocumentsMI});
            this.WindowMNU.Text = "Windows";
            // 
            // WindowsMinimizeMI
            // 
            this.WindowsMinimizeMI.Index = 0;
            this.WindowsMinimizeMI.Text = "Minimize";
            // 
            // WindowsSep01MI
            // 
            this.WindowsSep01MI.Index = 1;
            this.WindowsSep01MI.Text = "-";
            // 
            // WindowsCascadeMI
            // 
            this.WindowsCascadeMI.Index = 2;
            this.WindowsCascadeMI.Text = "Cascade Documents";
            // 
            // WindowsMinDocumentsMI
            // 
            this.WindowsMinDocumentsMI.Index = 3;
            this.WindowsMinDocumentsMI.Text = "Minimize Documents";
            // 
            // HelpMNU
            // 
            this.HelpMNU.Index = 3;
            this.HelpMNU.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.HelpAboutMI});
            this.HelpMNU.Text = "&Help";
            // 
            // HelpAboutMI
            // 
            this.HelpAboutMI.Index = 0;
            this.HelpAboutMI.Text = "About SqlServer Companion v3.28";
            // 
            // AddFormTestMI
            // 
            this.AddFormTestMI.Index = 4;
            this.AddFormTestMI.Text = "Add Form Test";
            this.AddFormTestMI.Visible = false;
            this.AddFormTestMI.Click += new System.EventHandler(this.AddFormTestMI_Click);
            // 
            // pnlToolbox
            // 
            this.pnlToolbox.Controls.Add(this.pnlToolboxTree);
            this.pnlToolbox.Controls.Add(this.pnlToolbaxBottom);
            this.pnlToolbox.Controls.Add(this.pnlToolboxTop);
            this.pnlToolbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlToolbox.Location = new System.Drawing.Point(0, 8);
            this.pnlToolbox.Name = "pnlToolbox";
            this.pnlToolbox.Size = new System.Drawing.Size(276, 310);
            this.pnlToolbox.TabIndex = 5;
            // 
            // pnlToolboxTree
            // 
            this.pnlToolboxTree.Controls.Add(this.DbTree);
            this.pnlToolboxTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolboxTree.Location = new System.Drawing.Point(0, 32);
            this.pnlToolboxTree.Name = "pnlToolboxTree";
            this.pnlToolboxTree.Padding = new System.Windows.Forms.Padding(4);
            this.pnlToolboxTree.Size = new System.Drawing.Size(276, 230);
            this.pnlToolboxTree.TabIndex = 2;
            // 
            // DbTree
            // 
            this.DbTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbTree.ImageIndex = 0;
            this.DbTree.ImageList = this.DataImgs;
            this.DbTree.Location = new System.Drawing.Point(4, 4);
            this.DbTree.Name = "DbTree";
            treeNode1.Name = "";
            treeNode1.Text = "Servers";
            this.DbTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.DbTree.SelectedImageIndex = 0;
            this.DbTree.Size = new System.Drawing.Size(268, 222);
            this.DbTree.TabIndex = 0;
            // 
            // DataImgs
            // 
            this.DataImgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("DataImgs.ImageStream")));
            this.DataImgs.TransparentColor = System.Drawing.Color.Transparent;
            this.DataImgs.Images.SetKeyName(0, "");
            this.DataImgs.Images.SetKeyName(1, "");
            this.DataImgs.Images.SetKeyName(2, "");
            this.DataImgs.Images.SetKeyName(3, "");
            this.DataImgs.Images.SetKeyName(4, "");
            // 
            // pnlToolbaxBottom
            // 
            this.pnlToolbaxBottom.Controls.Add(this.CommonTB);
            this.pnlToolbaxBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlToolbaxBottom.Location = new System.Drawing.Point(0, 262);
            this.pnlToolbaxBottom.Name = "pnlToolbaxBottom";
            this.pnlToolbaxBottom.Size = new System.Drawing.Size(276, 48);
            this.pnlToolbaxBottom.TabIndex = 1;
            // 
            // CommonTB
            // 
            this.CommonTB.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.CommonTB.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.AddServerBTN,
            this.GenerateTableBTN,
            this.GenerateDbBatchBTN,
            this.RefreshTreeBTN});
            this.CommonTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommonTB.DropDownArrows = true;
            this.CommonTB.ImageList = this.CommonToolBarImgs;
            this.CommonTB.Location = new System.Drawing.Point(0, 0);
            this.CommonTB.Name = "CommonTB";
            this.CommonTB.ShowToolTips = true;
            this.CommonTB.Size = new System.Drawing.Size(276, 42);
            this.CommonTB.TabIndex = 0;
            // 
            // AddServerBTN
            // 
            this.AddServerBTN.ImageIndex = 5;
            this.AddServerBTN.Name = "AddServerBTN";
            this.AddServerBTN.Text = "Add Server";
            this.AddServerBTN.ToolTipText = "Add a SqlServer to the treeview.";
            // 
            // GenerateTableBTN
            // 
            this.GenerateTableBTN.ImageIndex = 4;
            this.GenerateTableBTN.Name = "GenerateTableBTN";
            this.GenerateTableBTN.Text = "Generate";
            this.GenerateTableBTN.ToolTipText = "Generate Data Classes and Stored Procedures based on the selected table.";
            // 
            // GenerateDbBatchBTN
            // 
            this.GenerateDbBatchBTN.ImageIndex = 2;
            this.GenerateDbBatchBTN.Name = "GenerateDbBatchBTN";
            this.GenerateDbBatchBTN.Text = "Batch Generate";
            this.GenerateDbBatchBTN.ToolTipText = "Run Automatic Generation for every table.";
            // 
            // RefreshTreeBTN
            // 
            this.RefreshTreeBTN.ImageIndex = 6;
            this.RefreshTreeBTN.Name = "RefreshTreeBTN";
            this.RefreshTreeBTN.Text = "Refresh";
            this.RefreshTreeBTN.ToolTipText = "Refresh View.";
            // 
            // CommonToolBarImgs
            // 
            this.CommonToolBarImgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CommonToolBarImgs.ImageStream")));
            this.CommonToolBarImgs.TransparentColor = System.Drawing.Color.Transparent;
            this.CommonToolBarImgs.Images.SetKeyName(0, "");
            this.CommonToolBarImgs.Images.SetKeyName(1, "");
            this.CommonToolBarImgs.Images.SetKeyName(2, "");
            this.CommonToolBarImgs.Images.SetKeyName(3, "");
            this.CommonToolBarImgs.Images.SetKeyName(4, "");
            this.CommonToolBarImgs.Images.SetKeyName(5, "");
            this.CommonToolBarImgs.Images.SetKeyName(6, "");
            this.CommonToolBarImgs.Images.SetKeyName(7, "");
            this.CommonToolBarImgs.Images.SetKeyName(8, "");
            // 
            // pnlToolboxTop
            // 
            this.pnlToolboxTop.Controls.Add(this.ServerLBL);
            this.pnlToolboxTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolboxTop.Location = new System.Drawing.Point(0, 0);
            this.pnlToolboxTop.Name = "pnlToolboxTop";
            this.pnlToolboxTop.Size = new System.Drawing.Size(276, 32);
            this.pnlToolboxTop.TabIndex = 0;
            // 
            // ServerLBL
            // 
            this.ServerLBL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerLBL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ServerLBL.ImageIndex = 3;
            this.ServerLBL.ImageList = this.CommonToolBarImgs;
            this.ServerLBL.Location = new System.Drawing.Point(0, 0);
            this.ServerLBL.Name = "ServerLBL";
            this.ServerLBL.Size = new System.Drawing.Size(276, 32);
            this.ServerLBL.TabIndex = 0;
            this.ServerLBL.Text = "       Servers";
            this.ServerLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MDIMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(592, 342);
            this.Controls.Add(this.pnlToolbox);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlMenuToolbar);
            this.IsMdiContainer = true;
            this.Menu = this.MainMenu;
            this.Name = "MDIMain";
            this.Text = "SqlServer Companion v3.07";
            this.pnlToolbox.ResumeLayout(false);
            this.pnlToolboxTree.ResumeLayout(false);
            this.pnlToolbaxBottom.ResumeLayout(false);
            this.pnlToolbaxBottom.PerformLayout();
            this.pnlToolboxTop.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void AddFormTestMI_Click(object sender, System.EventArgs e)
		{
			Windows.Generator gn = new SqlServer.Companion.Windows.Generator();

			gn.MdiParent = this;
		

			gn.Show();
		}

		private void FileNewProjectMI_Click(object sender, System.EventArgs e)
		{
			Windows.NewProject prj = new NewProject();
			prj.MdiParent = this;
			prj.Show();
		}

	}
}
