using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class CharacterGroups
    {
        public Guid Id { get; set; }
        public Character Character { get; set; }
        public Group Group { get; set; }
        public Guid CharacterId { get; set; }
        public Guid GroupId { get; set; }
    }
}
