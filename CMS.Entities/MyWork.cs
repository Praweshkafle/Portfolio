using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Entities
{
   public class MyWork
    {
        private string _title { get; set; }
        private string _description { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Title is not Valid");
                }
                _title = value;
            }
        }
        public string Description
        {
            get { return _description; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Description is not valid");
                }
                _description = value;
            }
        }

        public string Image { get; set; }
        public DateTime Created_date { get; set; } = DateTime.Now;
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
