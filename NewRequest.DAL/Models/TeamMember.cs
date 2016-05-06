using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRequest.DAL.Models
{
    public class TeamMember
    {
        public int Id { set; get; }
        public int GameId { set; get; }
        public int TeamId { set; get; }
        public string TeamName { set; get; }
    }
}
