using System;

namespace SqlServer.Companion.Exceptions
{
	/// <summary>
	/// Summary description for BuildException.
	/// </summary>
	public class BuildException : ExceptionBase
	{
		public BuildException() : base("A build error has occured while attempting to ready from the database."){}
		public BuildException(Exception inner) : base("A build error has occured while attempting to ready from the database. Developers for more detailed information view the inner exception information.", inner){}
	}
}
