//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewRequest.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTeamStatu
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public int StatusId { get; set; }
    
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
        public virtual UserStatu UserStatu { get; set; }
    }
}
