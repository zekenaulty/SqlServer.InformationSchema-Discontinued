using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

using SqlServer.Companion;
using SqlServer.Companion.Schema;

namespace SqlServer.Companion.Enviorment
{
	public enum Source
	{
		Default,
		Enviorment,
		Tray
	}

	/// <summary>
	/// Common global constants, methods and settings.
	/// </summary>
	internal sealed class Common
	{
		internal sealed class Settings
		{
			static Settings()
			{

			}
			/// <summary>
			/// Create a new project using integrated security.
			/// </summary>
			/// <param name="server">The server the project is located on.</param>
			/// <param name="catalog">the database that is being worked with.</param>
			internal static void NewProject(string server, string catalog)
			{
				XmlTextWriter xout = new XmlTextWriter(PROJECT_PATH + Guid.NewGuid().ToString() + ".dprj", System.Text.Encoding.Default);

				
			}
			/// <summary>
			/// Create a new project.
			/// </summary>
			/// <param name="server">The server the project is located on.</param>
			/// <param name="catalog">the database that is being worked with.</param>
			/// <param name="user">The user name to be used when connecting.</param>
			/// <param name="password">The password being used when connectin.</param>
			internal static void NewProject(string server, string catalog, string user, string password)
			{

			}
		}
		/// <summary>
		/// Intialize static members and properties.
		/// </summary>
		static Common()
		{
			int trim_at = -1;

			string tmp = string.Empty;
			string root = string.Empty;

			#region Find ROOT
			//determine where to remove chars at
			tmp =  System.Windows.Forms.Application.ExecutablePath;
			if(tmp.IndexOf("bin\\Debug") != -1)			{trim_at = tmp.IndexOf("bin\\Debug");}
			else if(tmp.IndexOf("bin\\Release") != -1)	{trim_at = tmp.IndexOf("bin\\Release");}
			else if(tmp.IndexOf("bin") != -1)			{trim_at = tmp.IndexOf("bin");}
			else
			{
				string dir = "SqlServer.Companion.Enviorment";
				int len = dir.Length;

				if(tmp.IndexOf(dir) != -1)
					trim_at = tmp.IndexOf(dir) + len + 1;

				//extend later to handle dynamic install directory...
				//use a recursive directory walk out with a check for file Exists
				//use App.config as check file....
			}
			root = tmp.Substring(0, trim_at);
			tmp = string.Empty;
			trim_at = -1;
			#endregion

			//hope we found the path and tak on the projects dir
			PROJECT_PATH = root + "Projects\\";

		}
		/// <summary>
		/// Program Icon stored in the Windows System Tray...
		/// </summary>
		internal static Windows.TrayIcon TI = new Windows.TrayIcon();
		/// <summary>
		/// Project Enviorment.
		/// </summary>
		internal static readonly Windows.MDI MDI = new Windows.MDI(TI);
		/// <summary>
		/// Location of project files.
		/// </summary>
		internal static readonly string PROJECT_PATH = string.Empty;
		/// <summary>
		/// Suspend execution of the calling application for the specifed TimeSpan.
		/// </summary>
		/// <param name="duration">The amount of time the appliation will be paused.</param>
		internal static void Pause(TimeSpan duration)
		{
			DateTime stop_at = DateTime.Now.Add(duration);
			while(stop_at > DateTime.Now)
				System.Windows.Forms.Application.DoEvents();
		}
	}
}
