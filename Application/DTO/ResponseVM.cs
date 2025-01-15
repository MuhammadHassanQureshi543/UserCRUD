using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ResponseVM
    {
        public bool status { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public dynamic Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
