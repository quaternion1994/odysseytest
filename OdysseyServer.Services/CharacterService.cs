using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using OdysseyServer.Services.Converters;
using OdysseyServer.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<CharacterGetResponse> GetCharacterByIdAsync(long characterId)
        {
            var characterDbo = await _unitOfWork.Character.GetCharacterById(characterId);
            var currentAbility = Helper.GetMaxLevelAbilities(characterDbo.Abilities.ToList());
            var listOfAbilities = Helper.ConvertToAbility(currentAbility.ToList());
            var listOfGroups = Helper.ConvertToGroup(characterDbo.Groups.ToList());
            var character = new Character();
            character = Converter.CharacterDboToCharacter(character, characterDbo, listOfAbilities, listOfGroups);
            var result = new CharacterGetResponse
            {
                Character = character
            };
            return result;
        }

        public async Task<CharacterCreateResponse> CreateCharacterAsync(CharacterCreateRequest requestObject)
        {
            var characterDbo = new CharacterDbo();
            var initialAbility = await _unitOfWork.Ability.GetInitialAbility();
            var abilityDboList = Helper.ConvertToAbilityDbo(requestObject.Character.Ability.ToList());
            var groupDboList = Helper.ConvertToGroupDbo(requestObject.Character.Group.ToList());            
            Converter.CharacterToCharacterDbo(requestObject.Character, characterDbo, initialAbility, groupDboList);
            await _unitOfWork.Character.Insert(characterDbo);
            var character = new Character();
            var listOfAbilities = Helper.ConvertToAbility(characterDbo.Abilities.ToList());
            var listOfGroups = Helper.ConvertToGroup(characterDbo.Groups.ToList());
            character = Converter.CharacterDboToCharacter(character, characterDbo, listOfAbilities, listOfGroups);
            var result = new CharacterCreateResponse
            {
                Character = character
            };
            return result;
        }

        public async Task<CharacterAllResponse> GetAllCharactersAsync()
        {
            var characterDbo = await _unitOfWork.Character.GetAllCharacters();
            var listOfCharacters = new List<Character>();
            foreach (var elem in characterDbo)
            {
                var currentAbility = Helper.GetMaxLevelAbilities(elem.Abilities.ToList());
                var listOfAbilities = Helper.ConvertToAbility(currentAbility.ToList());
                var listOfGroups = Helper.ConvertToGroup(elem.Groups.ToList());               
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

        public async Task<CharacterUpdateResponse> UpdateCharacterAsync(CharacterUpdateRequest requestObject)
        {
            var characterDbo = await _unitOfWork.Character.GetCharacterById(requestObject.Character.Id);
            Converter.UpdateDboByCharacter(requestObject.Character, characterDbo);
            await _unitOfWork.Character.SaveChangesAsync();
            var character = new Character();
            var currentAbility = Helper.GetMaxLevelAbilities(characterDbo.Abilities.ToList());
            var listOfAbilities = Helper.ConvertToAbility(currentAbility.ToList());
            var listOfGroups = Helper.ConvertToGroup(characterDbo.Groups.ToList());
            character = Converter.CharacterDboToCharacter(character, characterDbo, listOfAbilities, listOfGroups);
            var result = new CharacterUpdateResponse
            {
                Character = character
            };
            return result;
        }

        public async Task DeleteCharacterAsync(long characterId)
        {
            await _unitOfWork.Character.Delete(characterId);
        }

        public async Task<CharacterLevelBoostResponse> CharacterLevelBoostAsync(CharacterLevelBoostRequest requestObject)
        {
            await _unitOfWork.Character.CharacterLevelBoost(requestObject.CharacterId, requestObject.LevelNumber);
            var characterDbo = await _unitOfWork.Character.GetCharacterById(requestObject.CharacterId);
            var currentAbilities = Helper.GetMaxLevelAbilities(characterDbo.Abilities.ToList());
            if (currentAbilities.Count < 4)
            {
                await _unitOfWork.Character.UnlockInitialAbility(characterDbo.Id, characterDbo.Level);
            }
            var newAbilities = Helper.ConvertToAbility(Helper.GetMaxLevelAbilities((await _unitOfWork.Character
                .GetCharacterById(requestObject.CharacterId)).Abilities.ToList()));
            var listOfGroups = Helper.ConvertToGroup(characterDbo.Groups.ToList());
            var character = new Character();
            character = Converter.CharacterDboToCharacter(character, characterDbo, newAbilities, listOfGroups);
            var result = new CharacterLevelBoostResponse
            {
                Character = character
            };
            return result;
        }

        public async Task<CharacterAddGroupResponse> CharacterAddGroupAsync(CharacterAddGroupRequest requestObject)
        {
            var characterDbo = await _unitOfWork.Character.GetCharacterById(requestObject.CharacterId);
            var groupDbo = await _unitOfWork.Group.GetByArrayId(requestObject.GroupId.ToList());
            groupDbo.ForEach(characterDbo.Groups.Add);
            await _unitOfWork.Character.SaveChangesAsync();
            var character = new Character();
            var listOfAbilities = Helper.ConvertToAbility(characterDbo.Abilities.ToList());
            var listOfGroups = Helper.ConvertToGroup(characterDbo.Groups.ToList());
            character = Converter.CharacterDboToCharacter(character, characterDbo, listOfAbilities, listOfGroups);
            var result = new CharacterAddGroupResponse
            {
                Character = character
            };
            return result;
        }

        public async Task<CharacterAbilityBoostResponse> CharacterBoostAbilitiesAsync(CharacterAbilityBoostRequest requestObject)
        {
            await _unitOfWork.Character.CharacterAbilityBoost(requestObject.CharacterId, requestObject.AbilityId);
            var characterDbo = await _unitOfWork.Character.GetCharacterById(requestObject.CharacterId);
            var currentAbility = Helper.GetMaxLevelAbilities(characterDbo.Abilities.ToList());
            var listOfAbilities = Helper.ConvertToAbility(currentAbility.ToList());
            var listOfGroups = Helper.ConvertToGroup(characterDbo.Groups.ToList());
            var character = new Character();
            character = Converter.CharacterDboToCharacter(character, characterDbo, listOfAbilities, listOfGroups);
            var result = new CharacterAbilityBoostResponse
            {
                Character = character
            };
            return result;
        }
    }
}
