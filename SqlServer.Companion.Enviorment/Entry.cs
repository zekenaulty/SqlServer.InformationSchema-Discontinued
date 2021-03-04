using System;

namespace SqlServer.Companion.Enviorment
{
	/// <summary>Runs the application.</summary>
	public class Entry
	{
		/// <summary>
		/// Application entry point.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			System.Windows.Forms.Application.Run(Common.TI);
		}
	}
}
