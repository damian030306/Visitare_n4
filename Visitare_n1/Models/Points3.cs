using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Visitare_n1.Models
{
    public class Points3
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public double X { get; set; }
        [Required]
        public double Y { get; set; }

        public int RouteId { set; get; }
        [ForeignKey("RouteId")]
        public Route Route { get; set; }
    }
}