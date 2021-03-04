using System;
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using SqlServer.Companion;
using SqlServer.Companion.Schema;
using SqlServer.Companion.CodeDom;

namespace SqlServer.Companion.Enviorment.Windows
{
	/// <summary></summary>
	public class MDI : System.Windows.Forms.Form
	{
		#region Controls

		private System.Windows.Forms.Panel ExplorerPNL;
		private System.Windows.Forms.ToolTip TipProvider;
		private System.Windows.Forms.ImageList DataImgs;
		private System.Windows.Forms.MainMenu MainMenu;
		private System.Windows.Forms.MenuItem FileMenuMI;
		private System.Windows.Forms.MenuItem FileMenuExitMI;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem HelpMI;
		private System.Windows.Forms.MenuItem AboutMI;
		private System.Windows.Forms.Panel DbPNL;
		private System.Windows.Forms.Panel DbTreeDockPNL;
		private System.Windows.Forms.TreeView DbTV;
		private System.Windows.Forms.Panel DocumentTabDockPNL;
		private System.Windows.Forms.TabControl DocumentTC;
		private System.Windows.Forms.ToolBar CommonTB;
		private System.Windows.Forms.Panel CommonToolbarPNL;
		private System.Windows.Forms.ImageList CommonToolBarImgs;
		private System.Windows.Forms.MenuItem HideMI;
		private System.Windows.Forms.ToolBarButton GenTB_BTN;
		private System.Windows.Forms.MenuItem FileSeperator01MI;
		private System.Windows.Forms.MenuItem AddServerMI;
		private System.Windows.Forms.MenuItem FileSeperator02MI;
		private System.Windows.Forms.ToolBarButton HideTB_BTN;
		private System.Windows.Forms.ToolBarButton AddServerTB_BTN;
		private System.Windows.Forms.ToolBarButton ExitTB_BTN;
		private System.Windows.Forms.ToolBarButton Seperator01TB_BTN;
		private System.Windows.Forms.ToolBarButton Seperator02TB_BTN;
		private System.Windows.Forms.ContextMenu TreeCMNU;
		private System.Windows.Forms.MenuItem TreeGenerateCMI;
		private System.Windows.Forms.MenuItem TreeShowInsertCMI;
		private System.Windows.Forms.MenuItem TreeShowUpdateCMI;
		private System.Windows.Forms.MenuItem TreeShowDeleteCMI;
		private System.Windows.Forms.MenuItem TreeSeperator01CMI;
		private System.Windows.Forms.MenuItem LangCSharpMI;
		private System.Windows.Forms.MenuItem LangVbMI;
		
		#endregion
		#region Var
		Hashtable servers = new Hashtable();

		#endregion
		private System.Windows.Forms.MenuItem LanguageMI;
		private System.Windows.Forms.ToolBarButton AboutTB_BTN;
		private System.Windows.Forms.ToolBarButton RefreshTB_BTN;
		#region Construction
		TrayIcon ti = null;
		public MDI(TrayIcon TI)
		{
			ti = TI;
			InitializeComponent();
		}
		#endregion
		#region Dispose
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
		#endregion
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDI));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Servers", 0, 0);
            this.CommonToolbarPNL = new System.Windows.Forms.Panel();
            this.CommonTB = new System.Windows.Forms.ToolBar();
            this.AddServerTB_BTN = new System.Windows.Forms.ToolBarButton();
            this.GenTB_BTN = new System.Windows.Forms.ToolBarButton();
            this.RefreshTB_BTN = new System.Windows.Forms.ToolBarButton();
            this.Seperator01TB_BTN = new System.Windows.Forms.ToolBarButton();
            this.AboutTB_BTN = new System.Windows.Forms.ToolBarButton();
            this.HideTB_BTN = new System.Windows.Forms.ToolBarButton();
            this.ExitTB_BTN = new System.Windows.Forms.ToolBarButton();
            this.Seperator02TB_BTN = new System.Windows.Forms.ToolBarButton();
            this.CommonToolBarImgs = new System.Windows.Forms.ImageList(this.components);
            this.ExplorerPNL = new System.Windows.Forms.Panel();
            this.DbPNL = new System.Windows.Forms.Panel();
            this.DbTreeDockPNL = new System.Windows.Forms.Panel();
            this.DbTV = new System.Windows.Forms.TreeView();
            this.DataImgs = new System.Windows.Forms.ImageList(this.components);
            this.TipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.FileMenuMI = new System.Windows.Forms.MenuItem();
            this.HideMI = new System.Windows.Forms.MenuItem();
            this.FileSeperator01MI = new System.Windows.Forms.MenuItem();
            this.AddServerMI = new System.Windows.Forms.MenuItem();
            this.FileSeperator02MI = new System.Windows.Forms.MenuItem();
            this.FileMenuExitMI = new System.Windows.Forms.MenuItem();
            this.LanguageMI = new System.Windows.Forms.MenuItem();
            this.LangCSharpMI = new System.Windows.Forms.MenuItem();
            this.LangVbMI = new System.Windows.Forms.MenuItem();
            this.HelpMI = new System.Windows.Forms.MenuItem();
            this.AboutMI = new System.Windows.Forms.MenuItem();
            this.DocumentTabDockPNL = new System.Windows.Forms.Panel();
            this.DocumentTC = new System.Windows.Forms.TabControl();
            this.TreeCMNU = new System.Windows.Forms.ContextMenu();
            this.TreeGenerateCMI = new System.Windows.Forms.MenuItem();
            this.TreeSeperator01CMI = new System.Windows.Forms.MenuItem();
            this.TreeShowInsertCMI = new System.Windows.Forms.MenuItem();
            this.TreeShowUpdateCMI = new System.Windows.Forms.MenuItem();
            this.TreeShowDeleteCMI = new System.Windows.Forms.MenuItem();
            this.CommonToolbarPNL.SuspendLayout();
            this.ExplorerPNL.SuspendLayout();
            this.DbPNL.SuspendLayout();
            this.DbTreeDockPNL.SuspendLayout();
            this.DocumentTabDockPNL.SuspendLayout();
            this.SuspendLayout();
            // 
            // CommonToolbarPNL
            // 
            this.CommonToolbarPNL.Controls.Add(this.CommonTB);
            this.CommonToolbarPNL.Dock = System.Windows.Forms.DockStyle.Top;
            this.CommonToolbarPNL.Location = new System.Drawing.Point(0, 0);
            this.CommonToolbarPNL.Name = "CommonToolbarPNL";
            this.CommonToolbarPNL.Size = new System.Drawing.Size(1254, 74);
            this.CommonToolbarPNL.TabIndex = 0;
            // 
            // CommonTB
            // 
            this.CommonTB.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.CommonTB.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.AddServerTB_BTN,
            this.GenTB_BTN,
            this.RefreshTB_BTN,
            this.Seperator01TB_BTN,
            this.AboutTB_BTN,
            this.HideTB_BTN,
            this.ExitTB_BTN,
            this.Seperator02TB_BTN});
            this.CommonTB.DropDownArrows = true;
            this.CommonTB.ImageList = this.CommonToolBarImgs;
            this.CommonTB.Location = new System.Drawing.Point(0, 0);
            this.CommonTB.Name = "CommonTB";
            this.CommonTB.ShowToolTips = true;
            this.CommonTB.Size = new System.Drawing.Size(1254, 54);
            this.CommonTB.TabIndex = 0;
            this.CommonTB.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.CommonTB_ButtonClick);
            // 
            // AddServerTB_BTN
            // 
            this.AddServerTB_BTN.ImageIndex = 5;
            this.AddServerTB_BTN.Name = "AddServerTB_BTN";
            this.AddServerTB_BTN.Tag = "AddServer";
            this.AddServerTB_BTN.Text = "Add";
            this.AddServerTB_BTN.ToolTipText = "Add a SqlServer";
            // 
            // GenTB_BTN
            // 
            this.GenTB_BTN.Enabled = false;
            this.GenTB_BTN.ImageIndex = 4;
            this.GenTB_BTN.Name = "GenTB_BTN";
            this.GenTB_BTN.Tag = "Generate";
            this.GenTB_BTN.Text = "Generate";
            this.GenTB_BTN.ToolTipText = "Generate Table Classes";
            // 
            // RefreshTB_BTN
            // 
            this.RefreshTB_BTN.ImageIndex = 9;
            this.RefreshTB_BTN.Name = "RefreshTB_BTN";
            this.RefreshTB_BTN.Tag = "Refresh";
            this.RefreshTB_BTN.Text = "Refresh";
            this.RefreshTB_BTN.ToolTipText = "Refresh the selected node.";
            // 
            // Seperator01TB_BTN
            // 
            this.Seperator01TB_BTN.Name = "Seperator01TB_BTN";
            this.Seperator01TB_BTN.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // AboutTB_BTN
            // 
            this.AboutTB_BTN.ImageIndex = 8;
            this.AboutTB_BTN.Name = "AboutTB_BTN";
            this.AboutTB_BTN.Tag = "About";
            this.AboutTB_BTN.Text = "About";
            // 
            // HideTB_BTN
            // 
            this.HideTB_BTN.ImageIndex = 0;
            this.HideTB_BTN.Name = "HideTB_BTN";
            this.HideTB_BTN.Tag = "Hide";
            this.HideTB_BTN.Text = "Hide";
            this.HideTB_BTN.ToolTipText = "Hide in System Tray";
            // 
            // ExitTB_BTN
            // 
            this.ExitTB_BTN.ImageIndex = 6;
            this.ExitTB_BTN.Name = "ExitTB_BTN";
            this.ExitTB_BTN.Tag = "Exit";
            this.ExitTB_BTN.Text = "Exit";
            this.ExitTB_BTN.ToolTipText = "Exit SqlServer Companion Generator UI v3.57";
            // 
            // Seperator02TB_BTN
            // 
            this.Seperator02TB_BTN.Name = "Seperator02TB_BTN";
            this.Seperator02TB_BTN.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
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
            this.CommonToolBarImgs.Images.SetKeyName(9, "");
            // 
            // ExplorerPNL
            // 
            this.ExplorerPNL.Controls.Add(this.DbPNL);
            this.ExplorerPNL.Dock = System.Windows.Forms.DockStyle.Left;
            this.ExplorerPNL.Location = new System.Drawing.Point(0, 74);
            this.ExplorerPNL.Name = "ExplorerPNL";
            this.ExplorerPNL.Size = new System.Drawing.Size(440, 741);
            this.ExplorerPNL.TabIndex = 1;
            // 
            // DbPNL
            // 
            this.DbPNL.Controls.Add(this.DbTreeDockPNL);
            this.DbPNL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbPNL.Location = new System.Drawing.Point(0, 0);
            this.DbPNL.Name = "DbPNL";
            this.DbPNL.Size = new System.Drawing.Size(440, 741);
            this.DbPNL.TabIndex = 0;
            // 
            // DbTreeDockPNL
            // 
            this.DbTreeDockPNL.Controls.Add(this.DbTV);
            this.DbTreeDockPNL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbTreeDockPNL.Location = new System.Drawing.Point(0, 0);
            this.DbTreeDockPNL.Name = "DbTreeDockPNL";
            this.DbTreeDockPNL.Size = new System.Drawing.Size(440, 741);
            this.DbTreeDockPNL.TabIndex = 1;
            // 
            // DbTV
            // 
            this.DbTV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbTV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbTV.HotTracking = true;
            this.DbTV.ImageIndex = 0;
            this.DbTV.ImageList = this.DataImgs;
            this.DbTV.Location = new System.Drawing.Point(0, 0);
            this.DbTV.Name = "DbTV";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "Servers";
            this.DbTV.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.DbTV.SelectedImageIndex = 0;
            this.DbTV.Size = new System.Drawing.Size(440, 741);
            this.DbTV.TabIndex = 0;
            this.DbTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DbTV_AfterSelect);
            this.DbTV.DoubleClick += new System.EventHandler(this.DbTV_DoubleClick);
            this.DbTV.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DbTV_MouseUp);
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
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileMenuMI,
            this.LanguageMI,
            this.HelpMI});
            // 
            // FileMenuMI
            // 
            this.FileMenuMI.Index = 0;
            this.FileMenuMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.HideMI,
            this.FileSeperator01MI,
            this.AddServerMI,
            this.FileSeperator02MI,
            this.FileMenuExitMI});
            this.FileMenuMI.Text = "&File";
            // 
            // HideMI
            // 
            this.HideMI.Index = 0;
            this.HideMI.OwnerDraw = true;
            this.HideMI.Text = "Hide";
            this.HideMI.Click += new System.EventHandler(this.HideMI_Click);
            // 
            // FileSeperator01MI
            // 
            this.FileSeperator01MI.Index = 1;
            this.FileSeperator01MI.OwnerDraw = true;
            this.FileSeperator01MI.Text = "-";
            // 
            // AddServerMI
            // 
            this.AddServerMI.Index = 2;
            this.AddServerMI.OwnerDraw = true;
            this.AddServerMI.Text = "Add Server";
            this.AddServerMI.Click += new System.EventHandler(this.AddServerMI_Click);
            // 
            // FileSeperator02MI
            // 
            this.FileSeperator02MI.Index = 3;
            this.FileSeperator02MI.OwnerDraw = true;
            this.FileSeperator02MI.Text = "-";
            // 
            // FileMenuExitMI
            // 
            this.FileMenuExitMI.Index = 4;
            this.FileMenuExitMI.OwnerDraw = true;
            this.FileMenuExitMI.Text = "E&xit";
            this.FileMenuExitMI.Click += new System.EventHandler(this.FileMenuExitMI_Click);
            // 
            // LanguageMI
            // 
            this.LanguageMI.Index = 1;
            this.LanguageMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.LangCSharpMI,
            this.LangVbMI});
            this.LanguageMI.Text = "Language";
            // 
            // LangCSharpMI
            // 
            this.LangCSharpMI.Checked = true;
            this.LangCSharpMI.Index = 0;
            this.LangCSharpMI.OwnerDraw = true;
            this.LangCSharpMI.Text = "C#";
            this.LangCSharpMI.Click += new System.EventHandler(this.LangCSharpMI_Click);
            // 
            // LangVbMI
            // 
            this.LangVbMI.Index = 1;
            this.LangVbMI.OwnerDraw = true;
            this.LangVbMI.Text = "VB.Net";
            this.LangVbMI.Click += new System.EventHandler(this.LangVbMI_Click);
            // 
            // HelpMI
            // 
            this.HelpMI.Index = 2;
            this.HelpMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AboutMI});
            this.HelpMI.Text = "Help";
            // 
            // AboutMI
            // 
            this.AboutMI.Index = 0;
            this.AboutMI.OwnerDraw = true;
            this.AboutMI.Text = "About";
            this.AboutMI.Click += new System.EventHandler(this.AboutMI_Click);
            // 
            // DocumentTabDockPNL
            // 
            this.DocumentTabDockPNL.BackColor = System.Drawing.SystemColors.Menu;
            this.DocumentTabDockPNL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DocumentTabDockPNL.Controls.Add(this.DocumentTC);
            this.DocumentTabDockPNL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentTabDockPNL.Location = new System.Drawing.Point(440, 74);
            this.DocumentTabDockPNL.Name = "DocumentTabDockPNL";
            this.DocumentTabDockPNL.Size = new System.Drawing.Size(814, 741);
            this.DocumentTabDockPNL.TabIndex = 2;
            // 
            // DocumentTC
            // 
            this.DocumentTC.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.DocumentTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentTC.HotTrack = true;
            this.DocumentTC.ItemSize = new System.Drawing.Size(0, 26);
            this.DocumentTC.Location = new System.Drawing.Point(0, 0);
            this.DocumentTC.Name = "DocumentTC";
            this.DocumentTC.Padding = new System.Drawing.Point(3, 3);
            this.DocumentTC.SelectedIndex = 0;
            this.DocumentTC.ShowToolTips = true;
            this.DocumentTC.Size = new System.Drawing.Size(812, 739);
            this.DocumentTC.TabIndex = 0;
            // 
            // TreeCMNU
            // 
            this.TreeCMNU.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.TreeGenerateCMI,
            this.TreeSeperator01CMI,
            this.TreeShowInsertCMI,
            this.TreeShowUpdateCMI,
            this.TreeShowDeleteCMI});
            this.TreeCMNU.Popup += new System.EventHandler(this.TreeCMNU_Popup);
            // 
            // TreeGenerateCMI
            // 
            this.TreeGenerateCMI.Enabled = false;
            this.TreeGenerateCMI.Index = 0;
            this.TreeGenerateCMI.OwnerDraw = true;
            this.TreeGenerateCMI.Text = "Generate";
            this.TreeGenerateCMI.Click += new System.EventHandler(this.TreeGenerateCMI_Click);
            // 
            // TreeSeperator01CMI
            // 
            this.TreeSeperator01CMI.Index = 1;
            this.TreeSeperator01CMI.OwnerDraw = true;
            this.TreeSeperator01CMI.Text = "-";
            // 
            // TreeShowInsertCMI
            // 
            this.TreeShowInsertCMI.Enabled = false;
            this.TreeShowInsertCMI.Index = 2;
            this.TreeShowInsertCMI.OwnerDraw = true;
            this.TreeShowInsertCMI.Text = "View CREATE INSERT";
            this.TreeShowInsertCMI.Click += new System.EventHandler(this.TreeShowInsertCMI_Click);
            // 
            // TreeShowUpdateCMI
            // 
            this.TreeShowUpdateCMI.Enabled = false;
            this.TreeShowUpdateCMI.Index = 3;
            this.TreeShowUpdateCMI.OwnerDraw = true;
            this.TreeShowUpdateCMI.Text = "View CREATE UPDATE";
            this.TreeShowUpdateCMI.Click += new System.EventHandler(this.TreeShowUpdateCMI_Click);
            // 
            // TreeShowDeleteCMI
            // 
            this.TreeShowDeleteCMI.Enabled = false;
            this.TreeShowDeleteCMI.Index = 4;
            this.TreeShowDeleteCMI.OwnerDraw = true;
            this.TreeShowDeleteCMI.Text = "View CREATE DELETE";
            this.TreeShowDeleteCMI.Click += new System.EventHandler(this.TreeShowDeleteCMI_Click);
            // 
            // MDI
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(10, 24);
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1254, 815);
            this.Controls.Add(this.DocumentTabDockPNL);
            this.Controls.Add(this.ExplorerPNL);
            this.Controls.Add(this.CommonToolbarPNL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 886);
            this.Menu = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(1280, 886);
            this.Name = "MDI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SqlServer Companion Generator UI v3.57";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MDI_Closing);
            this.Load += new System.EventHandler(this.MDI_Load);
            this.CommonToolbarPNL.ResumeLayout(false);
            this.CommonToolbarPNL.PerformLayout();
            this.ExplorerPNL.ResumeLayout(false);
            this.DbPNL.ResumeLayout(false);
            this.DbTreeDockPNL.ResumeLayout(false);
            this.DocumentTabDockPNL.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		#region Hide
		private void HideMI_Click(object sender, System.EventArgs e)
		{
			this.Visible = false;
		}
		#endregion
		#region Exit
		private void FileMenuExitMI_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
			Application.Exit();
		}
		#endregion
		#region Load
		private void MDI_Load(object sender, System.EventArgs e)
		{
			ShowAddServer();
		}
		#endregion
//		#region Connection Source
//		private SqlConnectionSource conn = null;
//
//		internal SqlConnectionSource Conn
//		{
//			get{return conn;}
//		}
//		#endregion
		#region Show AddServer
		void ShowAddServer()
		{
			TabPage con = new TabPage("Add Server");
			SqlServer.Companion.Enviorment.Windows.Controls.AddServer svr = new SqlServer.Companion.Enviorment.Windows.Controls.AddServer();

			con.Controls.Add(svr);
			DocumentTC.TabPages.Add(con);

		}
		#endregion
		#region Add Database Node
		internal void AddServer(string server)
		{
			foreach(TreeNode n in DbTV.Nodes[0].Nodes)
			{
				if(n.Text == server)
					return;
			}

			DbServer svr = new DbServer();

			svr.Integrated = true;
			svr.Server = server;

			servers.Add(server, svr);

			TreeNode svrNode = new TreeNode(server,0,0);

			svrNode.Tag = "ServerNode";

			int i = DbTV.Nodes[0].Nodes.Add(svrNode);
			
			SqlConnectionSource conn = new SqlConnectionSource(server, "master");
			SqlDataReader r = conn.DbAccessor.ExecuteSql.ExecuteReader("SELECT name FROM sysdatabases;");

			while(r.Read())
			{
				TreeNode tn = new TreeNode(r.GetString(0),1,1);
				tn.Tag = "DbNode";
				DbTV.Nodes[0].Nodes[i].Nodes.Add(tn);
			}
			DbTV.Nodes[0].Expand();
			DbTV.Nodes[0].Nodes[i].Expand();

			conn.CleanDataReader(r);
			conn = null;
		}
	
		internal void AddServer(string server, string user, string password)
		{
			foreach(TreeNode n in DbTV.Nodes[0].Nodes)
			{
				if(n.Text == server)
					return;
			}

			DbServer svr = new DbServer();

			svr.Integrated = false;
			svr.Server = server;
			svr.User = user;
			svr.Password = password;

			servers.Add(server, svr);

			int i = DbTV.Nodes[0].Nodes.Add(new TreeNode(server,0,0));

			SqlConnectionSource conn = new SqlConnectionSource(server, "master", user, password);
			SqlDataReader r = conn.DbAccessor.ExecuteSql.ExecuteReader("SELECT name FROM sysdatabases;");

			while(r.Read())
			{
				TreeNode tn = new TreeNode(r.GetString(0),1,1);
				tn.Tag = "DbNode";
				DbTV.Nodes[0].Nodes[i].Nodes.Add(tn);
			}
			DbTV.Nodes[0].Expand();
			DbTV.Nodes[0].Nodes[i].Expand();

			conn.CleanDataReader(r);
			conn = null;
		}

		#endregion
		#region DB Tree View
		#region AfterSelect
		private void DbTV_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			GenTB_BTN.Enabled = false;
			
			if(null == e.Node.Tag)
				return;

			if(e.Node.Tag.ToString() == "TableNode")
			{
				GenTB_BTN.Enabled = true;
				return;
			}

			if(e.Node.Tag.ToString() == "DbNode")
			{
				if(e.Node.Nodes.Count > 0)
					return;

				this.Cursor = Cursors.WaitCursor;

				string db = e.Node.Text;
				string server = e.Node.Parent.Text;

				DbServer svr = (DbServer)servers[server];
				SqlConnectionSource conn = null;
				
				if(svr.Integrated)
				{
					conn = new SqlConnectionSource(svr.Server, db);
				}
				else
				{
					conn = new SqlConnectionSource(svr.Server, db, svr.User, svr.Password);
				}

				SqlDb _db = SqlSchema.FindDb(conn, LoadType.Lazy);

				this.Cursor = Cursors.Default;
				if(null == _db.Tables)
					return;

				this.Cursor = Cursors.WaitCursor;
				foreach(SqlDbTable tbl in _db.Tables)
				{
					if(tbl.TableName.Trim().ToLower() != "dtproperties")
					{
						TreeNode tn = new TreeNode(tbl.TableName, 2, 2);
						tn.Tag = "TableNode";
						int n = e.Node.Nodes.Add(tn);
						if(null != tbl.Columns)
						{
							foreach(SqlDbTableField fld in tbl.Columns)
							{
								TreeNode tnn = new TreeNode(fld.ColumnName, 3,3);
								tnn.Tag = "ColumnNode";
								tnn.Nodes.Add(new TreeNode("Oridinal Position: " + fld.OridinalPosition.ToString(), 4,4));
								tnn.Nodes.Add(new TreeNode("Data Type: " + fld.DataType, 4,4));
								tnn.Nodes.Add(new TreeNode("Nullable: " + fld.IsNullable, 4,4));
							
								if(fld.CharacterMaximumLength != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Character Length: " + fld.CharacterMaximumLength.ToString(), 4,4));

								if(fld.CharacterOctetLength != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Character Octet Length: " + fld.CharacterOctetLength.ToString(), 4,4));

								if(fld.NumericPrecision != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Numeric Precision: " + fld.NumericPrecision.ToString(), 4,4));

								if(fld.NumericPrecisionRadix != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Numeric Pecision Radix: " + fld.NumericPrecisionRadix.ToString(), 4,4));

								if(fld.NumericScale != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Numeric Scale: " + fld.NumericScale.ToString(), 4,4));

								e.Node.Nodes[n].Nodes.Add(tnn);
							}
						}
					}
				}
				this.Cursor = Cursors.Default;
			}

		}

		#endregion

		#endregion
		#region DB Tree Toolbar
		private void CommonTB_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button.Tag.ToString() == "Refresh")
			{
				if(null != DbTV.SelectedNode.Tag && DbTV.SelectedNode.Tag.ToString() == "ServerNode")
				{
					return;
				}
				else if(DbTV.SelectedNode.Text == "Servers")
				{
					return;
				}

				TreeNode DbNode = FindDbNode(DbTV.SelectedNode);
				if(DbNode == null) return;

				DbNode.Nodes.Clear();

				SqlConnectionSource conn = ConnFromNode(DbNode);

				SqlDb _db = SqlSchema.FindDb(conn, LoadType.Lazy);

				if(null == _db.Tables) return;

				this.Cursor = Cursors.WaitCursor;
				foreach(SqlDbTable tbl in _db.Tables)
				{
					if(tbl.TableName.Trim().ToLower() != "dtproperties")
					{
						TreeNode tn = new TreeNode(tbl.TableName, 2, 2);
						tn.Tag = "TableNode";
						int n = DbNode.Nodes.Add(tn);
						if(null != tbl.Columns)
						{
							foreach(SqlDbTableField fld in tbl.Columns)
							{
								TreeNode tnn = new TreeNode(fld.ColumnName, 3,3);
								tnn.Tag = "ColumnNode";
								tnn.Nodes.Add(new TreeNode("Oridinal Position: " + fld.OridinalPosition.ToString(), 4,4));
								tnn.Nodes.Add(new TreeNode("Data Type: " + fld.DataType, 4,4));
								tnn.Nodes.Add(new TreeNode("Nullable: " + fld.IsNullable, 4,4));
							
								if(fld.CharacterMaximumLength != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Character Length: " + fld.CharacterMaximumLength.ToString(), 4,4));

								if(fld.CharacterOctetLength != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Character Octet Length: " + fld.CharacterOctetLength.ToString(), 4,4));

								if(fld.NumericPrecision != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Numeric Precision: " + fld.NumericPrecision.ToString(), 4,4));

								if(fld.NumericPrecisionRadix != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Numeric Pecision Radix: " + fld.NumericPrecisionRadix.ToString(), 4,4));

								if(fld.NumericScale != int.MinValue)
									tnn.Nodes.Add(new TreeNode("Numeric Scale: " + fld.NumericScale.ToString(), 4,4));

								DbNode.Nodes[n].Nodes.Add(tnn);
							}
						}
					}
				}
				this.Cursor = Cursors.Default;

				return;
			}

			#region Hide
			if(e.Button.Tag.ToString() == "Hide")
			{
				this.Visible = false;
				return;
			}
			#endregion
			#region Add Server
			if(e.Button.Tag.ToString() == "AddServer")
			{
				ShowAddServer();
			}
			#endregion
			#region Exit
			if(e.Button.Tag.ToString() == "Exit")
			{
				this.Dispose();
				Application.Exit();
			}
			#endregion

			if(e.Button.Tag.ToString() == "About")
			{
				string txt = "";
				string cap = "About";
				Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SqlServer.Companion.Enviorment.Resx.About.txt");
				int i = 0;
				while(i != -1)
				{
					i = s.ReadByte();
					if( i != -1)
					{
						byte b = (byte)i;
						char c = (char)b;
						txt += c.ToString();
					}
				}
				s.Close();
				Windows.TextViewer abt = new TextViewer(txt, cap);
				abt.Show();
			}

			if(null != DbTV.SelectedNode)
			{
				if(e.Button.Tag.ToString() == "Generate")
				{
					if(null != DbTV.SelectedNode && null != DbTV.SelectedNode.Tag && DbTV.SelectedNode.Tag.ToString() == "TableNode")
					{
						SqlDbTable tbl = TableFromNode(DbTV.SelectedNode);
						if(null != tbl)
						{
							TabPage genTab = new TabPage("Generate " + tbl.TableName + " Code");
							SqlServer.Companion.Enviorment.Windows.Controls.Generator gen = new SqlServer.Companion.Enviorment.Windows.Controls.Generator(tbl, ServerFromNode(DbTV.SelectedNode));

							genTab.Controls.Add(gen);
							DocumentTC.TabPages.Add(genTab);

							DocumentTC.SelectedTab = genTab;
						}
					}
					#region Old Code
					/*
					#region Gen Table
					if(DbTV.SelectedNode.Tag.ToString() == "TableNode")
					{
						this.Cursor = Cursors.WaitCursor;
						TreeNode n = DbTV.SelectedNode;

						string table = n.Text;
						string db = n.Parent.Text;
						string server = n.Parent.Parent.Text;
					
						DbServer _db = (DbServer)servers[server];
						SqlConnectionSource conn = null;

						if(_db.Integrated == true)
							conn = new SqlConnectionSource(server, db);
						else
							conn = new SqlConnectionSource(server, db, _db.User, _db.Password);

						SqlDbTable tbl = SqlSchema.FindTable(conn, table, LoadType.Lazy);

						string ns = "";

						NameSpaceEx nfrm = new NameSpaceEx();
						if(nfrm.ShowDialog(this) == DialogResult.OK)
						{
							ns = nfrm.NamespaceEx;
							nfrm.Dispose();
						}

						SqlDataObjectGenerator.GenerateTable(tbl, ns, Common.PROJECT_PATH + DateTime.Now.ToString("yyyy-MM-dd") + "\\");

						this.Cursor = Cursors.Default;
						return;
					}
					#endregion
					#region Gen DB

					#endregion
					*/
					#endregion
				}
			}
		}
		#endregion
		#region Add Server
		private void AddServerMI_Click(object sender, System.EventArgs e)
		{
			ShowAddServer();
		}
		#endregion

		internal Language UseLanguage
		{
			get
			{
				if(LangCSharpMI.Checked == true)
					return Language.CSharp;
				else
					return Language.VB;
			}
		}
		private void MDI_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Dispose();
			Application.Exit();
		}

		private void TreeCMNU_Popup(object sender, System.EventArgs e)
		{

			if(null != DbTV.SelectedNode)
			{
				if(DbTV.SelectedNode.Tag.ToString() == "TableNode")
				{
					TreeShowInsertCMI.Enabled = true;
					TreeShowUpdateCMI.Enabled = true;
					TreeShowDeleteCMI.Enabled = true;
					TreeGenerateCMI.Enabled = true;
				}
				else
				{
					TreeShowInsertCMI.Enabled = false;
					TreeShowUpdateCMI.Enabled = false;
					TreeShowDeleteCMI.Enabled = false;
					TreeGenerateCMI.Enabled = false;
				}
			}
		}

		private void LangCSharpMI_Click(object sender, System.EventArgs e)
		{
			LangCSharpMI.Checked = !LangCSharpMI.Checked;
			LangVbMI.Checked = !LangVbMI.Checked;
		}

		private void LangVbMI_Click(object sender, System.EventArgs e)
		{
			LangCSharpMI.Checked = !LangCSharpMI.Checked;
			LangVbMI.Checked = !LangVbMI.Checked;
		}

		private void TreeShowInsertCMI_Click(object sender, System.EventArgs e)
		{
			if(null != DbTV.SelectedNode)
			{
				SqlDbTable tbl = TableFromNode(DbTV.SelectedNode);
				if(null != tbl)
				{
					SqlConnectionSource conn = ConnFromNode(DbTV.SelectedNode);
					if(null != conn)
					{
						string s = SqlStoredProcedureGenerator.CreateInsert(conn, tbl, true, false);
						TextViewer ci = new TextViewer(s,"Create Insert for: " + tbl.TableName);
						ci.Show();
					}
				}
			}
		}
		SqlConnectionSource ConnFromNode(TreeNode n)
		{
			if(null == n.Tag)
				return null;

			if(n.Tag.ToString() == "TableNode")
			{
				string table = n.Text;
				string db = n.Parent.Text;
				string server = n.Parent.Parent.Text;
					
				DbServer _db = (DbServer)servers[server];
				SqlConnectionSource conn = null;

				if(_db.Integrated == true)
					conn = new SqlConnectionSource(server, db);
				else
					conn = new SqlConnectionSource(server, db, _db.User, _db.Password);

				return conn;
			}
			else if(n.Tag.ToString() == "DbNode")
			{
				string db = n.Text;
				string server = n.Parent.Text;
					
				DbServer _db = (DbServer)servers[server];
				SqlConnectionSource conn = null;

				if(_db.Integrated == true)
					conn = new SqlConnectionSource(server, db);
				else
					conn = new SqlConnectionSource(server, db, _db.User, _db.Password);

				return conn;
			}
			else
			{
				return null;
			}
		}
		SqlDbTable TableFromNode(TreeNode n)
		{
			if( null == n.Tag || n.Tag.ToString() != "TableNode")
				return null;

			string table = n.Text;
			string db = n.Parent.Text;
			string server = n.Parent.Parent.Text;
					
			DbServer _db = (DbServer)servers[server];
			SqlConnectionSource conn = null;

			if(_db.Integrated == true)
				conn = new SqlConnectionSource(server, db);
			else
				conn = new SqlConnectionSource(server, db, _db.User, _db.Password);

			SqlDbTable tbl = SqlSchema.FindTable(conn, table, LoadType.Lazy);

			return tbl;
		}

		TreeNode FindDbNode(TreeNode node)
		{
			if(null != node.Tag && node.Tag.ToString() != "DbNode")
			{
				if(node.Tag.ToString() == "TableNode")
					return node.Parent;
				else if(node.Tag.ToString() == "ColumnNode")
					return node.Parent.Parent;
			}
			else if(node.Tag == null)
			{
				if(null != node.Parent.Tag && node.Parent.Tag.ToString() == "TableNode")
					return node.Parent.Parent;
				else if(null != node.Parent.Parent.Tag && node.Parent.Parent.Tag.ToString() == "TableNode")
					return node.Parent.Parent.Parent;
				else if(null != node.Parent.Parent.Parent.Tag && node.Parent.Parent.Parent.Tag.ToString() == "TableNode")
					return node.Parent.Parent.Parent.Parent;
				else
					return null;
			}
			else
			{
				return node;
			}
			return null;
		}
		public DbServer ServerFromSelectedNode(){return ServerFromNode(DbTV.SelectedNode);}
		DbServer ServerFromNode(TreeNode node)
		{
			TreeNode n = null;

			if( null == node.Tag || node.Tag.ToString() != "TableNode" || node.Tag.ToString() != "DbNode")
				n = FindDbNode(node);
			else if(null != node.Parent && node.Parent.Tag.ToString() == "TableNode")
				n = node.Parent;
			else if(null != node.Parent.Parent && node.Parent.Parent.Tag.ToString() == "TableNode")
				n = node.Parent.Parent;
			else if(null != node.Parent.Parent.Parent && node.Parent.Parent.Parent.Tag.ToString() == "TableNode")
				n = node.Parent.Parent.Parent;

			DbServer _db = null;
			string server = "";
			if(n.Tag.ToString() == "TableNode")
			{
				 server = n.Parent.Parent.Text;
				 _db = (DbServer)servers[server];
			}
			else if(n.Tag.ToString() == "DbNode")
			{
				server = n.Parent.Text;
				_db = (DbServer)servers[server];
			}
			return _db;
		}
		private void DbTV_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(null != DbTV.SelectedNode && null != DbTV.SelectedNode.Tag && DbTV.SelectedNode.Tag.ToString() == "TableNode" && e.Button == MouseButtons.Right)
				TreeCMNU.Show(this, new Point(DbTV.SelectedNode.Bounds.X, DbTV.SelectedNode.Bounds.Y + 50));
		}

		private void TreeGenerateCMI_Click(object sender, System.EventArgs e)
		{
			if(null != DbTV.SelectedNode && null != DbTV.SelectedNode.Tag && DbTV.SelectedNode.Tag.ToString() == "TableNode")
			{
				SqlDbTable tbl = TableFromNode(DbTV.SelectedNode);

				TabPage genTab = new TabPage("Generate " + tbl.TableName + " Code");
				SqlServer.Companion.Enviorment.Windows.Controls.Generator gen = new SqlServer.Companion.Enviorment.Windows.Controls.Generator(tbl, ServerFromNode(DbTV.SelectedNode));

				genTab.Controls.Add(gen);
				DocumentTC.TabPages.Add(genTab);

				DocumentTC.SelectedTab = genTab;
			}
		}

		private void TreeShowUpdateCMI_Click(object sender, System.EventArgs e)
		{
			if(null != DbTV.SelectedNode)
			{
				SqlDbTable tbl = TableFromNode(DbTV.SelectedNode);
				if(null != tbl)
				{
					SqlConnectionSource conn = ConnFromNode(DbTV.SelectedNode);
					if(null != conn)
					{
						string s = SqlStoredProcedureGenerator.CreateUpdate(conn, tbl, true, false);
						TextViewer ci = new TextViewer(s,"Create Update for: " + tbl.TableName);
						ci.Show();
					}
				}
			}
		}

		private void TreeShowDeleteCMI_Click(object sender, System.EventArgs e)
		{
			if(null != DbTV.SelectedNode)
			{
				SqlDbTable tbl = TableFromNode(DbTV.SelectedNode);
				if(null != tbl)
				{
					SqlConnectionSource conn = ConnFromNode(DbTV.SelectedNode);
					if(null != conn)
					{
						string s = SqlStoredProcedureGenerator.CreateDelete(conn, tbl, true, false);
						TextViewer ci = new TextViewer(s,"Create Delete for: " + tbl.TableName);
						ci.Show();
					}
				}
			}
		}

		private void DbTV_DoubleClick(object sender, System.EventArgs e)
		{
			if(null != DbTV.SelectedNode && null != DbTV.SelectedNode.Tag && DbTV.SelectedNode.Tag.ToString() == "TableNode")
			{
				SqlDbTable tbl = TableFromNode(DbTV.SelectedNode);

				TabPage genTab = new TabPage("Generate " + tbl.TableName + " Code");
				SqlServer.Companion.Enviorment.Windows.Controls.Generator gen = new SqlServer.Companion.Enviorment.Windows.Controls.Generator(tbl, ServerFromNode(DbTV.SelectedNode));

				genTab.Controls.Add(gen);
				DocumentTC.TabPages.Add(genTab);

				DocumentTC.SelectedTab = genTab;
			}
		}

		private void AboutMI_Click(object sender, System.EventArgs e)
		{
			string txt = "";
			string cap = "About";
			Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SqlServer.Companion.Enviorment.Resx.About.txt");
			int i = 0;
			while(i != -1)
			{
				i = s.ReadByte();
				if( i != -1)
				{
					byte b = (byte)i;
					char c = (char)b;
					txt += c.ToString();
				}
			}
			s.Close();
			Windows.TextViewer abt = new TextViewer(txt, cap);
			abt.Show();
		}
	}
}