using MimovitalRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimovitalRazor.DataAccess
{
    public interface IPostRepository
    {
        Task<List<CSVDataMaster>> SearchData(string babyid, int bodyCheckflag, int heartCheckflag, int respCheckflag);
        Task<List<AbnormalDataTotalByDate>> AbnormalDataTotalByDate(string babyid);
        Task<List<CSVDataMaster>> AbnormalDataDetails(string babyid, string riskDate);
    }
}
