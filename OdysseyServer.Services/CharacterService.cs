using AutoMapper;
using OdysseyServer.ApiClient;
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
        private readonly IMapper _mapper;

        public CharacterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Character> GetCharacterById(long id)
        {
            var characterDbo = await _unitOfWork.Character.GetCharacterById(id);
            var character = _mapper.Map<Character>(characterDbo);

            foreach (var elem in characterDbo.CharacterAbilities)
            {
                var mappedAbility = _mapper.Map<Ability>(elem.Ability);
                character.Ability.Add(mappedAbility);
            }

            foreach (var elem in characterDbo.CharacterGroups)
            {
                var mappedGroup= _mapper.Map<Group>(elem.Group);
                character.Group.Add(mappedGroup);
            }
            return character;
        }

        public async Task CreateCharacter(Character character)
        {
            var characterDbo = _mapper.Map<CharacterDbo>(character);
            await _unitOfWork.Character.Insert(characterDbo);
            var characterAbilities = new List<CharacterAbilitiesDbo>();
            foreach (var elem in character.Ability)
            {
                characterAbilities.Add(new CharacterAbilitiesDbo
                {
                    AbilityId = elem.Id,
                    CharacterId = characterDbo.Id
                });
            }
            await _unitOfWork.CharacterAbilities.InsertMany(characterAbilities);
        }

        public async Task<AllCharacter> GetAllCharacters()
        {
            var characterDbo = await _unitOfWork.Character.GetAllCharacters();
            var objectForMap = new AllCharacterDbo
            {
                Characters = characterDbo
            };
            var character = _mapper.Map<AllCharacter>(objectForMap);
            return character;
        }

        public async Task<Character> UpdateCharacter(Character character)
        {
            var characterDbo = _mapper.Map<CharacterDbo>(character);
            await _unitOfWork.Character.Update(characterDbo);
            var characterDboUpdated = await _unitOfWork.Character.GetByID(characterDbo.Id);
            var result = _mapper.Map<Character>(characterDboUpdated);
            return result;
        }

        public async Task DeleteCharacter(long id)
        {
            await _unitOfWork.CharacterAbilities.DeleteManyByCharacterId(id);
            await _unitOfWork.CharacterAbilities.DeleteGroupsByCharacterId(id);
            await _unitOfWork.Character.Delete(id);
        }

        public async Task<Character> CharacterLevelBoost(long id, int lvlNumber)
        {
            await _unitOfWork.Character.CharacterLevelBoost(id, lvlNumber);
            var characterDbo = await _unitOfWork.Character.GetByID(id);
            var character = _mapper.Map<Character>(characterDbo);
            return character;
        }
    }
}
