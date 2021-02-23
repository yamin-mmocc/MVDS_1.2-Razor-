using Microsoft.AspNetCore.Mvc;
using MimovitalRazor.DataAccess;
using MimovitalRazor.Models;
using MimovitalRazor.Properties;
using MimovitalRazor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimovitalRazor.Controllers
{
    public class CSVDataMasterController : Controller
    {
        private readonly IPostRepository _postRepository;

        public CSVDataMasterController(IPostRepository postRepository)
        {
            _postRepository = postRepository;

        }
        public IActionResult Index()
        {
            this.ViewBag.Status = 0;
            return View("~/Views/DataSearchList.cshtml");
        }

        public async Task<IActionResult> Search(CSVDataViewModel csvViewModel)
        {
            CSVDataViewModel csvDataSearchModel = new CSVDataViewModel();
            try
            {                
                if (ModelState.IsValid)
                {
                    csvViewModel.setPropertiesForSearch(csvDataSearchModel);
                    List<CSVDataMaster> searchdata = await _postRepository.SearchData(csvDataSearchModel.babyID, csvDataSearchModel.bodyCheck, csvDataSearchModel.heartCheck, csvDataSearchModel.respCheck);
                    if (searchdata.Count == 0)
                    {
                        this.ViewBag.errorMsg = Resource.SYS01E001;
                    }
                    else
                    {
                        this.ViewBag.Status = 1;
                        this.ViewBag.SearchDataList = searchdata;
                        foreach (var data in ViewBag.SearchDataList)
                        {
                            this.ViewBag.btnAbnormalFlag = data.btnAbnormalFlag;
                        }
                    }
                }
            }catch(Exception ex)
            {
                this.ViewBag.errorMsg = Resource.SYS01E002;
            }
            return View("~/Views/DataSearchList.cshtml", csvDataSearchModel);
        }

        public async Task<IActionResult> AbnormalDataTotalList(CSVDataViewModel csvViewModel)
        {
            try
            {
                List<AbnormalDataTotalByDate> abnormaldata = new List<AbnormalDataTotalByDate>();
                var babyID = this.HttpContext.Request.Query["babyid"].ToString();
                if (babyID == "" || babyID == null)
                {
                    this.ViewBag.msgFlag = "error";
                    this.ViewBag.msg = Resource.SYS02E002;
                }
                else
                {
                    csvViewModel.babyID = this.HttpContext.Request.Query["babyid"].ToString();
                    this.ViewBag.msgFlag = "info";
                    this.ViewBag.msg = Resource.SYS02I001;
                    abnormaldata = await _postRepository.AbnormalDataTotalByDate(csvViewModel.babyID);
                    this.ViewBag.abnormalDataList = abnormaldata;
                }
            }catch(Exception ex)
            {
                this.ViewBag.msgFlag = "error";
                this.ViewBag.errorMsg = Resource.SYS02E001;
            }
            return View("~/Views/AbnormalDataTotalList.cshtml", csvViewModel);
        }

        public async Task<IActionResult> AbnormalDataListPrint(CSVDataViewModel csvViewModel)
        {
            try
            {
                List<CSVDataMaster> abnormaldatadetails = new List<CSVDataMaster>();
                var babyID = this.HttpContext.Request.Query["babyid"].ToString();
                var riskdate = this.HttpContext.Request.Query["riskdate"].ToString();
                var okByDate = this.HttpContext.Request.Query["okByDate"].ToString();
                var bodyNG = this.HttpContext.Request.Query["bodyNG"].ToString();
                var heartNG = this.HttpContext.Request.Query["heartNG"].ToString();
                var respNG = this.HttpContext.Request.Query["respNG"].ToString();
                if (babyID == "" || riskdate == "" || okByDate == "" || bodyNG == "" || heartNG == "" || respNG == "")
                {
                   this.ViewBag.msg = "Error";
                }
                else
                {
                    csvViewModel.babyID = this.HttpContext.Request.Query["babyid"].ToString();
                    csvViewModel.riskDate = this.HttpContext.Request.Query["riskdate"].ToString();
                    csvViewModel.okByDate = this.HttpContext.Request.Query["okByDate"].ToString();
                    csvViewModel.bodyMovementNGByDate = this.HttpContext.Request.Query["bodyNG"].ToString();
                    csvViewModel.heartRateNGByDate = this.HttpContext.Request.Query["heartNG"].ToString();
                    csvViewModel.respRateNGByDate = this.HttpContext.Request.Query["respNG"].ToString();
                    this.ViewBag.msg = "";
                    abnormaldatadetails = await _postRepository.AbnormalDataDetails(csvViewModel.babyID, csvViewModel.riskDate);
                    this.ViewBag.abnormalDataDetailsList = abnormaldatadetails;
                }
            }catch(Exception ex)
            {
                this.ViewBag.errorMsg = Resource.SYS03E001;
            }
            return View("~/Views/AbnormalListPrint.cshtml", csvViewModel);
        }
    }
}
