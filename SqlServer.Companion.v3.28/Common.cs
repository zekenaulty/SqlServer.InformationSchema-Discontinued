using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlServer.Companion
{
	public class Common
	{
		public static MDIMain MDI;
		public static void Main()
		{
			MDI = new MDIMain();
			Application.Run(MDI);
		}
	}
}
