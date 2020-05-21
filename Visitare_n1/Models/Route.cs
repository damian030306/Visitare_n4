using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Visitare_n1.Models
{
    public class Route
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "dwa")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
    }
}