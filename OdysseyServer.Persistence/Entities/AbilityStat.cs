using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Entities
{
    public class AbilityStat
    {
        public Guid Id { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public ICollection<Ability> Abilities { get; set; }
    }
}
