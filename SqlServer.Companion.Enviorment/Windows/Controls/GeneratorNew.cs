using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SqlServer.Companion.Enviorment.Windows.Controls
{
	/// <summary>
	/// Summary description for GeneratorNew.
	/// </summary>
	public class GeneratorNew : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TabControl TableObjectTAB;
		private System.Windows.Forms.TabPage FileOutputTP;
		private System.Windows.Forms.TabPage QueriesTP;
		private System.Windows.Forms.TabPage OneToOneFkTP;
		private System.Windows.Forms.TabPage OneToManyFkTP;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GeneratorNew()
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
			this.TableObjectTAB = new System.Windows.Forms.TabControl();
			this.FileOutputTP = new System.Windows.Forms.TabPage();
			this.QueriesTP = new System.Windows.Forms.TabPage();
			this.OneToOneFkTP = new System.Windows.Forms.TabPage();
			this.OneToManyFkTP = new System.Windows.Forms.TabPage();
			this.TableObjectTAB.SuspendLayout();
			this.SuspendLayout();
			// 
			// TableObjectTAB
			// 
			this.TableObjectTAB.Controls.Add(this.FileOutputTP);
			this.TableObjectTAB.Controls.Add(this.QueriesTP);
			this.TableObjectTAB.Controls.Add(this.OneToOneFkTP);
			this.TableObjectTAB.Controls.Add(this.OneToManyFkTP);
			this.TableObjectTAB.ItemSize = new System.Drawing.Size(100, 18);
			this.TableObjectTAB.Location = new System.Drawing.Point(12, 120);
			this.TableObjectTAB.Multiline = true;
			this.TableObjectTAB.Name = "TableObjectTAB";
			this.TableObjectTAB.SelectedIndex = 0;
			this.TableObjectTAB.ShowToolTips = true;
			this.TableObjectTAB.Size = new System.Drawing.Size(516, 396);
			this.TableObjectTAB.TabIndex = 0;
			// 
			// FileOutputTP
			// 
			this.FileOutputTP.Location = new System.Drawing.Point(4, 22);
			this.FileOutputTP.Name = "FileOutputTP";
			this.FileOutputTP.Size = new System.Drawing.Size(508, 370);
			this.FileOutputTP.TabIndex = 0;
			this.FileOutputTP.Text = "File Output";
			// 
			// QueriesTP
			// 
			this.QueriesTP.Location = new System.Drawing.Point(4, 22);
			this.QueriesTP.Name = "QueriesTP";
			this.QueriesTP.Size = new System.Drawing.Size(536, 598);
			this.QueriesTP.TabIndex = 1;
			this.QueriesTP.Text = "Table Queries";
			// 
			// OneToOneFkTP
			// 
			this.OneToOneFkTP.Location = new System.Drawing.Point(4, 22);
			this.OneToOneFkTP.Name = "OneToOneFkTP";
			this.OneToOneFkTP.Size = new System.Drawing.Size(536, 598);
			this.OneToOneFkTP.TabIndex = 2;
			this.OneToOneFkTP.Text = "1 to 1";
			// 
			// OneToManyFkTP
			// 
			this.OneToManyFkTP.Location = new System.Drawing.Point(4, 22);
			this.OneToManyFkTP.Name = "OneToManyFkTP";
			this.OneToManyFkTP.Size = new System.Drawing.Size(536, 598);
			this.OneToManyFkTP.TabIndex = 3;
			this.OneToManyFkTP.Text = "1 to *";
			// 
			// GeneratorNew
			// 
			this.Controls.Add(this.TableObjectTAB);
			this.Name = "GeneratorNew";
			this.Size = new System.Drawing.Size(544, 624);
			this.TableObjectTAB.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
