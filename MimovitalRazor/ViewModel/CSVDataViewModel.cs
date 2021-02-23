using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MimovitalRazor.ViewModel
{
    public class CSVDataViewModel
    {
        [Required(ErrorMessage = "個人IDを入力してください。")]
        [RegularExpression("^[0-9]{13}$",ErrorMessage = "個人IDは半角数字13桁で入力してください。")]
        public string babyID { get; set; }

        public string staffID { get; set; }
        public string roomID { get; set; }
        public long sensorID { get; set; }
        public DateTime occurDate { get; set; }

        public long bodyResult { get; set; }
        public long heartResult { get; set; }
        public long respResult { get; set; }

        public string riskDate { get; set; }
        public bool bodyCheckflag { get; set; } 
        public bool heartCheckflag { get; set; } 
        public bool respCheckflag { get; set; }

        public int bodyCheck { get; set; }
        public int heartCheck { get; set; }
        public int respCheck { get; set; }

        public string errorMsg { get; set; }

        public int btnAbnormalFlag { get; set; }

        public string okByDate { get; set; }
        public string bodyMovementNGByDate { get; set; }
        public string heartRateNGByDate { get; set; }
        public string respRateNGByDate { get; set; }

        /*public bool setBodyStatus()
        {
            this.bodyCheckflag = this.bodyCheck == 1 ? true : false;
            return this.bodyCheckflag;
        }

        public bool setHeartStatus()
        {
            this.heartCheckflag = this.heartCheck == 1 ? true : false;
            return this.heartCheckflag;
        }

        public bool setRespStatus()
        {
            this.respCheckflag = this.respCheck == 1 ? true : false;
            return this.respCheckflag;
        }*/

        public int getBodyStatus()
        {
            this.bodyCheck = this.bodyCheckflag == true ? 1 : 0;
            return this.bodyCheck;
        }

        public int getHeartStatus()
        {
            this.heartCheck = this.heartCheckflag == true ? 1 : 0;
            return this.heartCheck;
        }

        public int getRespStatus()
        {
            this.respCheck = this.respCheckflag == true? 1 : 0;
            return this.respCheck;
        }

        public void setPropertiesForSearch(CSVDataViewModel csvDataView)
        {
            csvDataView.babyID = this.babyID;
            csvDataView.bodyCheck = this.bodyCheckflag == true ? 1:0;
            csvDataView.heartCheck = this.heartCheckflag == true? 1:0;
            csvDataView.respCheck = this.respCheckflag == true? 1:0;
            csvDataView.bodyCheckflag = this.bodyCheck == 1 ? true : false;
            csvDataView.heartCheckflag = this.heartCheck == 1 ? true : false;
            csvDataView.respCheckflag = this.respCheck == 1 ? true : false;
        }
    }

}
