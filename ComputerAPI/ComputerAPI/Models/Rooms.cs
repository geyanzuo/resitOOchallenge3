using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerAPI.Models
{
    public class Rooms
    {
        public string Building { get; set; }
        public int RoomNo { get; set; }
        public int Capacity { get; set; }


        public IEnumerable<Computers> Computers { get; set; }
        public IEnumerable<Classes> Classes { get; set; } 
    }
}