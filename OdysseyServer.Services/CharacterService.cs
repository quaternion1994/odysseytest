using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using OdysseyServer.Services.Converters;
using OdysseyServer.Services.Helpers;
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
            var abilitiesDbo = characterDbo.Abilities;
            var groupsDbo = characterDbo.Groups;
            var listOfAbilities = new List<Ability>();
            var listOfGroups = new List<Group>();
            foreach (var elem in abilitiesDbo)
            {
                var convertedAbility = new Ability();
                convertedAbility = Converter.AbilityDboToAbility(convertedAbility, elem);
                listOfAbilities.Add(convertedAbility);
            }
            foreach (var elem in groupsDbo)
            {
                var convertedGroup = new Group();
                convertedGroup = Converter.GroupDboToGroup(convertedGroup, elem);
                listOfGroups.Add(convertedGroup);
            }
            var character = new Character();
            character = Converter.CharacterDboToCharacter(character, characterDbo, listOfAbilities, listOfGroups);
            var result = new CharacterGetResponse
            {
                Character = character
            };
            return result;
        }

        public async Task<CharacterCreateResponse> CreateCharacter(CharacterCreateRequest requestObject)
        {
            var characterDbo = new CharacterDbo();            
            var abilityDboList = new List<AbilityDbo>();
            var groupDboList = new List<GroupDbo>();
            foreach (var elem in requestObject.Character.Ability)
            {
                var abilityDbo = _mapper.Map<AbilityDbo>(elem);
                abilityDboList.Add(abilityDbo);
            }
            foreach (var elem in requestObject.Character.Group)
            {
                var groupDbo = _mapper.Map<GroupDbo>(elem);
                groupDboList.Add(groupDbo);
            }
            Converter.CharacterToCharacterDbo(requestObject.Character, characterDbo, abilityDboList, groupDboList);
            await _unitOfWork.Character.Insert(characterDbo);           
            var character = _mapper.Map<Character>(characterDbo);
            var result = new CharacterCreateResponse
            {
                Character = character
            };
            return result;

        }

        public async Task<CharacterAllResponse> GetAllCharacters()
        {
            var characterDbo = await _unitOfWork.Character.GetAllCharacters();
            var listOfCharacters = new List<Character>();
            foreach (var elem in characterDbo)
            {
                var abilitiesDbo = elem.Abilities;
                var groupsDbo = elem.Groups;
                var listOfAbilities = new List<Ability>();
                var listOfGroups = new List<Group>();
                foreach (var ability in abilitiesDbo)
                {
                    var convertedAbility = new Ability();
                    convertedAbility = Converter.AbilityDboToAbility(convertedAbility, ability);
                    listOfAbilities.Add(convertedAbility);
                }
                foreach (var group in groupsDbo)
                {
                    var convertedGroup = new Group();
                    convertedGroup = Converter.GroupDboToGroup(convertedGroup, group);
                    listOfGroups.Add(convertedGroup);
                }
                var convertedCharacter = new Character();
                convertedCharacter = Converter.CharacterDboToCharacter(convertedCharacter, elem, listOfAbilities, listOfGroups);
                listOfCharacters.Add(convertedCharacter);
            }

            var allCharacters = new AllCharacter();
            allCharacters.Characters.AddRange(listOfCharacters);
            
            var result = new CharacterAllResponse
            {
                Character = allCharacters
            };
            return result;
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
            /*var characterGroups = new List<CharacterGroupsDbo>();
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
            };*/
            return null;
        }

        public async Task<CharacterAddAbilitiesResponse> CharacterAddAbilities(CharacterAddAbilitiesRequest requestObject)
        {
            //var characterAbilities = new List<CharacterAbilitiesDbo>();
            //foreach (var elem in requestObject.Abilities)
            //{
            //    characterAbilities.Add(new CharacterAbilitiesDbo
            //    {
            //        CharacterId = requestObject.CharacterId,
            //        AbilityId = elem
            //    });
            //}
            //await _unitOfWork.CharacterAbilities.InsertMany(characterAbilities);
            //var characterDbo = await _unitOfWork.Character.GetByID(requestObject.CharacterId);
            //var character = _mapper.Map<Character>(characterDbo);
            //var result = new CharacterAddAbilitiesResponse
            //{
            //    Character = character
            //};
            return null;
        }
    }
}
