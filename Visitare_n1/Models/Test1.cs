using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Visitare_n1.Models
{
    public class Test1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public int Punkty { get; set; }
    }
}