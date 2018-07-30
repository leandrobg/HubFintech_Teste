using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Web;

namespace Teste_HubFintech.Data
{
    public class ConnectionFactory
    {
        static string mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Database") + @"\dbHubFintech.db";

        public SQLiteConnection conn = new SQLiteConnection("Data Source=" + mappedPath);
    }

}
