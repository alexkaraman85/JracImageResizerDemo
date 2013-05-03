using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using ImageResizer;

namespace JracImageResizerDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Process(ProcessModel model)
        {
            /*
             * How to process
             * step 1: download the image
             * step 2: resize the image
             * step 3: crop the image
             * step 4: stream out the result
             */

            // step 1: download
            var downloader = new Downloader();
            var bytes = downloader.ReadBytes(model.ImageUrl);

            // step 2: resize
            var dest = new MemoryStream();
            var settings = string.Format("width={0};height={1};format=png;scale=both", model.ImageWidth, model.ImageHeight);
            ImageBuilder.Current.Build(bytes, dest, new ResizeSettings(settings));
            dest.Position = 0;

            // step 3: crop
            var result = new MemoryStream();
            var cropSettings = string.Format("crop={0},{1},{2},{3}",
                                             model.CropX,
                                             model.CropY,
                                             model.CropX + model.CropWidth,
                                             model.CropY + model.CropHeight);
            ImageBuilder.Current.Build(dest, result, new ResizeSettings(cropSettings));
            result.Position = 0;

            // step 4: stream result
            return new FileStreamResult(result, "image/png");
        }
    }
    public class ProcessModel
    {
        public string ImageUrl { get; set; }
        public int CropX { get; set; }
        public int CropY { get; set; }
        public int CropWidth { get; set; }
        public int CropHeight { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
    }
    
    public class Downloader
    {
        public byte[] ReadBytes(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("url");
            }

            using (var www = new WebClient())
            {
                return www.DownloadData(url);
            }
        }
    }
}
