using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class Ability
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public Guid CharacterId { get; set; }
        public ICollection<CharacterAbilities> CharacterAbilities { get; set; }
        public AbilityStat AbilityStat { get; set; }
    }
}
