using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class AbilityStatsDbo
    {
        public long Id { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public long AbilityId { get; set; }
        public AbilityDbo Ability { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
