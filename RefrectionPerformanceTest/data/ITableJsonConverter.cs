using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrectionPerformanceTest.data
{
    public interface ITableJsonConverter
    {
        public object ToJson(object source);

        public object ToTableEntity(object source);

    }
}
