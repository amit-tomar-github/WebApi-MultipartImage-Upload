using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
    }

    public class PatientsData
    {
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public List<OrderType> OrderTypes { get; set; }
        public string Priority { get; set; }
        public List<ImageEditBase64> ImagesBase64Str { get; set; }
    }
    public class ImageEditBase64
    {
        public string OriginalImgString { get; set; }
        public string EditImgString { get; set; }
    }
    public class OrderType
    {
        public string OrderInfo { get; set; }
    }

    public class ReturnMsg
    {
        public string Msg { get; set; }
    }

}