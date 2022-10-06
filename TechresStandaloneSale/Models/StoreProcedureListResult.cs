using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechresStandaloneSale.Models
{
    public class StoreProcedureListResult<T, Long>
    {

        public Long TotalRecord;
        public List<T> Result;
        public StoreProcedureListResult()
        {

        }
        public StoreProcedureListResult( List<T> result, Long totalRecord)
        {
            Result = result;
            TotalRecord = totalRecord;
        }

    }
}
