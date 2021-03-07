using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class CharacterGroupsDbo
    {
        public CharacterDbo Character { get; set; }
        public GroupDbo Group { get; set; }
        public long CharacterId { get; set; }
        public long GroupId { get; set; }
    }
}
