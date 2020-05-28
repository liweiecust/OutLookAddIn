using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public class CsvResultItem
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Result { get; set; }

        public string Note { get; set; }
    }
}
