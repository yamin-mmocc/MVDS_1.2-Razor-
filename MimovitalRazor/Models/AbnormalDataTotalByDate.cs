using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimovitalRazor.Models
{
    public class AbnormalDataTotalByDate
    {
        public string babyID { get; set; }
        public DateTime riskDate { get; set; }
        public int okByDate { get; set; }
        public int bodyMovementNGByDate { get; set; }
        public int heartRateNGByDate { get; set; }
        public int respirationNGByDate { get; set; }
    }
}
