using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

namespace SqlServer.Companion.Console
{
	/// <summary>Console Shell that can be used to test the functionality of this Assembly.</summary>
	public sealed class Shell : BaseConsole
	{
		SqlConnectionSource conn = null;
		#region Construction
		public Shell()
		{
			//add main menu and init
			char[] keys = new char[]{'1','2','3','4','5','6','x'};
			string[] captions = new string[]{
												"Enter ConnectionString",	//1
												"Test ConnectionString",	//2
												"Query Check Contraints",	//3
												"Query Tables",				//4
												"Query Views",				//5
												"Query Routines",			//6
												"Exit"						//x
											};

			AddMenuItems("Main Menu", keys, captions);

			//add more..?

		}
		#endregion
		#region Message Loop
		public void BeginLoop()
		{
			WriteMainMenu();
		}
		void WriteExit()
		{
			WriteSpacer(4);
			Write("|--<->-<>------------------------------< Press enter/return to exit. >");
			Read();
		}
		#endregion
		#region AddMenuItem
		Hashtable menu_captions = new Hashtable();
		StringDictionary menu_keys = new StringDictionary();
		void AddMenuItem(string menu, char key, string caption)
		{
			string s = key.ToString().ToLower();

			if(!menu_keys.ContainsKey(menu))
				menu_keys.Add(menu, "|" + s + "|");
			else if(menu_keys[menu].IndexOf("|" + s + "|") == -1)
				menu_keys[menu] += s + "|";

			if(!menu_captions.Contains(menu))
				menu_captions.Add(menu, new StringDictionary());

			((StringDictionary)menu_captions[menu]).Add(s, caption);
		}
		#endregion
		#region AddMenuItems
		void AddMenuItems(string menu, char[] keys, string[] captions)
		{
			for(int i = 0; i < keys.Length; i++)
				AddMenuItem(menu, keys[i], captions[i]);
		}
		#endregion
		#region MainMenuProc
		void MainMenuProc(string key)
		{
			switch(key)
			{
				case "1":
					ReadConnectionString();
					WriteMainMenu();
					break;
				case "2":
					if(null == conn)
						ReadConnectionString();

					tested = conn.IsValid;
					if(tested)
						WriteLine("|--< >-<> Test Connection Succeded!");
					else
						WriteLine("|--< >-<> Test Connection Failed!");

					Write("|--< >-<> Press enter/return. ");
					Read();

					WriteMainMenu();
					break;
					//case "3":
					//	break;
					//case "4":
					//	break;
					//case "5":
					//	break;
				case "x":
					WriteExit();
					break;
				default:
					WriteMainMenu();
					break;
			}
		}
		#endregion
		#region Write
		#region WriteMainMenu
		bool tested = false;
		void WriteMainMenu()
		{
			WriteSpacer(2);
			WriteLine("|--<->-<>-------------------------< SqlServer.Companion.dll Test App >");
			if(null != conn)
			{
				WriteSpacer();
				WriteLine("|--< >-<> Server: " + conn.Server);
				WriteLine("|--< >-<> Database: " + conn.Db);
				if(tested)
					WriteLine("|--< >-<> [Connection Is Valid]");

				WriteSpacer();
			}
			WriteMenu("Main Menu");
		}
		#endregion
		#region WriteMenu
		void WriteMenu(string menu)
		{
			WriteLine("|--<->-<>------------------------------------------------< " + menu + " >");
			WriteSpacer();
			WriteMenuItems(menu);
			WriteSpacer(2);
			Write("|--<->-<>--------------< Enter a menu number and press enter/return. > "); 
			ReadMenu(menu);
		}
		#endregion
		#region WriteMenuItem
		void WriteMenuItem(string menu, char key)
		{
			string s = key.ToString().ToLower();

			if(menu_captions.Contains(menu) && ((StringDictionary)menu_captions[menu]).ContainsKey(s))
			{
				WriteLine("|--<" + s + ">-<> " + ((StringDictionary)menu_captions[menu])[s]);
				WriteSpacer();
			}
		}
		#endregion
		#region WriteMenuItems
		void WriteMenuItems(string menu)
		{
			if(menu_captions.Contains(menu))
			{
				StringDictionary mc = (StringDictionary)menu_captions[menu];
				
				IEnumerator k = mc.Keys.GetEnumerator();
				
				while(k.MoveNext())
				{
					char c = char.Parse(k.Current.ToString());
					WriteMenuItem(menu, c);
				}
				
			}
		}
		#endregion
		#region WriteCap
		void WriteCap(){WriteLine("|--<->-<>-----------------------------------------------------------<>");}
		#endregion
		#region WriteSpacer
		void WriteSpacer(){WriteLine("|--< >-<>");}
		void WriteSpacer(int count){for(int i =0; i < count; i++) WriteSpacer();}
		void WriteLine(string text, bool top_spacer, bool bottom_spacer)
		{
			if(top_spacer)
				WriteSpacer();

			WriteLine(text);

			if(bottom_spacer)
				WriteSpacer();
		}
		#endregion
		#region WriteMainMenuError
		void WriteMainMenuError()
		{
			WriteLine("|--<->-<>--<Invalid try again.>", true, true);
			WriteMainMenu();
		}
		#endregion
		#region WriteMainMenuChoice
		void WriteMainMenuChoice(string n_key)
		{
			if(menu_keys.ContainsValue(n_key))
				WriteLine("|--<->-<>--< Selected >-<" + n_key + ">-< " + menu_keys[n_key].ToString() + " > ", true, true);
		}
		#endregion
		#endregion
		#region Read
		#region ReadMenu
		void ReadMenu(string menu)
		{
			string mi = ReadLine();
			if(menu_keys.ContainsKey(menu))
			{
				if(menu_keys[menu].IndexOf("|" + mi + "|") != -1)
				{
					WriteMainMenuChoice(mi);
					MainMenuProc(mi);
					return;
				}
			}
			WriteMainMenuError();
		}
		#endregion
		#region ReadConnectionString
		void WriteConnStrError()
		{

		}
		void ReadConnectionString()
		{
			if(null == conn)
				conn = new SqlConnectionSource();

			string s = string.Empty;
			string server = string.Empty;
			string db = string.Empty;
			string user = string.Empty;
			string password = string.Empty;
			string ex = string.Empty;

			WriteSpacer(3);
			Write("|--< >-<> Use Integrated Security? Y/N ");
			s = Read().ToString();
			s = s.ToUpper();
			WriteSpacer(3);
			if(s == "Y")
			{
				

				Write("|--< >-<> Server: ");
				server = ReadInput();
				conn.Server = server;
				WriteSpacer();

				Write("|--< >-<> Database: ");
				db = ReadInput();
				conn.Db = db;
				WriteSpacer();

			}
			else if(s == "N")
			{
				

				Write("|--< >-<> Server: ");//get the server
				server = ReadInput();
				conn.Server = server;
				WriteSpacer();

				Write("|--< >-<> Database: ");
				db = ReadInput();
				conn.Db = db;
				WriteSpacer();

				Write("|--< >-<> User: ");
				user = ReadInput();
				conn.User = user;
				WriteSpacer();

				Write("|--< >-<> Password: ");
				password = ReadInput();
				conn.Password = password;

			}
			else
			{
				WriteLine("|--<->-<>--<Invalid try again.>", true, true);
				ReadConnectionString();
			}
		}
		#endregion
		#endregion
	}
}