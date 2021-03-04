using System;

namespace SqlServer.Companion
{
	/// <summary>Common methods to throw errors.</summary>
	internal sealed class Err
	{
		public static void ThrowErr(string msg){throw new System.Exception(msg);}
		public static void ThrowErr(string msg, System.Exception inner){throw new System.Exception(msg, inner);}
	}
}
