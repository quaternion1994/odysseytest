using OdysseyServer.Persistence.Contracts;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private OdysseyDbContext _context;

        public UnitOfWork(OdysseyDbContext context)
        {
            _context = context;
            Ability = new AbilityRepository(_context);
            Character = new CharacterRepository(_context);
            Group = new GroupRepository(_context);
        }

        public async virtual Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IAbilityRepository Ability { get; private set; }
        public ICharacterRepository Character { get; private set; }
        public IGroupRepository Group { get; private set; }
        
    }
}
