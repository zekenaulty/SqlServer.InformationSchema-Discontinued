using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using SqlServer.Companion;
using SqlDevTest.Business;

namespace WinTestApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class NewsEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid NewsGrid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewsEditor()
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
				if (components != null) 
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
			this.NewsGrid = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.NewsGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// NewsGrid
			// 
			this.NewsGrid.DataMember = "";
			this.NewsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.NewsGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.NewsGrid.Location = new System.Drawing.Point(0, 0);
			this.NewsGrid.Name = "NewsGrid";
			this.NewsGrid.Size = new System.Drawing.Size(440, 367);
			this.NewsGrid.TabIndex = 0;
			// 
			// NewsEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 367);
			this.Controls.Add(this.NewsGrid);
			this.Name = "NewsEditor";
			this.Text = "News Editor";
			this.Load += new System.EventHandler(this.NewsEditor_Load);
			((System.ComponentModel.ISupportInitialize)(this.NewsGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new NewsEditor());
		}

		private void NewsEditor_Load(object sender, System.EventArgs e)
		{
			SqlConnectionSource conn = new SqlServer.Companion.SqlConnectionSource("z-box", "SqlDevTest");
			NewsCollection news = NewsCollection.FromBaseArray(News.News__FindAll(conn));

			NewsGrid.DataSource = news;

			
		}
	}
}
