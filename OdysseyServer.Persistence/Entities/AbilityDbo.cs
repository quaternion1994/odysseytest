using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class AbilityDbo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int RequiredLevel { get; set; }
        public ICollection<CharacterDbo> Character { get; set; }
        public AbilityStatsDbo Stats { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
