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
    
    public partial class GameTip
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public string Tip { get; set; }
        public int Times { get; set; }
    
        public virtual AllLevel AllLevel { get; set; }
    }
}
