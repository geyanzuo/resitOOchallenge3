//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComputerAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Class
    {
        public string ClassCode { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public Nullable<int> RoomNo { get; set; }
    
        public virtual Room Room { get; set; }
    }
}
