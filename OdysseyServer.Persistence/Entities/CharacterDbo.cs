using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class CharacterDbo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Xp { get; set; }
        public int Level { get; set; }
        public int GearTier { get; set; }
        public ICollection<CharacterAbilitiesDbo> CharacterAbilities { get; set; }
        public ICollection<CharacterGroupsDbo> CharacterGroups { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
