using Sys = System;
using System.IO;
using System.Text;

namespace SqlServer.Companion.Console
{
	/// <summary>Inherit from this class to provide basic console helper method to your class.</summary>
	public abstract class BaseConsole
	{
		#region Read Helpers
		/// <summary>
		/// Read a single character.
		/// </summary>
		/// <returns>Char that was read.</returns>
		protected char Read(){return Sys.Convert.ToChar(Sys.Console.Read());}
		/// <summary>
		/// Read a line of text input from the Sys.Console.
		/// </summary>
		/// <returns>The text that was read.</returns>
		protected string ReadLine(){return Sys.Console.ReadLine();}
		protected string ReadInput()
		{
			string s = string.Empty;;
			StringBuilder sb = new StringBuilder();
			int i = 0;

			while(s != "\r")
			{
				s = Read().ToString();

				if( i == 0 && s == "\r")
					s = string.Empty;

				sb.Append(s);
				i++;
			}//ugly nasty format fix
			return sb.ToString().Replace("\r", "").Replace("\n", "");
		}
		#endregion
		#region Write Helpers
		/// <summary>
		/// Write text to the Sys.Console.
		/// </summary>
		/// <param name="text">Text to be written.</param>
		protected void Write(string text){Sys.Console.Write(text);}
		/// <summary>
		/// Write a blank line to the Sys.Console.
		/// </summary>
		protected void WriteLine(){WriteLine(string.Empty);}
		/// <summary>
		/// Write a line of text to the Sys.Console.
		/// </summary>
		/// <param name="text">Text to be written.</param>
		protected void WriteLine(string text){Sys.Console.WriteLine(text);}
		/// <summary>
		/// Write a series of consecutive blank lines to the Sys.Console.
		/// </summary>
		/// <param name="lines">The number of blank lines to be written.</param>
		protected void WriteLine(int lines){for(int i = 0; i < lines; i++)WriteLine();}
		/// <summary>
		/// Write text to the console multi times. Text is write per for the numer of lines.
		/// </summary>
		/// <param name="text">Text to write</param>
		/// <param name="lines">The number of blank lines to be written.</param>
		protected void WriteLine(string text, int lines){for(int i = 0; i < lines; i++)WriteLine(text);}
		#endregion
	}
}