using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class GroupDbo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public ICollection<CharacterGroupsDbo> CharacterGroups { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
