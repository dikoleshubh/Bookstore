using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RequestBook : BookModel
    {
        public IFormFile BookImage { get; set; }
    }
}
