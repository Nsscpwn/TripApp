using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class LogIn
    {
        public LogIn()
        {
            this.Users = new Users();
        }
        public Users Users { get; set; }
           
    }
}