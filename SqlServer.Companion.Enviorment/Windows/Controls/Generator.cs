using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.IO;
using System.Xml;

using Microsoft.CSharp;
using Microsoft.VisualBasic;

using SqlServer.Companion;
using SqlServer.Companion.Schema;
using SqlServer.Companion.CodeDom;

namespace SqlServer.Companion.Enviorment.Windows.Controls
{
	/// <summary>
	/// Summary description for Generator.
	/// </summary>
	public class Generator : PanelBase
	{
		private System.Windows.Forms.Panel AreaPNL;
		private System.Windows.Forms.Label InstructionLBL;
		private System.Windows.Forms.ToolTip ToolTips;
		private System.Windows.Forms.GroupBox OutputOpGRP;
		private System.Windows.Forms.TextBox DataNsTXT;
		private System.Windows.Forms.TextBox BusinessNsTXT;
		private System.Windows.Forms.TextBox RootDirTXT;
		private System.Windows.Forms.Label RootDirLBL;
		private System.Windows.Forms.GroupBox QueriesGenGRP;
		private System.Windows.Forms.LinkLabel AddQueryBTN;
		private System.Windows.Forms.GroupBox QueriesGRP;
		private System.Windows.Forms.Panel QueriesPNL;
		private System.Windows.Forms.Button CloseBTN;
		private System.Windows.Forms.Button GenBTN;
		private System.Windows.Forms.Label DataNsLBL;
		private System.Windows.Forms.Label BusinessNsLBL;
		private System.Windows.Forms.FolderBrowserDialog BrowseFDLG;
		private System.Windows.Forms.Button FolderBTN;
		private System.ComponentModel.IContainer components;

		#region Constructor
		public Generator()
		{
			InitializeComponent();

			QueriesPNL.ControlRemoved += new ControlEventHandler(QueriesPNL_ControlRemoved);
		}
		
		int qc = 0;
		SqlDbTable table = null;
		DbServer svr = null;
		public Generator(SqlDbTable tbl, DbServer server):this()
		{
			table = tbl;
			svr = server;

			if(null == table)
				return;

			RootDirTXT.Text = Common.PROJECT_PATH;
			BusinessNsTXT.Text = tbl.TableCatalog + ".Business";
			DataNsTXT.Text = tbl.TableCatalog + ".Data";

			string serial_dir = "";
			XmlTextReader r = null;
			#region Load Stored Output Info
			serial_dir = Common.PROJECT_PATH + "Serial\\" + table.TableCatalog + "\\" + "\\" + table.TableName + "\\";

			if(File.Exists(serial_dir + table.TableName + ".oxml"))
			{
				r = new XmlTextReader(serial_dir + table.TableName + ".oxml");

				while(r.Read())
				{
					if(r.NodeType == XmlNodeType.Element)
					{
						switch(r.Name)
						{
							case "OutputData":
								break;
							case "Output-Folder":
								RootDirTXT.Text = r.ReadInnerXml();
								break;
							case "Business-Namespace":
								BusinessNsTXT.Text = r.ReadInnerXml();
								break;
							case "Data-Namespace":
								DataNsTXT.Text = r.ReadInnerXml();
								break;
						}
					}
				}
				r.Close();
				r = null;
			}
			#endregion
			#region Load Stored Query Data
			serial_dir = Common.PROJECT_PATH + "Serial\\" + table.TableCatalog + "\\" + table.TableName + "\\Query Data\\";
			if(Directory.Exists(serial_dir))
			{
				string[] flz = Directory.GetFiles(serial_dir);

				for(int fi = 0; fi < flz.Length; fi++)
				{
					if(flz[fi].Substring(flz[fi].Length - 5) == "qxml" || flz[fi].Substring(flz[fi].Length - 5) == ".qxml")
					{
						r = new XmlTextReader(flz[fi]);
						QueriesPNL.Controls.Add(new QueryGenerator(tbl, svr, r));
						QueriesPNL.Controls[QueriesPNL.Controls.Count - 1].Top = (QueriesPNL.Controls.Count - 1) * QueriesPNL.Controls[QueriesPNL.Controls.Count - 1].Height; 
						r.Close();
						r = null;
					}
				}

			}
			#endregion
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
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.AreaPNL = new System.Windows.Forms.Panel();
			this.QueriesGenGRP = new System.Windows.Forms.GroupBox();
			this.AddQueryBTN = new System.Windows.Forms.LinkLabel();
			this.QueriesGRP = new System.Windows.Forms.GroupBox();
			this.QueriesPNL = new System.Windows.Forms.Panel();
			this.OutputOpGRP = new System.Windows.Forms.GroupBox();
			this.FolderBTN = new System.Windows.Forms.Button();
			this.DataNsTXT = new System.Windows.Forms.TextBox();
			this.BusinessNsTXT = new System.Windows.Forms.TextBox();
			this.RootDirTXT = new System.Windows.Forms.TextBox();
			this.DataNsLBL = new System.Windows.Forms.Label();
			this.BusinessNsLBL = new System.Windows.Forms.Label();
			this.RootDirLBL = new System.Windows.Forms.Label();
			this.InstructionLBL = new System.Windows.Forms.Label();
			this.GenBTN = new System.Windows.Forms.Button();
			this.CloseBTN = new System.Windows.Forms.Button();
			this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
			this.BrowseFDLG = new System.Windows.Forms.FolderBrowserDialog();
			this.AreaPNL.SuspendLayout();
			this.QueriesGenGRP.SuspendLayout();
			this.QueriesGRP.SuspendLayout();
			this.OutputOpGRP.SuspendLayout();
			this.SuspendLayout();
			// 
			// AreaPNL
			// 
			this.AreaPNL.BackColor = System.Drawing.SystemColors.ControlLight;
			this.AreaPNL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AreaPNL.Controls.Add(this.QueriesGenGRP);
			this.AreaPNL.Controls.Add(this.OutputOpGRP);
			this.AreaPNL.Controls.Add(this.InstructionLBL);
			this.AreaPNL.Controls.Add(this.GenBTN);
			this.AreaPNL.Controls.Add(this.CloseBTN);
			this.AreaPNL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AreaPNL.Location = new System.Drawing.Point(0, 0);
			this.AreaPNL.Name = "AreaPNL";
			this.AreaPNL.Size = new System.Drawing.Size(404, 352);
			this.AreaPNL.TabIndex = 0;
			// 
			// QueriesGenGRP
			// 
			this.QueriesGenGRP.Controls.Add(this.AddQueryBTN);
			this.QueriesGenGRP.Controls.Add(this.QueriesGRP);
			this.QueriesGenGRP.Location = new System.Drawing.Point(8, 160);
			this.QueriesGenGRP.Name = "QueriesGenGRP";
			this.QueriesGenGRP.Size = new System.Drawing.Size(388, 164);
			this.QueriesGenGRP.TabIndex = 8;
			this.QueriesGenGRP.TabStop = false;
			this.QueriesGenGRP.Text = "Query Generation:  ";
			// 
			// AddQueryBTN
			// 
			this.AddQueryBTN.AutoSize = true;
			this.AddQueryBTN.Location = new System.Drawing.Point(324, 12);
			this.AddQueryBTN.Name = "AddQueryBTN";
			this.AddQueryBTN.Size = new System.Drawing.Size(58, 16);
			this.AddQueryBTN.TabIndex = 0;
			this.AddQueryBTN.TabStop = true;
			this.AddQueryBTN.Text = "Add Query";
			this.AddQueryBTN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddQueryBTN_LinkClicked);
			// 
			// QueriesGRP
			// 
			this.QueriesGRP.Controls.Add(this.QueriesPNL);
			this.QueriesGRP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.QueriesGRP.Location = new System.Drawing.Point(3, 16);
			this.QueriesGRP.Name = "QueriesGRP";
			this.QueriesGRP.Size = new System.Drawing.Size(382, 145);
			this.QueriesGRP.TabIndex = 1;
			this.QueriesGRP.TabStop = false;
			this.QueriesGRP.Text = "Queries: ";
			// 
			// QueriesPNL
			// 
			this.QueriesPNL.AutoScroll = true;
			this.QueriesPNL.AutoScrollMargin = new System.Drawing.Size(0, 1);
			this.QueriesPNL.BackColor = System.Drawing.SystemColors.ControlLight;
			this.QueriesPNL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.QueriesPNL.Location = new System.Drawing.Point(3, 16);
			this.QueriesPNL.Name = "QueriesPNL";
			this.QueriesPNL.Size = new System.Drawing.Size(376, 126);
			this.QueriesPNL.TabIndex = 0;
			// 
			// OutputOpGRP
			// 
			this.OutputOpGRP.BackColor = System.Drawing.SystemColors.ControlLight;
			this.OutputOpGRP.Controls.Add(this.FolderBTN);
			this.OutputOpGRP.Controls.Add(this.DataNsTXT);
			this.OutputOpGRP.Controls.Add(this.BusinessNsTXT);
			this.OutputOpGRP.Controls.Add(this.RootDirTXT);
			this.OutputOpGRP.Controls.Add(this.DataNsLBL);
			this.OutputOpGRP.Controls.Add(this.BusinessNsLBL);
			this.OutputOpGRP.Controls.Add(this.RootDirLBL);
			this.OutputOpGRP.Location = new System.Drawing.Point(4, 68);
			this.OutputOpGRP.Name = "OutputOpGRP";
			this.OutputOpGRP.Size = new System.Drawing.Size(392, 88);
			this.OutputOpGRP.TabIndex = 7;
			this.OutputOpGRP.TabStop = false;
			this.OutputOpGRP.Text = "Output Options:  ";
			// 
			// FolderBTN
			// 
			this.FolderBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.FolderBTN.Location = new System.Drawing.Point(364, 16);
			this.FolderBTN.Name = "FolderBTN";
			this.FolderBTN.Size = new System.Drawing.Size(24, 20);
			this.FolderBTN.TabIndex = 17;
			this.FolderBTN.Text = "...";
			this.FolderBTN.Click += new System.EventHandler(this.FolderBTN_Click);
			// 
			// DataNsTXT
			// 
			this.DataNsTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DataNsTXT.Location = new System.Drawing.Point(104, 64);
			this.DataNsTXT.Name = "DataNsTXT";
			this.DataNsTXT.Size = new System.Drawing.Size(282, 20);
			this.DataNsTXT.TabIndex = 12;
			this.DataNsTXT.Text = "";
			// 
			// BusinessNsTXT
			// 
			this.BusinessNsTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BusinessNsTXT.Location = new System.Drawing.Point(104, 40);
			this.BusinessNsTXT.Name = "BusinessNsTXT";
			this.BusinessNsTXT.Size = new System.Drawing.Size(282, 20);
			this.BusinessNsTXT.TabIndex = 11;
			this.BusinessNsTXT.Text = "";
			// 
			// RootDirTXT
			// 
			this.RootDirTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RootDirTXT.Enabled = false;
			this.RootDirTXT.Location = new System.Drawing.Point(104, 16);
			this.RootDirTXT.Name = "RootDirTXT";
			this.RootDirTXT.Size = new System.Drawing.Size(260, 20);
			this.RootDirTXT.TabIndex = 10;
			this.RootDirTXT.Text = "";
			// 
			// DataNsLBL
			// 
			this.DataNsLBL.AutoSize = true;
			this.DataNsLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DataNsLBL.Location = new System.Drawing.Point(22, 68);
			this.DataNsLBL.Name = "DataNsLBL";
			this.DataNsLBL.Size = new System.Drawing.Size(80, 14);
			this.DataNsLBL.TabIndex = 9;
			this.DataNsLBL.Text = "Data Namespace:";
			// 
			// BusinessNsLBL
			// 
			this.BusinessNsLBL.AutoSize = true;
			this.BusinessNsLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BusinessNsLBL.Location = new System.Drawing.Point(2, 44);
			this.BusinessNsLBL.Name = "BusinessNsLBL";
			this.BusinessNsLBL.Size = new System.Drawing.Size(99, 14);
			this.BusinessNsLBL.TabIndex = 8;
			this.BusinessNsLBL.Text = "Business Namespace:";
			// 
			// RootDirLBL
			// 
			this.RootDirLBL.AutoSize = true;
			this.RootDirLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RootDirLBL.Location = new System.Drawing.Point(46, 20);
			this.RootDirLBL.Name = "RootDirLBL";
			this.RootDirLBL.Size = new System.Drawing.Size(56, 14);
			this.RootDirLBL.TabIndex = 7;
			this.RootDirLBL.Text = "Root Folder:";
			// 
			// InstructionLBL
			// 
			this.InstructionLBL.Dock = System.Windows.Forms.DockStyle.Top;
			this.InstructionLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.InstructionLBL.Location = new System.Drawing.Point(0, 0);
			this.InstructionLBL.Name = "InstructionLBL";
			this.InstructionLBL.Size = new System.Drawing.Size(402, 64);
			this.InstructionLBL.TabIndex = 0;
			this.InstructionLBL.Text = "Instructions.";
			// 
			// GenBTN
			// 
			this.GenBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GenBTN.Location = new System.Drawing.Point(304, 324);
			this.GenBTN.Name = "GenBTN";
			this.GenBTN.Size = new System.Drawing.Size(80, 20);
			this.GenBTN.TabIndex = 15;
			this.GenBTN.Text = "&Generate";
			this.GenBTN.Click += new System.EventHandler(this.GenBTN_Click);
			// 
			// CloseBTN
			// 
			this.CloseBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CloseBTN.Location = new System.Drawing.Point(276, 324);
			this.CloseBTN.Name = "CloseBTN";
			this.CloseBTN.Size = new System.Drawing.Size(24, 20);
			this.CloseBTN.TabIndex = 16;
			this.CloseBTN.Text = "x";
			this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
			// 
			// Generator
			// 
			this.Controls.Add(this.AreaPNL);
			this.Name = "Generator";
			this.Size = new System.Drawing.Size(404, 352);
			this.Load += new System.EventHandler(this.Generator_Load);
			this.AreaPNL.ResumeLayout(false);
			this.QueriesGenGRP.ResumeLayout(false);
			this.QueriesGRP.ResumeLayout(false);
			this.OutputOpGRP.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		#region Add Query
		private void AddQueryBTN_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(null != table)
			{
				QueriesPNL.Controls.Add(new QueryGenerator(table, svr, qc));
				
				if(QueriesPNL.Controls.Count > 1)
					QueriesPNL.Controls[QueriesPNL.Controls.Count -1].Top = QueriesPNL.Controls[QueriesPNL.Controls.Count - 1].Height + QueriesPNL.Controls[QueriesPNL.Controls.Count - 2].Top;

				qc++;
			}
		}
		#endregion
		#region Close
		private void CloseBTN_Click(object sender, System.EventArgs e)
		{
			if(this.Parent is TabPage)
			{
				TabPage tp = (TabPage)this.Parent;
				TabControl tc = (TabControl)tp.Parent;
				if(null != tc)
				{
					tc.TabPages.Remove(tp);
				}
			}

		}
		#endregion
		#region Load
		private void Generator_Load(object sender, System.EventArgs e)
		{
			InstructionLBL.Text = "Configure the \"Root Folder\", \"Business Namespace\", and \"Data Namespace\" for your project structure.\r\n\r\nBasic CRUD will be create by default in the database and in the classes; to add aditional searches/quries to the database and classes use the Query Generator.";
			ToolTips.SetToolTip(FolderBTN, RootDirTXT.Text);

		}
		#endregion
		#region Generate
		private void GenBTN_Click(object sender, System.EventArgs e)
		{
			#region Easy Out
			if(null == RootDirTXT.Text || RootDirTXT.Text == "")
			{
				MessageBox.Show("A root directory must be provided for file output.");
				return;
			}

			if(null == BusinessNsTXT.Text || BusinessNsTXT.Text == "")
			{
				MessageBox.Show("A business namespace must be provided for file output.");
				return;
			}

			if(null == DataNsTXT.Text || DataNsTXT.Text == "")
			{
				MessageBox.Show("A data namespace must be provided for file output.");
				return;
			}
			#endregion

			string serial_dir = "";
			XmlTextWriter w = null;

			#region Save Output Data
			serial_dir = Common.PROJECT_PATH + "Serial\\" + table.TableCatalog + "\\" + "\\" + table.TableName + "\\";

			if(Directory.Exists(serial_dir) == false)
				Directory.CreateDirectory(serial_dir);

			if(File.Exists(serial_dir + table.TableName + ".oxml"))
				File.Delete(serial_dir + table.TableName + ".oxml");

			w = new XmlTextWriter(serial_dir + table.TableName + ".oxml", System.Text.Encoding.Default);

			w.Formatting = System.Xml.Formatting.Indented;
			w.Indentation = 4;
			w.IndentChar = ' ';

			w.WriteStartDocument(true);
			w.WriteDocType("OutputInfo", null, null, null);

			w.WriteStartElement("OutputData");

			w.WriteStartElement("Output-Folder");
			w.WriteString(RootDirTXT.Text);
			w.WriteEndElement();

			w.WriteStartElement("Business-Namespace");
			w.WriteString(BusinessNsTXT.Text);
			w.WriteEndElement();

			w.WriteStartElement("Data-Namespace");
			w.WriteString(DataNsTXT.Text);
			w.WriteEndElement();

			w.WriteEndElement();
			w.WriteEndDocument();

			w.Flush();
			w.Close();

			#endregion
			#region Save Query Data
			serial_dir = Common.PROJECT_PATH + "Serial\\" + table.TableCatalog + "\\" + table.TableName + "\\Query Data\\";
			
			if(!Directory.Exists(serial_dir))
			{
				Directory.CreateDirectory(serial_dir);
			}
			else
			{	//clear the old serial info
				string[] flz = Directory.GetFiles(serial_dir);

				for(int fi = 0; fi < flz.Length; fi++)
				{
					try
					{
						File.Delete(flz[fi]);
					}
					catch(System.IO.IOException ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
					}
				}
			}
			//add xml query state save

			int zl = QueriesPNL.Controls.Count.ToString().Length;
			int qn = 0;
			foreach(QueryGenerator q in QueriesPNL.Controls)
			{
				string n = "";
				for(int zn = 0; zn < zl - qn.ToString().Length; zn++)
					n += "0";

				n += qn.ToString();
				qn++;

				w = new XmlTextWriter(serial_dir + n + ".qxml", System.Text.Encoding.Default);

				w.Formatting = System.Xml.Formatting.Indented;
				w.Indentation = 4;
				w.IndentChar = ' ';

				w.WriteStartDocument(true);
				w.WriteDocType("Query", null, null, null);

				w.WriteStartElement("QueryInfo");
				w.WriteAttributeString("QueryName", q.QueryName);

				foreach(CheckBox chk in q.FieldChecks)
				{
					SqlDbTableField fld = null;

					if(null != chk.Tag)
						fld = (SqlDbTableField)chk.Tag;

					if(null != fld)
					{
						w.WriteStartElement("Field");
						w.WriteAttributeString("FieldName", fld.ColumnName);
						w.WriteAttributeString("IsFilter", (chk.Checked) ? "1" : "0");
						w.WriteEndElement();
					}
				}

				w.WriteStartElement("Find-Many");
				w.WriteString((q.FindMany) ? "1" : "0");
				w.WriteEndElement();

				w.WriteStartElement("Perform-Drop");
				w.WriteString((q.Drop) ? "1" : "0");
				w.WriteEndElement();
				
				w.WriteStartElement("Perform-Count");
				w.WriteString((q.Count) ? "1" : "0");
				w.WriteEndElement();

				w.WriteStartElement("Perform-Paged");
				w.WriteString((q.Paged) ? "1" : "0");
				w.WriteEndElement();

				w.WriteEndElement();
				w.WriteEndDocument();

				
				w.Flush();
				w.Close();
				
			}
			#endregion


			SqlInterfaceGenerator.GenerateTableInterface(table, Common.MDI.UseLanguage, BusinessNsTXT.Text, RootDirTXT.Text);
			//SqlInterfaceGenerator.GenerateTableFieldInterface(table, Common.MDI.UseLanguage, BusinessNsTXT.Text, RootDirTXT.Text);

			//write data class
			CodeCompileUnit datau = new CodeCompileUnit();
			CodeCompileUnit bccu = new CodeCompileUnit();
			CodeCompileUnit bcu = new CodeCompileUnit();

			CodeTypeDeclaration data = SqlDataObjectGenerator.GenerateTableClass(datau, table, DataNsTXT.Text);
			CodeTypeDeclaration bcc = SqlBusinessObjectCollectionGenerator.GenerateBusinessCollection(bccu, table, this.BusinessNsTXT.Text, DataNsTXT.Text);
			CodeTypeDeclaration bc = SqlBusinessObjectGenerator.GenerateBusiness(bcu, table, BusinessNsTXT.Text, DataNsTXT.Text);
			
			SqlConnectionSource conn = null;

			if(null == svr)
				svr = Common.MDI.ServerFromSelectedNode();

			if(svr.Integrated == true)
				conn = new SqlConnectionSource(svr.Server, table.TableCatalog);
			else
				conn = new SqlConnectionSource(svr.Server, table.TableCatalog,svr.User, svr.Password);
			
			string pk = "";
			SqlDbTableField pkFld = null;
			if(null != table.Constraints)
			{
				foreach(SqlDbTableConstraint tc in table.Constraints)
				{
					if(tc.IsPrimaryKey)
					{
						pk = tc.ConstraintColumnName;
						break;
					}
				}
			}

			if(null != table.Columns && pk != "")
			{
				for(int i = 0; i < table.Columns.Count; i++)
				{
					if(pk == table.Columns[i].ColumnName)
					{
						pkFld = table.Columns[i];
					}
				}
			}

			if(null != pkFld)
			{
				ArrayList fldA = new ArrayList();

				fldA.Add(pkFld);

				string sql_q = SqlStoredProcedureGenerator.CreateQuery(conn, table, fldA, table.TableName.Replace(" ", string.Empty) + "__FindBy" + pkFld.ColumnName, true, true);
				WriteSqlFile(sql_q,  table.TableName + "__FindBy" + pkFld.ColumnName);

                SqlDataObjectGenerator.GenerateTableQuery(data, table, fldA, this.DataNsTXT.Text, table.TableName.Replace(" ", string.Empty) + "__FindBy" + pkFld.ColumnName, false);
                SqlBusinessObjectGenerator.GenerateFindOne(bc, table, fldA, BusinessNsTXT.Text, DataNsTXT.Text, table.TableName.Replace(" ", string.Empty) + "__FindBy" + pkFld.ColumnName);
			}

			SqlStoredProcedureGenerator.CreateDelete(conn, table, true, true);
			SqlDataObjectGenerator.GenerateTableDelete(data, table);

			SqlStoredProcedureGenerator.CreateInsert(conn,table, true, true);
			SqlDataObjectGenerator.GenerateTableInsert(data, table);
			
			SqlStoredProcedureGenerator.CreateUpdate(conn,table, true, true);
			SqlDataObjectGenerator.GenerateTableUpdate(data, table);

			foreach(QueryGenerator q in this.QueriesPNL.Controls)
			{
				q.GenerateQuery(conn, data, DataNsTXT.Text,bc, BusinessNsTXT.Text, this.RootDirTXT.Text);
			}

			SqlObjectWriter.WriteTableDataClass(datau, Common.MDI.UseLanguage, table, DataNsTXT.Text, RootDirTXT.Text);
			SqlObjectWriter.WriteTableBusinessClass(bcu, Common.MDI.UseLanguage, table, BusinessNsTXT.Text, RootDirTXT.Text);
			SqlObjectWriter.WriteTableBusinessCollectionClass(bccu, Common.MDI.UseLanguage, table, BusinessNsTXT.Text, RootDirTXT.Text);

			//close tab
			if(this.Parent is TabPage)
			{
				TabPage tp = (TabPage)this.Parent;
				TabControl tc = (TabControl)tp.Parent;
				if(null != tc)
				{
					tc.TabPages.Remove(tp);
				}
			}

		}
		#endregion
		#region Keep Query List Neat
		private void QueriesPNL_ControlRemoved(object sender, ControlEventArgs e)
		{
			QueriesPNL.AutoScroll = false;
			for(int i = 0; i < QueriesPNL.Controls.Count; i++)
			{
				if(i == 0)
					QueriesPNL.Controls[i].Top = 0;
				else
					QueriesPNL.Controls[i].Top = QueriesPNL.Controls[i].Height * i;
			}
			QueriesPNL.AutoScroll = true;
		}
		#endregion

		private void FolderBTN_Click(object sender, System.EventArgs e)
		{
			BrowseFDLG.SelectedPath = RootDirTXT.Text;
			if(BrowseFDLG.ShowDialog(Common.MDI) == DialogResult.OK)
				RootDirTXT.Text = BrowseFDLG.SelectedPath + "\\";

			ToolTips.SetToolTip(FolderBTN, RootDirTXT.Text);
		}

		void WriteSqlFile(string sql, string procName)
		{
			string path = RootDirTXT.Text;
			path += this.BusinessNsTXT.Text.Substring(0, this.BusinessNsTXT.Text.IndexOf("."));

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
				file = File.OpenWrite(path);

			foreach(char c in sql)
				file.WriteByte(System.Convert.ToByte(c));

			file.Close();
		}

	}
}
