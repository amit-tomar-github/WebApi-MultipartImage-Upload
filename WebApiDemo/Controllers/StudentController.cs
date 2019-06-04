using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    // [Route("api/[controller]") or
    //[Route("api/[controller]/[action]")]
    [RoutePrefix("api/stud")]
    public class StudentController : ApiController
    {
        public string Get()
        {
            return "I am coming from webapi";
        }

        //Multiple get method with different uri

        [Route("prodbyname")]
        public string GetProductByName()
        {
            return "hello: ";
        }

        [Route("prodbynameparam")]
        public string GetProductByNameParam([FromUri] string Name) //parameter will be send in uri as querystring
        {
            return $"Mr {Name} How are you";
        }

        [Route("prodbyidparam")]
        public string GetProductByIdParam(string Id) //parameter will be send in uri as querystring
        {
            return $"Mr {Id} I am doing well";
        }

        [Route("prodbyid")]
        public string GetProductById()
        {
            return "hello Id: ";
        }
    }
}
