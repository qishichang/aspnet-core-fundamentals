using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class Game
    {
        public string City { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
    }
}
