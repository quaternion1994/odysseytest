﻿using Microsoft.EntityFrameworkCore;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Repository
{
    public class GroupRepository : Repository<GroupDbo>, IGroupRepository
    {
        private OdysseyDbContext _context;

        public GroupRepository(OdysseyDbContext context) : base(context)
        {
            _context = context;
        }

        public async virtual Task<List<GroupDbo>> GetByArrayId(List<long> idsLIst)
        {
            return await dbSet.Where(x => idsLIst.Contains(x.Id)).ToListAsync();
        }
    }
}
