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
        public async Task<CharacterGetResponse> GetCharacterById(CharacterGetRequest requestObject)
        {
            var characterDbo = await _unitOfWork.Character.GetCharacterById(requestObject.CharacterId);
            var character = _mapper.Map<Character>(characterDbo);

            foreach (var elem in characterDbo.CharacterAbilities)
            {
                var mappedAbility = _mapper.Map<Ability>(elem.Ability);
                character.Ability.Add(mappedAbility);
            }

            foreach (var elem in characterDbo.CharacterGroups)
            {
                var mappedGroup = _mapper.Map<Group>(elem.Group);
                character.Group.Add(mappedGroup);
            }

            var result = new CharacterGetResponse
            {
                Character = character
            };
            return result;
        }

        public async Task<CharacterCreateResponse> CreateCharacter(CharacterCreateRequest requestObject)
        {
            var characterDbo = _mapper.Map<CharacterDbo>(requestObject.Character);
            await _unitOfWork.Character.Insert(characterDbo);
            var characterAbilities = new List<CharacterAbilitiesDbo>();
            foreach (var elem in requestObject.Character.Ability)
            {
                characterAbilities.Add(new CharacterAbilitiesDbo
                {
                    AbilityId = elem.Id,
                    CharacterId = characterDbo.Id
                });
            }
            await _unitOfWork.CharacterAbilities.InsertMany(characterAbilities);
            var characterDboCreated = await _unitOfWork.Character.GetByID(characterDbo.Id);
            var character = _mapper.Map<Character>(characterDboCreated);
            var result = new CharacterCreateResponse
            {
                Character = character
            };
            return result;

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

        public async Task<CharacterUpdateResponse> UpdateCharacter(CharacterUpdateRequest requestObject)
        {
            var characterDbo = _mapper.Map<CharacterDbo>(requestObject.Character);
            await _unitOfWork.Character.Update(characterDbo);
            var characterDboUpdated = await _unitOfWork.Character.GetByID(characterDbo.Id);
            var character = _mapper.Map<Character>(characterDboUpdated);
            var result = new CharacterUpdateResponse
            {
                Character = character
            };
            return result;
        }

        public async Task DeleteCharacter(CharacterDeleteRequest requestObject)
        {
            await _unitOfWork.CharacterAbilities.DeleteManyByCharacterId(requestObject.CharacterId);
            await _unitOfWork.CharacterAbilities.DeleteGroupsByCharacterId(requestObject.CharacterId);
            await _unitOfWork.Character.Delete(requestObject.CharacterId);
        }

        public async Task<CharacterLevelBoostResponse> CharacterLevelBoost(CharacterLevelBoostRequest requestObject)
        {
            await _unitOfWork.Character.CharacterLevelBoost(requestObject.CharacterId, requestObject.LevelNumber);
            var characterDbo = await _unitOfWork.Character.GetByID(requestObject.CharacterId);
            var character = _mapper.Map<Character>(characterDbo);
            var result = new CharacterLevelBoostResponse
            {
                Character = character
            };
            return result;
        }

        public async Task<CharacterAddGroupResponse> CharacterAddGroup(CharacterAddGroupRequest requestObject)
        {
            var characterGroups = new List<CharacterGroupsDbo>();
            foreach (var elem in requestObject.GroupId)
            {
                characterGroups.Add(new CharacterGroupsDbo
                {
                    CharacterId = requestObject.CharacterId,
                    GroupId = elem
                });
            }
            await _unitOfWork.Group.AddGroupForCharacter(characterGroups);
            var characterDbo = await _unitOfWork.Character.GetByID(requestObject.CharacterId);
            var character = _mapper.Map<Character>(characterDbo);
            var result = new CharacterAddGroupResponse
            {
                Character = character
            };
            return result;
        }

        public async Task<CharacterAddAbilitiesResponse> CharacterAddAbilities(CharacterAddAbilitiesRequest requestObject)
        {
            var characterAbilities = new List<CharacterAbilitiesDbo>();
            foreach (var elem in requestObject.Abilities)
            {
                characterAbilities.Add(new CharacterAbilitiesDbo
                {
                    CharacterId = requestObject.CharacterId,
                    AbilityId = elem
                });
            }
            await _unitOfWork.CharacterAbilities.InsertMany(characterAbilities);
            var characterDbo = await _unitOfWork.Character.GetByID(requestObject.CharacterId);
            var character = _mapper.Map<Character>(characterDbo);
            var result = new CharacterAddAbilitiesResponse
            {
                Character = character
            };
            return result;
        }
    }
}
