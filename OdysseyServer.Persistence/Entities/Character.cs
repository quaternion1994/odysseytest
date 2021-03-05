using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Xp { get; set; }
        public int Level { get; set; }
        public int GearTier { get; set; }
        public ICollection<CharacterAbilities> CharacterAbilities { get; set; }
    }
}
