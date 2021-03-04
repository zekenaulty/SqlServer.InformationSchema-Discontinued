using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SqlServer.Companion.Windows.Controls
{
	/// <summary>
	/// Summary description for QueryGenerator.
	/// </summary>
	public class QueryGenerator : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.CheckBox FindManyCHK;
		private System.Windows.Forms.LinkLabel RemoveBTN;
		private System.Windows.Forms.CheckBox DropCHK;
		private System.Windows.Forms.CheckBox GenCountCHK;
		private System.Windows.Forms.Button ViewBTN;
		private System.Windows.Forms.CheckBox PagedCHK;
		private System.Windows.Forms.GroupBox FieldGRP;
		private System.Windows.Forms.Panel FieldPNL;
		private System.Windows.Forms.TextBox NameTXT;
		private System.Windows.Forms.Label NameLBL;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public QueryGenerator()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.FindManyCHK = new System.Windows.Forms.CheckBox();
			this.RemoveBTN = new System.Windows.Forms.LinkLabel();
			this.DropCHK = new System.Windows.Forms.CheckBox();
			this.GenCountCHK = new System.Windows.Forms.CheckBox();
			this.ViewBTN = new System.Windows.Forms.Button();
			this.PagedCHK = new System.Windows.Forms.CheckBox();
			this.FieldGRP = new System.Windows.Forms.GroupBox();
			this.FieldPNL = new System.Windows.Forms.Panel();
			this.NameTXT = new System.Windows.Forms.TextBox();
			this.NameLBL = new System.Windows.Forms.Label();
			this.FieldGRP.SuspendLayout();
			this.SuspendLayout();
			// 
			// FindManyCHK
			// 
			this.FindManyCHK.Checked = true;
			this.FindManyCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.FindManyCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FindManyCHK.Location = new System.Drawing.Point(160, 192);
			this.FindManyCHK.Name = "FindManyCHK";
			this.FindManyCHK.Size = new System.Drawing.Size(80, 16);
			this.FindManyCHK.TabIndex = 29;
			this.FindManyCHK.Text = "Find Many..?";
			// 
			// RemoveBTN
			// 
			this.RemoveBTN.AutoSize = true;
			this.RemoveBTN.Location = new System.Drawing.Point(484, 8);
			this.RemoveBTN.Name = "RemoveBTN";
			this.RemoveBTN.Size = new System.Drawing.Size(10, 16);
			this.RemoveBTN.TabIndex = 28;
			this.RemoveBTN.TabStop = true;
			this.RemoveBTN.Text = "x";
			// 
			// DropCHK
			// 
			this.DropCHK.Checked = true;
			this.DropCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.DropCHK.Enabled = false;
			this.DropCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DropCHK.Location = new System.Drawing.Point(244, 192);
			this.DropCHK.Name = "DropCHK";
			this.DropCHK.Size = new System.Drawing.Size(80, 16);
			this.DropCHK.TabIndex = 27;
			this.DropCHK.Text = "Drop";
			// 
			// GenCountCHK
			// 
			this.GenCountCHK.Checked = true;
			this.GenCountCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.GenCountCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GenCountCHK.Location = new System.Drawing.Point(328, 192);
			this.GenCountCHK.Name = "GenCountCHK";
			this.GenCountCHK.Size = new System.Drawing.Size(80, 16);
			this.GenCountCHK.TabIndex = 26;
			this.GenCountCHK.Text = "Count";
			// 
			// ViewBTN
			// 
			this.ViewBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ViewBTN.Location = new System.Drawing.Point(404, 212);
			this.ViewBTN.Name = "ViewBTN";
			this.ViewBTN.Size = new System.Drawing.Size(80, 20);
			this.ViewBTN.TabIndex = 25;
			this.ViewBTN.Text = "View SQL";
			// 
			// PagedCHK
			// 
			this.PagedCHK.Checked = true;
			this.PagedCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.PagedCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PagedCHK.Location = new System.Drawing.Point(412, 192);
			this.PagedCHK.Name = "PagedCHK";
			this.PagedCHK.Size = new System.Drawing.Size(80, 16);
			this.PagedCHK.TabIndex = 24;
			this.PagedCHK.Text = "Paged Query";
			// 
			// FieldGRP
			// 
			this.FieldGRP.Controls.Add(this.FieldPNL);
			this.FieldGRP.Location = new System.Drawing.Point(4, 28);
			this.FieldGRP.Name = "FieldGRP";
			this.FieldGRP.Size = new System.Drawing.Size(496, 164);
			this.FieldGRP.TabIndex = 23;
			this.FieldGRP.TabStop = false;
			this.FieldGRP.Text = "Field Filters: ";
			// 
			// FieldPNL
			// 
			this.FieldPNL.AutoScroll = true;
			this.FieldPNL.BackColor = System.Drawing.SystemColors.ControlLight;
			this.FieldPNL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FieldPNL.Location = new System.Drawing.Point(3, 16);
			this.FieldPNL.Name = "FieldPNL";
			this.FieldPNL.Size = new System.Drawing.Size(490, 145);
			this.FieldPNL.TabIndex = 0;
			// 
			// NameTXT
			// 
			this.NameTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.NameTXT.Location = new System.Drawing.Point(44, 4);
			this.NameTXT.Name = "NameTXT";
			this.NameTXT.Size = new System.Drawing.Size(428, 20);
			this.NameTXT.TabIndex = 22;
			this.NameTXT.Text = "";
			// 
			// NameLBL
			// 
			this.NameLBL.AutoSize = true;
			this.NameLBL.Location = new System.Drawing.Point(4, 8);
			this.NameLBL.Name = "NameLBL";
			this.NameLBL.Size = new System.Drawing.Size(38, 16);
			this.NameLBL.TabIndex = 21;
			this.NameLBL.Text = "Name:";
			// 
			// QueryGenerator
			// 
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.Controls.Add(this.FindManyCHK);
			this.Controls.Add(this.RemoveBTN);
			this.Controls.Add(this.DropCHK);
			this.Controls.Add(this.GenCountCHK);
			this.Controls.Add(this.ViewBTN);
			this.Controls.Add(this.PagedCHK);
			this.Controls.Add(this.FieldGRP);
			this.Controls.Add(this.NameTXT);
			this.Controls.Add(this.NameLBL);
			this.Name = "QueryGenerator";
			this.Size = new System.Drawing.Size(504, 240);
			this.FieldGRP.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
