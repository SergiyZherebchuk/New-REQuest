using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRequest.DAL.Models
{
    public class UserMember
    {
        public int Id { set; get; }
        public int GameId { set; get; }
        public int UserId { set; get; }
        public string UserName { set; get; }
    }
}
