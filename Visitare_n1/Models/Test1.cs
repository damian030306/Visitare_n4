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
        public Test1(string id, string nickname, int punkty = 0, string firstName = "", string lastName = "")
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Nickname = nickname;
            Punkty = punkty;
            
        }

        public Test1()
        {
        }
    }
    public class UserReturn
    {
        public UserReturn()
        {
        }
        public UserReturn(Test1 user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Nickname = user.Nickname;
            Roles = new List<string>();
            Points = user.Punkty;
        }

        public UserReturn(string id, string nickname, int punkty, string firstName = "", string lastName = "")
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Nickname = nickname;
            Points = punkty;
            
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public List<string> Roles { get; set; }
        public int Points { get; set; }

    }
}