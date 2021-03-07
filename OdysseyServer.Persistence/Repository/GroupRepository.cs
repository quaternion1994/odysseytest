using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Persistence.Repository
{
    public class GroupRepository : Repository<GroupDbo>, IGroupRepository
    {
        private OdysseyDbContext _context;

        public GroupRepository(OdysseyDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
