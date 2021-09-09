using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrud.Areas.CMS.ViewModels
{
    public class MyWorkViewModel
    {
        public List<WorkDetails> workDetails = new List<WorkDetails>();
    }
    public class WorkDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public IFormFile ProfileImage { get; set; }

    }
}
