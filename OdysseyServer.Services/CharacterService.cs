using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CharacterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Character GetCharacterById(Guid Id)
        {
            return _unitOfWork.Character.GetByID(Id);
        }

        public async Task CreateCharacter(Character character)
        {
            await _unitOfWork.Character.Insert(character);
        }
    }
}
