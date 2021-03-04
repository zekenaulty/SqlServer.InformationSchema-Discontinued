using System;
using System.IO;
using System.Reflection;

namespace SqlServer.Companion.Resx
{
	/// <summary>
	/// Summary description for Read.
	/// </summary>
	internal class Reader
	{
		/// <summary>
		/// Instance the read class.
		/// </summary>
		internal Reader(){}
		/// <summary>
		/// Read an internal resource as a string.
		/// </summary>
		/// <param name="name">The fully qualified name of the resource, i.e. "_root.foo.XmlTemplates.Flyers.Jazz".</param>
		/// <returns>System.String.</returns>
		internal string ResourceString(string name)
		{
			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
			string s = "";
			if(null != stream)
			{
				for(int i =0; i < stream.Length; i++)
				{
					s += (char)stream.ReadByte();
				}
				stream.Close();
				stream = null;
			}
			return s;
		}
	}
}
