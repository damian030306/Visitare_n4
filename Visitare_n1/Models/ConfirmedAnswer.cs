using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Visitare_n1.Models
{
    public class ConfirmedAnswer
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnswerAndQuestion2Id { set; get; }
        [ForeignKey("AnswerAndQuestion2Id")]
        public AnswerAndQuestion2 answerAndQuestion2{ get; set; }
    }
}