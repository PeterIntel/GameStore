using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace GameStoreWebApplication.web.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public FileResult DownloadGame(int? gamekey)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/download/download.exe"));
            string filename = "download.exe";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}