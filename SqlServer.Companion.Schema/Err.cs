using System;

namespace SqlServer.Companion
{
	/// <summary>Common methods to throw errors.</summary>
	internal sealed class Err
	{
		public static void ThrowErr(string msg){throw new Exception(msg);}
		public static void ThrowErr(string msg, Exception inner){throw new Exception(msg, inner);}
	}
}
