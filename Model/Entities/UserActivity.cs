using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class UserActivity
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public String UserName { get; set; }
        public String Description { get; set; }
        public DateTime BeginOfActivity { get; set; }
        public DateTime EndOfActivity { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual IdentityUser User  { get; set; }
    }
}
