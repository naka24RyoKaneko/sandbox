using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrectionPerformanceTest.data
{
    public static class PostCodeTableConverter 
    {
        public static object ToJson(object source)
        {
            var current = (PostCodeTable)source;
            var dest = new PostCodeJson
            {
                Position = current.Position,
                Post5 = current.Post5,
                Post7 = current.Post7,
                prefkana = current.prefkana,
                citykana = current.citykana,
                townkana = current.townkana,
                pref = current.pref,
                city = current.city,
                town = current.town,
                kbn1 = current.kbn1,
                kbn2 = current.kbn2,
                kbn3 = current.kbn3,
                kbn4 = current.kbn4,
                kbn5 = current.kbn5,
                kbn6 = current.kbn6
            };
            return dest;
        }

        public static object ToTableEntity(object source)
        {
            var current = (PostCodeJson)source;
            var dest = new PostCodeTable()
            {
                Position = current.Position,
                Post5 = current.Post5,
                Post7 = current.Post7,
                prefkana = current.prefkana,
                citykana = current.citykana,
                townkana = current.townkana,
                pref = current.pref,
                city = current.city,
                town = current.town,
                kbn1 = current.kbn1,
                kbn2 = current.kbn2,
                kbn3 = current.kbn3,
                kbn4 = current.kbn4,
                kbn5 = current.kbn5,
                kbn6 = current.kbn6
            };
            return dest;
        }
    }
}
