using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class LevelStatsDbo
    {
        public int LevelNumber { get; set; }
        public int Offence { get; set; }
        public int Defence { get; set; }
        public int Health { get; set; }
    }
}
