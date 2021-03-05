using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class CharacterAbilities
    {
        public Guid Id { get; set; }
        public Character Character { get; set; }
        public Ability Ability { get; set; }       
        public Guid CharacterId { get; set; }
        public Guid AbilityId{ get; set; }
    }
}
