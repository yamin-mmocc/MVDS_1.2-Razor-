using Microsoft.EntityFrameworkCore;
using MimovitalRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimovitalRazor.DataAccess
{
    public class Repository : IPostRepository
    {
        private readonly ApplicationContext _context;
        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        // MVD001_Search_検索処理
        public async Task<List<CSVDataMaster>> SearchData(string babyid, int bodyCheckflag, int heartCheckflag, int respCheckflag)
        {
            // babyid以外がunmapped propertyのため、初めにbabyidのみの検索リストを取得
            var babyIdResult = await _context.CSVDataMaster
                    .Where(x => x.babyID == babyid)
                    .ToListAsync();

            // unmapped propertyでfilteringする
            var result = babyIdResult
                    .Where(x => x.bodyResult == bodyCheckflag && x.heartResult == heartCheckflag && x.respResult == respCheckflag)
                    .ToList();

            return result;
        }

        // 修正中
        // MVD002_AbnormalDataTotalByDate_集計処理
        public async Task<List<AbnormalDataTotalByDate>> AbnormalDataTotalByDate(string babyid)
        {
            // 初期化
            List<AbnormalDataTotalByDate> abnormalDataList = new List<AbnormalDataTotalByDate>();
            var groupbyResult = new List<CSVDataMaster>();

            // babyidのみの検索リストを取得
            var result = await _context.CSVDataMaster.Where(x => x.babyID == babyid).ToListAsync();

            // 集計処理
            if (result != null)
            {
                // 日付のみリストを抽出
                var uniqueDate = result
                                  .Select(p => p.occurDate.Date)
                                  .Distinct();

                // 日付リストをルーピングする
                foreach (var d in uniqueDate)
                {
                    // 同じ日付のデータをCSVDataMasterからセレクトする
                    groupbyResult = ((from u in result
                                      where u.occurDate.Date == d.Date.Date
                                      select new CSVDataMaster
                                      {
                                          babyID = u.babyID,
                                          occurDate = u.occurDate,
                                          levelRespirationL = u.levelRespirationL,
                                          minRespiration = u.minRespiration,
                                          levelLearnedRespirationL = u.levelLearnedRespirationL,
                                          levelBodyMotionL = u.levelBodyMotionL,
                                          minBodyMotion = u.minBodyMotion,
                                          levelLearnedBodyMotionL = u.levelLearnedBodyMotionL,
                                          levelHeartL = u.levelHeartL,
                                          minlHeart = u.minlHeart,
                                          levelLearnedHeartL = u.levelLearnedHeartL
                                      })).ToList();

                    // 集計カウントする
                    var oktotal = groupbyResult.Where(x => x.bodyResult == 0 && x.heartResult == 0 && x.respResult == 0).ToList<CSVDataMaster>();
                    var btotal = groupbyResult.Where(x => x.bodyResult == 1).ToList<CSVDataMaster>();
                    var htotal = groupbyResult.Where(x => x.heartResult == 1).ToList<CSVDataMaster>();
                    var rtotal = groupbyResult.Where(x => x.respResult == 1).ToList<CSVDataMaster>();

                    if (btotal.Count != 0 || htotal.Count != 0 || rtotal.Count != 0)
                    {
                        AbnormalDataTotalByDate data = new AbnormalDataTotalByDate
                        {
                            babyID = babyid,
                            riskDate = d.Date,
                            okByDate = oktotal.Count,
                            bodyMovementNGByDate = btotal.Count,
                            heartRateNGByDate = htotal.Count,
                            respirationNGByDate = rtotal.Count
                        };

                        abnormalDataList.Add(data);
                    }
                }
            }
            return abnormalDataList;
        }

        // MVD003_AbnormalDataDetails_詳細処理
        public async Task<List<CSVDataMaster>> AbnormalDataDetails(string babyid, string riskDate)
        {
            //条件で検索
            var babyIdResult = await _context.CSVDataMaster
                    .Where(x => x.babyID == babyid && x.occurDate.Date == Convert.ToDateTime(riskDate))
                    .ToListAsync();

            // unmapped propertyでfilteringする
            var result = babyIdResult
                    .Where(x => x.btnAbnormalFlag == 1)
                    .ToList();

            return result;
        }
    }
}
