using System;

namespace Test
{

    
	/// <summary>
	/// Summary description for News.
	/// </summary>
	public class News : SqlDevTest.Data.News
	{

        public event EventHandler foo;

		new public static System.Collections.ArrayList News__FindAll(SqlServer.Companion.SqlConnectionSource conn)
		{
			System.Collections.ArrayList ret = SqlDevTest.Data.News.News__FindAll(conn);

			for(int i = 0; i < ret.Count; i++)
			{
				News n = new News();
				SqlDevTest.Data.News nn = (SqlDevTest.Data.News)ret[i];
				n._isDirty = nn.IsDirty;
				n._NewsCaption = nn.NewsCaption;
				n._NewsID = nn.NewsID;
				n._NewsText = nn.NewsText;
				ret[i] = n;
			}
										
			return ret;
		}


        void foo_bar()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            txt.TextChanged += (s, e) =>
            {
                
            };
        }

        void fo(Action<object> act)
        {
            act(null);
        }

        void bar()
        {
            fo((o) => { o = null; });
        }

	}

}
