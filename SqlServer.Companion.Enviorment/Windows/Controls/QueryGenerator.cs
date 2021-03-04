using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using SqlServer.Companion;
using SqlServer.Companion.Schema;
using SqlServer.Companion.CodeDom;

using System.IO;
using System.Text;

using Microsoft.CSharp;
using Microsoft.VisualBasic;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Xml;

namespace SqlServer.Companion.Enviorment.Windows.Controls
{
	/// <summary>
	/// Summary description for QueryGenerator.
	/// </summary>
	[Serializable]
	public class QueryGenerator : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel AreaPNL;
		private System.Windows.Forms.Label NameLBL;
		private System.Windows.Forms.TextBox NameTXT;
		private System.Windows.Forms.GroupBox FieldGRP;
		private System.Windows.Forms.ToolTip Tooltips;
		private System.Windows.Forms.Panel FieldPNL;
		private System.Windows.Forms.CheckBox PagedCHK;
		private System.Windows.Forms.Button ViewBTN;
		private System.Windows.Forms.CheckBox GenCountCHK;
		private System.Windows.Forms.CheckBox DropCHK;
		private System.Windows.Forms.LinkLabel RemoveBTN;
		private System.Windows.Forms.CheckBox FindManyCHK;
		private System.ComponentModel.IContainer components;

		public QueryGenerator()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}
		void WriteSqlFile(string sql, string procName, string @namespace, string dir)
		{
			string path = dir;
			path += @namespace.Substring(0, @namespace.IndexOf("."));

			if(!Directory.Exists(path))
				Directory.CreateDirectory(path);

			path += "\\SQL\\";
			
			if(!Directory.Exists(path))
				Directory.CreateDirectory(path);

			path += procName + ".sql";

			FileStream file = null;

			if(!File.Exists(path))
				file = File.Create(path);
			else
				file = System.IO.File.OpenWrite(path);

			foreach(char c in sql)
				file.WriteByte(System.Convert.ToByte(c));

			file.Close();
		}

		internal void GenerateQuery(SqlConnectionSource conn, CodeTypeDeclaration @ref, string @namespace, CodeTypeDeclaration business, string businessNamespace, string dir)
		{
			ArrayList filters = new ArrayList();
			foreach(CheckBox chk in FieldChecks)
			{
				if(chk.Checked)
					filters.Add(chk.Tag);

				

			}
			string sql = "";

			sql = SqlStoredProcedureGenerator.CreateQuery(conn, table, filters, this.QueryName, true, true);
			WriteSqlFile(sql, this.QueryName, @namespace, dir);

			SqlDataObjectGenerator.GenerateTableQuery(@ref, table, filters, @namespace, this.QueryName, this.FindMany);
			
			if(FindManyCHK.Checked)
				SqlBusinessObjectGenerator.GenerateFindMany(business, table, filters, businessNamespace, @namespace, this.QueryName);
			else
				SqlBusinessObjectGenerator.GenerateFindOne(business, table, filters, businessNamespace, @namespace, this.QueryName);

			if(FindManyCHK.Checked && PagedCHK.Checked)
			{
				sql = SqlStoredProcedureGenerator.CreatePagedQuery(conn, table, filters, this.QueryName + "Paged", true, true);
				WriteSqlFile(sql, this.QueryName + "Paged", @namespace, dir);

				SqlDataObjectGenerator.GenerateTablePagedQuery(@ref, table, filters, @namespace, this.QueryName + "Paged");
				SqlBusinessObjectGenerator.GenerateFindManyPaged(business, table, filters, businessNamespace, @namespace, this.QueryName + "Paged");
			}

			if(FindManyCHK.Checked && GenCountCHK.Checked)
			{
				sql = SqlStoredProcedureGenerator.CreateCountQuery(conn, table, filters, this.QueryName + "Count", true, true);
				WriteSqlFile(sql, this.QueryName + "Count", @namespace, dir);

				SqlDataObjectGenerator.GenerateTableCountQuery(@ref, table, filters, @namespace, this.QueryName + "Count");
				SqlBusinessObjectGenerator.GenerateFindCount(business, table, filters, businessNamespace, @namespace, this.QueryName + "Count");
			}

		}
		internal string QueryName
		{
			get{return NameTXT.Text;}
			set{NameTXT.Text = value;}
		}
		internal bool Drop{get{return DropCHK.Checked;}}
		internal bool Count{get{return GenCountCHK.Checked;}}
		internal bool Paged{get{return PagedCHK.Checked;}}
		internal Control[] FieldChecks
		{
			get
			{
				Control[] cc = new Control[FieldPNL.Controls.Count];
				FieldPNL.Controls.CopyTo(cc, 0);
				return cc;
			}
		}
		SqlDbTable table = null;
		DbServer svr = null;
		public QueryGenerator(SqlDbTable tbl, DbServer server, int number):this()
		{
			table = tbl;
			svr = server;
			if(null != tbl && null != tbl.Columns)
			{
				NameTXT.Text = tbl.TableName + "__FindQuery" + number.ToString();
				
				string pk = "";
				if(null != tbl.Constraints)
				{	//find primary key
					foreach(SqlDbTableConstraint tc in tbl.Constraints)
					{
						if(tc.IsPrimaryKey == true)
						{
							pk = tc.ConstraintColumnName;
							break;
						}
					}
				}

				foreach(SqlDbTableField fld in tbl.Columns)
				{	//add all columns except for pk
					if(fld.DataType != "text" && fld.DataType != "ntext" && fld.DataType != "image")
					{
						if(pk != fld.ColumnName)
						{
							CheckBox chk = new CheckBox();
							chk.Font = new Font(chk.Font.FontFamily, 7);
							chk.Width = 220;
							chk.Tag = fld;
							chk.Text = fld.ColumnName + "(" + fld.DataType + ")";
							chk.Name = fld.ColumnName + "CHK";
							FieldPNL.Controls.Add(chk);
							chk.Top = (FieldPNL.Controls.Count - 1) * chk.Height;
						}
					}
				}
			}
		}
		public QueryGenerator(SqlDbTable tbl, DbServer server, XmlTextReader r):this()
		{
			table = tbl;
			svr = server;
			Hashtable filtered = new Hashtable();
			string qn = "";
			while(r.Read())
			{
				if(r.NodeType == XmlNodeType.Element)
				{
					switch(r.Name)
					{
						case "QueryInfo":
							r.MoveToFirstAttribute();

							qn = r.Value;
							break;
						case "Find-Many":
							if(r.ReadInnerXml() == "1")
							{
								FindManyCHK.Checked = true;
								GenCountCHK.Enabled = true;
								PagedCHK.Enabled = true;
							}
							else
							{
								FindManyCHK.Checked = false;
								GenCountCHK.Enabled = false;
								PagedCHK.Enabled = false;
							}

							break;
						case "Perform-Drop":
							if(r.ReadInnerXml() == "1")
								DropCHK.Checked = true;
							else
								DropCHK.Checked = false;

							break;
						case "Perform-Count":
							if(r.ReadInnerXml() == "1")
								GenCountCHK.Checked = true;
							else
								GenCountCHK.Checked = false;

							break;
						case "Perform-Paged":
							if(r.ReadInnerXml() == "1")
								PagedCHK.Checked = true;
							else
								PagedCHK.Checked = false;

							break;
						case "Field":
							string fname = "";
							string filter = "0";

							r.MoveToFirstAttribute();

							if(r.Name == "FieldName")
								fname = r.Value;
							else if(r.Name == "IsFilter")
								filter = r.Value;

							r.MoveToNextAttribute();

							if(r.Name == "FieldName")
								fname = r.Value;
							else if(r.Name == "IsFilter")
								filter = r.Value;

							if(filter == "1")
								filtered.Add(fname, filter);

							break;
						default:
							break;
					}
				}
			}

			if(null != tbl && null != tbl.Columns)
			{
				NameTXT.Text = qn;
				
				string pk = "";
				if(null != tbl.Constraints)
				{	//find primary key
					foreach(SqlDbTableConstraint tc in tbl.Constraints)
					{
						if(tc.IsPrimaryKey == true)
						{
							pk = tc.ConstraintColumnName;
							break;
						}
					}
				}

				foreach(SqlDbTableField fld in tbl.Columns)
				{	//add all columns except for pk
					if(fld.DataType != "text" && fld.DataType != "ntext" && fld.DataType != "image")
					{
						if(pk != fld.ColumnName)
						{
							CheckBox chk = new CheckBox();
							chk.Font = new Font(chk.Font.FontFamily, 7);
							chk.Width = 220;
							chk.Tag = fld;
							chk.Text = fld.ColumnName + "(" + fld.DataType + ")";
							chk.Name = fld.ColumnName + "CHK";

							if(filtered.Contains(fld.ColumnName))
								chk.Checked = true;
							else
								chk.Checked = false;

							FieldPNL.Controls.Add(chk);
							chk.Top = (FieldPNL.Controls.Count - 1) * chk.Height;
						}
					}
				}
			}
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

		internal bool FindMany
		{
			get{return this.FindManyCHK.Checked;}
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.AreaPNL = new System.Windows.Forms.Panel();
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
			this.Tooltips = new System.Windows.Forms.ToolTip(this.components);
			this.AreaPNL.SuspendLayout();
			this.FieldGRP.SuspendLayout();
			this.SuspendLayout();
			// 
			// AreaPNL
			// 
			this.AreaPNL.BackColor = System.Drawing.SystemColors.ControlLight;
			this.AreaPNL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AreaPNL.Controls.Add(this.FindManyCHK);
			this.AreaPNL.Controls.Add(this.RemoveBTN);
			this.AreaPNL.Controls.Add(this.DropCHK);
			this.AreaPNL.Controls.Add(this.GenCountCHK);
			this.AreaPNL.Controls.Add(this.ViewBTN);
			this.AreaPNL.Controls.Add(this.PagedCHK);
			this.AreaPNL.Controls.Add(this.FieldGRP);
			this.AreaPNL.Controls.Add(this.NameTXT);
			this.AreaPNL.Controls.Add(this.NameLBL);
			this.AreaPNL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AreaPNL.Location = new System.Drawing.Point(0, 0);
			this.AreaPNL.Name = "AreaPNL";
			this.AreaPNL.Size = new System.Drawing.Size(348, 124);
			this.AreaPNL.TabIndex = 0;
			// 
			// FindManyCHK
			// 
			this.FindManyCHK.Checked = true;
			this.FindManyCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.FindManyCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FindManyCHK.Location = new System.Drawing.Point(260, 52);
			this.FindManyCHK.Name = "FindManyCHK";
			this.FindManyCHK.Size = new System.Drawing.Size(80, 16);
			this.FindManyCHK.TabIndex = 20;
			this.FindManyCHK.Text = "Find Many..?";
			this.Tooltips.SetToolTip(this.FindManyCHK, "If checked the C# Method that runs this Sql Procedure will return an ArrayList; i" +
				"f not it returns a single object.");
			this.FindManyCHK.CheckedChanged += new System.EventHandler(this.FindManyCHK_CheckedChanged);
			// 
			// RemoveBTN
			// 
			this.RemoveBTN.AutoSize = true;
			this.RemoveBTN.Location = new System.Drawing.Point(328, 8);
			this.RemoveBTN.Name = "RemoveBTN";
			this.RemoveBTN.Size = new System.Drawing.Size(10, 16);
			this.RemoveBTN.TabIndex = 19;
			this.RemoveBTN.TabStop = true;
			this.RemoveBTN.Text = "x";
			this.Tooltips.SetToolTip(this.RemoveBTN, "Remove this Query.");
			this.RemoveBTN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RemoveBTN_LinkClicked);
			// 
			// DropCHK
			// 
			this.DropCHK.Checked = true;
			this.DropCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.DropCHK.Enabled = false;
			this.DropCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DropCHK.Location = new System.Drawing.Point(260, 68);
			this.DropCHK.Name = "DropCHK";
			this.DropCHK.Size = new System.Drawing.Size(80, 16);
			this.DropCHK.TabIndex = 18;
			this.DropCHK.Text = "Drop";
			this.Tooltips.SetToolTip(this.DropCHK, "Attempt to drop the existing procedure before creating new procedure");
			// 
			// GenCountCHK
			// 
			this.GenCountCHK.Checked = true;
			this.GenCountCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.GenCountCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GenCountCHK.Location = new System.Drawing.Point(260, 84);
			this.GenCountCHK.Name = "GenCountCHK";
			this.GenCountCHK.Size = new System.Drawing.Size(80, 16);
			this.GenCountCHK.TabIndex = 17;
			this.GenCountCHK.Text = "Count";
			this.Tooltips.SetToolTip(this.GenCountCHK, "Checking this box will generate a query with a matching filter that return a coun" +
				"t.");
			this.GenCountCHK.CheckedChanged += new System.EventHandler(this.GenCountCHK_CheckedChanged);
			// 
			// ViewBTN
			// 
			this.ViewBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ViewBTN.Location = new System.Drawing.Point(260, 28);
			this.ViewBTN.Name = "ViewBTN";
			this.ViewBTN.Size = new System.Drawing.Size(80, 20);
			this.ViewBTN.TabIndex = 16;
			this.ViewBTN.Text = "View SQL";
			this.ViewBTN.Click += new System.EventHandler(this.ViewBTN_Click);
			// 
			// PagedCHK
			// 
			this.PagedCHK.Checked = true;
			this.PagedCHK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.PagedCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PagedCHK.Location = new System.Drawing.Point(260, 100);
			this.PagedCHK.Name = "PagedCHK";
			this.PagedCHK.Size = new System.Drawing.Size(80, 16);
			this.PagedCHK.TabIndex = 3;
			this.PagedCHK.Text = "Paged Query";
			this.Tooltips.SetToolTip(this.PagedCHK, "Checking this box will cause this query to have a pageNumber and pageCount filter" +
				" as well as field filters");
			this.PagedCHK.CheckedChanged += new System.EventHandler(this.PagedCHK_CheckedChanged);
			// 
			// FieldGRP
			// 
			this.FieldGRP.Controls.Add(this.FieldPNL);
			this.FieldGRP.Location = new System.Drawing.Point(4, 28);
			this.FieldGRP.Name = "FieldGRP";
			this.FieldGRP.Size = new System.Drawing.Size(252, 92);
			this.FieldGRP.TabIndex = 2;
			this.FieldGRP.TabStop = false;
			this.FieldGRP.Text = "Field Filters: ";
			this.Tooltips.SetToolTip(this.FieldGRP, "Check the fields you wish to use as paramaters/conditions");
			// 
			// FieldPNL
			// 
			this.FieldPNL.AutoScroll = true;
			this.FieldPNL.BackColor = System.Drawing.SystemColors.ControlLight;
			this.FieldPNL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FieldPNL.Location = new System.Drawing.Point(3, 16);
			this.FieldPNL.Name = "FieldPNL";
			this.FieldPNL.Size = new System.Drawing.Size(246, 73);
			this.FieldPNL.TabIndex = 0;
			// 
			// NameTXT
			// 
			this.NameTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.NameTXT.Location = new System.Drawing.Point(44, 4);
			this.NameTXT.Name = "NameTXT";
			this.NameTXT.Size = new System.Drawing.Size(272, 20);
			this.NameTXT.TabIndex = 1;
			this.NameTXT.Text = "";
			// 
			// NameLBL
			// 
			this.NameLBL.AutoSize = true;
			this.NameLBL.Location = new System.Drawing.Point(4, 8);
			this.NameLBL.Name = "NameLBL";
			this.NameLBL.Size = new System.Drawing.Size(38, 16);
			this.NameLBL.TabIndex = 0;
			this.NameLBL.Text = "Name:";
			// 
			// QueryGenerator
			// 
			this.Controls.Add(this.AreaPNL);
			this.Name = "QueryGenerator";
			this.Size = new System.Drawing.Size(348, 124);
			this.AreaPNL.ResumeLayout(false);
			this.FieldGRP.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void GenCountCHK_CheckedChanged(object sender, System.EventArgs e)
		{
			//if(GenCountCHK.Checked == true && PagedCHK.Checked == true)
				//PagedCHK.Checked = false;
		}

		private void PagedCHK_CheckedChanged(object sender, System.EventArgs e)
		{
			//if(GenCountCHK.Checked == true && PagedCHK.Checked == true)
				//GenCountCHK.Checked = false;
		}

		private void RemoveBTN_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(null != this.Parent)
				this.Parent.Controls.Remove(this);
		}

		private void ViewBTN_Click(object sender, System.EventArgs e)
		{
			if(null == table || null == svr)
			{
				MessageBox.Show("Error! Unable to generate SQL Statement!");
				return;
			}

			string sql = "";
			SqlConnectionSource conn = null;

			if(svr.Integrated == true)
				conn = new SqlConnectionSource(svr.Server, table.TableCatalog);
			else
				conn = new SqlConnectionSource(svr.Server, table.TableCatalog, svr.User, svr.Password);

			ArrayList fl = new ArrayList();

			foreach(CheckBox chk in FieldPNL.Controls)
			{
				if(chk.Checked == true && null != chk.Tag && chk.Tag is SqlDbTableField)
					fl.Add(chk.Tag);
			}

			sql = SqlStoredProcedureGenerator.CreateQuery(conn, table, fl, NameTXT.Text, DropCHK.Checked, false);

			TextViewer queryV = new TextViewer(sql, NameTXT.Text);
			queryV.Show();

			if(GenCountCHK.Checked == true)
			{
				sql = SqlStoredProcedureGenerator.CreateCountQuery(conn, table, fl, NameTXT.Text + "__COUNT", DropCHK.Checked, false);

				TextViewer queryCV = new TextViewer(sql, NameTXT.Text + "__COUNT");
				queryCV.Show();
			}

			if(PagedCHK.Checked == true)
			{
				sql = SqlStoredProcedureGenerator.CreatePagedQuery(conn, table, fl, NameTXT.Text + "__PAGED", DropCHK.Checked, false);

				TextViewer queryPV = new TextViewer(sql, NameTXT.Text + "__PAGED");
				queryPV.Show();
			}
		}

		private void FindManyCHK_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!FindManyCHK.Checked)
			{
				PagedCHK.Enabled = false;
				PagedCHK.Checked = false;
				GenCountCHK.Enabled = false;
				GenCountCHK.Checked = false;
			}
			else
			{
				PagedCHK.Enabled = true;
				GenCountCHK.Enabled = true;
			}
		}
	}
}
