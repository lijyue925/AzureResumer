using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace AzureResumer.Models
{
    public class Resume
    {
        [Display(Name = "類別"), HiddenInput(DisplayValue = false)]
        public int Type { get; set; }

        //部署資訊
        [Display(Name = "FTP 主機名稱:")]
        public string FtpHost { get; set; }
        [Display(Name = "FTP/部署使用者名稱")]
        public string FtpUser { get; set; }
        [Display(Name = "FTP 使用者密碼:(部署認證設定的密碼)"), DataType(DataType.Password)]
        public string FtpPassword { get; set; }



        [Display(Name = "姓名(中+英文) Ex. 張大帥 Stan")]
        public string Name { get; set; }
        [Display(Name = "電子信箱"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "連絡電話")]
        public string Phone { get; set; }
        [Display(Name = "就讀學校(最高學歷)")]
        public string School { get; set; }
        [Display(Name = "學校系所")]
        public string Depart { get; set; }
        [Display(Name = "自傳(簡短就好)"), DataType(DataType.MultilineText)]
        public string Bio { get; set; }
        [Display(Name = "專長")]
        public string Advantage { get; set; }
        [Display(Name = "嗜好")]
        public string Hobit { get; set; }

        [Display(Name = "專業技能一")]
        public string Skill1 { get; set; }
        [Display(Name = "專業技能二")]
        public string Skill2 { get; set; }
        [Display(Name = "專業技能三")]
        public string Skill3 { get; set; }
        [Display(Name = "專業技能四")]
        public string Skill4 { get; set; }

        [Display(Name = "專業技能一專精度(0-100)")]
        public string SkillValue1 { get; set; }
        [Display(Name = "專業技能二專精度(0-100)")]
        public string SkillValue2 { get; set; }
        [Display(Name = "專業技能三專精度(0-100)")]
        public string SkillValue3 { get; set; }
        [Display(Name = "專業技能四專精度(0-100)")]
        public string SkillValue4 { get; set; }

        [Display(Name = "經歷一名稱")]
        public string ExpName1 { get; set; }
        [Display(Name = "經歷二名稱")]
        public string ExpName2 { get; set; }
        [Display(Name = "經歷三名稱")]
        public string ExpName3 { get; set; }
        [Display(Name = "經歷一描述")]
        public string ExpContent1 { get; set; }
        [Display(Name = "經歷二描述")]
        public string ExpContent2 { get; set; }
        [Display(Name = "經歷三描述")]
        public string ExpContent3 { get; set; }

        [Display(Name = "作品集一名稱")]
        public string PortfolioName1 { get; set; }
        [Display(Name = "作品集二名稱")]
        public string PortfolioName2 { get; set; }
        [Display(Name = "作品集三名稱")]
        public string PortfolioName3 { get; set; }
        [Display(Name = "作品集四名稱")]
        public string PortfolioName4 { get; set; }
        [Display(Name = "作品集五名稱")]
        public string PortfolioName5 { get; set; }
        [Display(Name = "作品集六名稱")]
        public string PortfolioName6 { get; set; }

        [Display(Name = "作品集一連結(無連結可填#)")]
        public string PortfolioLink1 { get; set; }
        [Display(Name = "作品集二連結(無連結可填#)")]
        public string PortfolioLink2 { get; set; }
        [Display(Name = "作品集三連結(無連結可填#)")]
        public string PortfolioLink3 { get; set; }
        [Display(Name = "作品集四連結(無連結可填#)")]
        public string PortfolioLink4 { get; set; }
        [Display(Name = "作品集五連結(無連結可填#)")]
        public string PortfolioLink5 { get; set; }
        [Display(Name = "作品集六連結(無連結可填#)")]
        public string PortfolioLink6 { get; set; }


        [HiddenInput(DisplayValue = false)]
        public List<string> Photos { get; set; }
        //public HttpPostedFile TopFile { get; set; } 
        //public HttpPostedFile HeadFile { get; set; } 
        //public HttpPostedFile PortfolioImg1File { get; set; } 
        //public HttpPostedFile PortfolioImg2File { get; set; } 
        //public HttpPostedFile PortfolioImg3File { get; set; } 
        //public HttpPostedFile PortfolioImg4File { get; set; } 
        //public HttpPostedFile PortfolioImg5File { get; set; } 
        //public HttpPostedFile PortfolioImg6File { get; set; }

        public Resume()
        {
            this.Photos = new List<string>();
        }
        public Resume(int type)
        {
            this.Photos = new List<string>();
            this.Type = type;
        }
    }
}