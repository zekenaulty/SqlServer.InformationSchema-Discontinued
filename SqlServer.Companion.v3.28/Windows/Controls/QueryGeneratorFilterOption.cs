using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SqlServer.Companion.Windows.Controls
{
	/// <summary>
	/// Summary description for QueryGeneratorFilterOption.
	/// </summary>
	public class QueryGeneratorFilterOption : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ComboBox OperatorDDL;
		private System.Windows.Forms.Label OperatorLBL;
		private System.Windows.Forms.LinkLabel FieldInfoLNK;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public QueryGeneratorFilterOption()
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
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.OperatorLBL = new System.Windows.Forms.Label();
			this.OperatorDDL = new System.Windows.Forms.ComboBox();
			this.FieldInfoLNK = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(8, 4);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(444, 20);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Filter Table Field [%fld%]";
			// 
			// OperatorLBL
			// 
			this.OperatorLBL.Location = new System.Drawing.Point(4, 32);
			this.OperatorLBL.Name = "OperatorLBL";
			this.OperatorLBL.Size = new System.Drawing.Size(124, 16);
			this.OperatorLBL.TabIndex = 1;
			this.OperatorLBL.Text = "Comparison Operator:";
			// 
			// OperatorDDL
			// 
			this.OperatorDDL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OperatorDDL.Items.AddRange(new object[] {
															 "Equal ( = )",
															 "Not Equal ( != )",
															 "Less Than ( < )",
															 "Greater Than ( > )",
															 "Less Than Equal ( <= )",
															 "Greater Than Equal ( >= )",
															 "Like ( LIKE )"});
			this.OperatorDDL.Location = new System.Drawing.Point(128, 28);
			this.OperatorDDL.Name = "OperatorDDL";
			this.OperatorDDL.Size = new System.Drawing.Size(212, 21);
			this.OperatorDDL.TabIndex = 2;
			// 
			// FieldInfoLNK
			// 
			this.FieldInfoLNK.AutoSize = true;
			this.FieldInfoLNK.Location = new System.Drawing.Point(356, 32);
			this.FieldInfoLNK.Name = "FieldInfoLNK";
			this.FieldInfoLNK.Size = new System.Drawing.Size(89, 16);
			this.FieldInfoLNK.TabIndex = 3;
			this.FieldInfoLNK.TabStop = true;
			this.FieldInfoLNK.Text = "Field Information";
			// 
			// QueryGeneratorFilterOption
			// 
			this.Controls.Add(this.FieldInfoLNK);
			this.Controls.Add(this.OperatorDDL);
			this.Controls.Add(this.OperatorLBL);
			this.Controls.Add(this.checkBox1);
			this.Name = "QueryGeneratorFilterOption";
			this.Size = new System.Drawing.Size(460, 56);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
