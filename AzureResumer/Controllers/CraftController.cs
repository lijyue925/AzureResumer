using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;
using AzureResumer.Models;

namespace AzureResumer.Controllers
{
    public class CraftController : Controller
    {
        // GET: Craft
        public ActionResult Index(int type)
        {
            return View("Create", new Resume(type));
        }

        // POST: Craft/Create
        [HttpPost]
        public ActionResult Create(Resume model, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            var validFotmats = new List<ImageFormat>() { ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif };
            var message = "";

            try
            {
                //連接FTP
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(model.FtpUser, model.FtpPassword);

                string uploadUrl = model.FtpHost + "/site/wwwroot/test.txt";
                byte[] data = Encoding.Default.GetBytes("Created by AzureResumer");
                wc.UploadData(uploadUrl, data);
                

                //驗證上傳 & 儲存圖片路徑
                foreach (var file in fileUpload)
                {
                    if (file == null)
                    {
                        model.Photos.Add("");
                        continue;
                    }
                    if (file.ContentLength > 2 * 1024 * 1024)
                    {
                        message += "圖片大小不得超過2MB;";
                        model.Photos.Add("");
                        continue;
                    }
                    using (var img = Image.FromStream(file.InputStream))
                    {
                        if (!validFotmats.Contains(img.RawFormat))
                        {
                            message += "圖片格式錯誤，只能為PNG JPEG GIF;";
                            model.Photos.Add("");
                            continue;
                        }
                    }

                    var directory = Server.MapPath("~/App_Data/uploads/");
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    var path = "";
                    var extension = Path.GetExtension(file.FileName);
                    do
                    {
                        var fileName = Guid.NewGuid().ToString("N") + extension;
                        path = Path.Combine(directory, fileName);
                    } while (System.IO.File.Exists(path));

                    file.SaveAs(path);
                    model.Photos.Add(path);
                }
                //處理圖片 & 上傳圖片
                for (int index = 0; index < model.Photos.Count; index++)
                {
                    var photo = model.Photos[index];
                    var filename = "";

                    if (photo == "")
                    {
                        filename = Guid.NewGuid().ToString("N") + "DEFAULT.png";
                        data = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/template/B_Resume_Html/images/block.png"));
                    }
                    else
                    {
                        filename = Path.GetFileName(photo);
                        data = System.IO.File.ReadAllBytes(photo);
                    }

                    uploadUrl = model.FtpHost + "/site/wwwroot/" + filename;

                    model.Photos[index] = filename;

                    wc.UploadData(uploadUrl, data);
                }
                //產生Index.html 並上傳
                var html = ViewToString("~/Views/Template/IndexB.cshtml", model);
                data = Encoding.UTF8.GetBytes(html);
                uploadUrl = model.FtpHost + "/site/wwwroot/index.html";
                wc.UploadData(uploadUrl, data);

                //上傳用到的IMG
                data = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/template/B_Resume_Html/images/exp.png"));
                uploadUrl = model.FtpHost + "/site/wwwroot/exp.png";
                wc.UploadData(uploadUrl, data);
                //上傳CSS
                data = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/template/B_Resume_Html/css/site.css"));
                uploadUrl = model.FtpHost + "/site/wwwroot/site.css";
                wc.UploadData(uploadUrl, data);
                //上傳JS
                data = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/template/B_Resume_Html/js/site.js"));
                uploadUrl = model.FtpHost + "/site/wwwroot/site.js";
                wc.UploadData(uploadUrl, data);

                ViewData["message"] = message;
                return message != "" ? View("Error") : View("Success");
            }
            catch(Exception e)
            {
                ViewData["message"] = e.Message + message;
                return View("Error");
            }
        }


        private string ViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }


    }
}
