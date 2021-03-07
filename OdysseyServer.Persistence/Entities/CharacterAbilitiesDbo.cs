using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class CharacterAbilitiesDbo
    {
        public CharacterDbo Character { get; set; }
        public AbilityDbo Ability { get; set; }       
        public long CharacterId { get; set; }
        public long AbilityId{ get; set; }
    }
}
