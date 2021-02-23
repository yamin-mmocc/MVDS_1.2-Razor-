using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MimovitalRazor.Models
{
    public class CSVDataMaster
    {
        [Key]
        public long ID { get; set; }
        public long levelBodyMotionL { get; set; }
        public long levelHeartL { get; set; }
        public long levelRespirationL { get; set; }
        public long levelBodyMotionR { get; set; }
        public long levelHeartR { get; set; }
        public long levelRespirationR { get; set; }
        public DateTime occurDate { get; set; }
        [Required(ErrorMessage = "個人IDを入力してください。")]
        [RegularExpression("^[0-9]{13}$", ErrorMessage = "個人IDは半角数字13桁で入力してください。")]
        public string babyID { get; set; }
        public string staffID { get; set; }
        public string roomID { get; set; }
        public long sensorID { get; set; }
        public long levelLearnedBodyMotionL { get; set; }
        public long levelLearnedHeartL { get; set; }
        public long levelLearnedRespirationL { get; set; }
        public long levelLearnedBodyMotionR { get; set; }
        public long levelLearnedHeartR { get; set; }
        public long levelLearnedRespirationR { get; set; }
        public double averagingPeriodBodyMotion { get; set; }
        public double averagingPeriodHeart { get; set; }
        public double averagingPeriodRespiration { get; set; }
        public double inputOffsetL { get; set; }
        public double inputGainL { get; set; }
        public double outputGainHeartL { get; set; }
        public double outputGainRespirationL { get; set; }
        public double inputOffsetR { get; set; }
        public double inputGainR { get; set; }
        public double outputGainHeartR { get; set; }
        public double outputGainRespirationR { get; set; }
        public long heartRate { get; set; }
        public long respirationRate { get; set; }
        public long minBodyMotion { get; set; }
        public long minlHeart { get; set; }
        public long minRespiration { get; set; }
        public long maxBodyMotion { get; set; }
        public long maxlHeart { get; set; }
        public long maxRespiration { get; set; }
        [Required]
        [NotMapped]
        public virtual long bodyResult
        {
            get
            {
                return ((((32767 - levelLearnedBodyMotionL) * (((decimal)minBodyMotion) / 100)) + levelLearnedBodyMotionL) <= levelBodyMotionL) ? 0 : 1;
            }
        }

        [Required]
        [NotMapped]
        public virtual long heartResult
        {
            get
            {
                return ((((32767 - levelLearnedHeartL) * (((decimal)minlHeart) / 100)) + levelLearnedHeartL) <= levelHeartL) ? 0 : 1;
            }
        }

        [Required]
        [NotMapped]
        public virtual long respResult
        {
            get
            {
                return ((((32767 - levelLearnedRespirationL) * (((decimal)minRespiration) / 100)) + levelLearnedRespirationL) <= levelRespirationL) ? 0 : 1;
            }
        }
        [Required]
        [NotMapped]
        public virtual int btnAbnormalFlag
        {
            get
            {
                return (bodyResult == 1 || heartResult == 1 || respResult == 1) ? 1 : 0;
            }
        }
    }
}
