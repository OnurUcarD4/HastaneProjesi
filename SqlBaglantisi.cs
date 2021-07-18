using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HastaneProjesi
{
    class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source = DESKTOP-Q0J7R6D\\SQLEXPRESS; Initial Catalog = HastaneProje; Integrated Security = True");
            baglan.Open();
            return baglan;
        }
    }
}
