using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Visitare_n1.Models
{
    public class AnswerAndQuestion2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Question1 { get; set; }
        public String AnswersString
        {
            get
            {
                return String.Join(@",", Answers);

            }

            set
            {
                Answers = new List<string>(value.Split(','));

            }
        }
        public List<string> Answers { get; set; }
        public int GoodAnswer { get; set; }
        public string Correct { get; set; }
        public int RouteId { set; get; }
        [ForeignKey("RouteId")]
        public Route Route { get; set; }
    }
}