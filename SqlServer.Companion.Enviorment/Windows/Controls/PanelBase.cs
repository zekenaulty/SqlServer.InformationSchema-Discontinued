using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SqlServer.Companion.Enviorment.Windows.Controls
{
	/// <summary>
	/// Summary description for PanelBase.
	/// </summary>
	public class PanelBase : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PanelBase()
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
			// 
			// PanelBase
			// 
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.Name = "PanelBase";
			this.Size = new System.Drawing.Size(48, 44);

		}
		#endregion

		protected override void OnLoad(EventArgs e)
		{
			Max();
			base.OnLoad(e);
		}

		protected void Max()
		{
			if(this.Parent is TabPage)
			{
				TabControl tc = (TabControl)this.Parent.Parent;

				if(null != tc)
				{

					this.Width = tc.DisplayRectangle.Width;
					this.Height = tc.DisplayRectangle.Height;
				}
			}
			else
			{
				this.Width = this.Parent.Width;
				this.Height = this.Parent.Height;
			}
		}

		protected override void OnParentChanged(System.EventArgs e)
		{
			Max();
			base.OnParentChanged(e);
		}

	}
}
