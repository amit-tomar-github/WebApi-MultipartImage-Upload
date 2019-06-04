using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        //http://localhost:51297/api//products/UploadImg
        //public void PostUploadImg(Product id)
        //{
        //    SaveImage(id.Img, "test");//id.Img will send as json object it will be like {Img:"base64string"} as post body
        //}

        public bool SaveImage(string ImgStr, string ImgName)
        {
            String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = ImgName + ".png";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(imgPath, imageBytes);

            return true;
        }
        //Save Image as base64 string
        //public IHttpActionResult PostPatientImg(PatientsData patientsData)
        //{
        //    int i = 1;
        //    foreach (var Item in patientsData.ImagesBase64Str)
        //    {
        //        SavePatientImage(Item.OriginalImgString, patientsData.MobileNo + "_Orig" + i.ToString());
        //        SavePatientImage(Item.EditImgString, patientsData.MobileNo + "_Edit" + i.ToString());
        //        i++;
        //    }
        //    return Ok(new ReturnMsg { Msg = "Y" });
        //}
        //Work good
        //public async Task<FileResult> PostPatientImg()
        //{
        //    String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path
        //    var streamProvider = new MultipartFormDataStreamProvider(path);
        //    await Request.Content.ReadAsMultipartAsync(streamProvider);

        //    return new FileResult
        //    {
        //        FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
        //        Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
        //        ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
        //        Description = streamProvider.FormData["description"],
        //        CreatedTimestamp = DateTime.UtcNow,
        //        UpdatedTimestamp = DateTime.UtcNow,
        //        DownloadLink = "TODO, will implement when file is persisited"
        //    };
        //}
        public async Task<HttpResponseMessage> PostPatientImg()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        postedFile.SaveAs(path + "/" + postedFile.FileName);
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        public bool SavePatientImage(string ImgStr, string ImgName)
        {
            String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = ImgName + ".png";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(imgPath, imageBytes);

            return true;
        }
    }

    public class FileResult
    {
        public IEnumerable<string> FileNames { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string DownloadLink { get; set; }
        public IEnumerable<string> ContentTypes { get; set; }
        public IEnumerable<string> Names { get; set; }
    }
}
