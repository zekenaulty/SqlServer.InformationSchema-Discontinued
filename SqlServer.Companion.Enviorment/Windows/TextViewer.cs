using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlServer.Companion.Enviorment.Windows
{
	/// <summary>
	/// Summary description for TextViewer.
	/// </summary>
	public class TextViewer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox TXT;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextViewer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public TextViewer(string text, string caption):this()
		{
			TXT.Text = text;
			this.Text = caption;
			TXT.Select(0, 0);
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
            this.TXT = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TXT
            // 
            this.TXT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TXT.Location = new System.Drawing.Point(0, 0);
            this.TXT.Multiline = true;
            this.TXT.Name = "TXT";
            this.TXT.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TXT.Size = new System.Drawing.Size(634, 455);
            this.TXT.TabIndex = 0;
            this.TXT.WordWrap = false;
            this.TXT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT_KeyPress);
            // 
            // TextViewer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(634, 455);
            this.Controls.Add(this.TXT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TextViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void TXT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled = true;
		}
	}
}
