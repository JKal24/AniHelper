using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniHelper.AniClasses
{
    public class Helper
    {
        public String cnnAnime(String genre)
        {
            return ConfigurationManager.ConnectionStrings[genre].ConnectionString;
        }

    }
}
