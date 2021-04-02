using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
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

        private IEnumerable<AbilityDbo> GetMaxLevelAbilities(IEnumerable<AbilityDbo> abilities)
        {
            foreach (IGrouping<int, AbilityDbo> group in abilities.GroupBy(x => x.AbilityType))
            {
                AbilityDbo maxLevelAbilityOfType = group.FirstOrDefault();
                foreach (AbilityDbo item in group)
                {
                    if (maxLevelAbilityOfType.Level < item.Level)
                        maxLevelAbilityOfType = item;
                }
                if (maxLevelAbilityOfType == null)
                    continue;

                yield return maxLevelAbilityOfType;
            }
        }

        public async Task<CharacterGetResponse> GetCharacterByIdAsync(long characterId)
        {
            CharacterDbo characterDbo = await _unitOfWork.Character.GetCharacterByIdAsync(characterId);
            characterDbo.Abilities = GetMaxLevelAbilities(characterDbo.Abilities).ToArray();

            CharacterGetResponse result = new CharacterGetResponse()
            {
                Character = _mapper.Map<Character>(characterDbo)
            };
            return result;
        }

        public async Task<CharacterCreateResponse> CreateCharacterAsync(CharacterCreateRequest requestObject)
        {
            CharacterDbo characterDbo = _mapper.Map<CharacterDbo>(requestObject.Character);
            characterDbo.Abilities = await _unitOfWork.Ability.GetInitialAbilityAsync();
            await _unitOfWork.Character.Insert(characterDbo);

            CharacterCreateResponse result = new CharacterCreateResponse
            {
                Character = requestObject.Character
            };
            result.Character.Id = characterDbo.Id;

            return result;
        }

        public async Task<CharacterAllResponse> GetAllCharactersAsync()
        {
            List<CharacterDbo> characterDbo = await _unitOfWork.Character.GetAllCharactersAsync();
            foreach (CharacterDbo elem in characterDbo)
            {
                elem.Abilities = GetMaxLevelAbilities(elem.Abilities).ToArray();
            }

            CharacterAllResponse result = new CharacterAllResponse
            {
                Character = new AllCharacter()
            };

            _mapper.Map(characterDbo, result.Character.Characters);

            return result;
        }

        public async Task<CharacterUpdateResponse> UpdateCharacterAsync(CharacterUpdateRequest requestObject)
        {
            await _unitOfWork.Character.Update(_mapper.Map<CharacterDbo>(requestObject.Character));

            CharacterUpdateResponse result = new CharacterUpdateResponse
            {
                Character = new Character()
            };
            CharacterDbo characterDbo = await _unitOfWork.Character.GetCharacterByIdAsync(requestObject.Character.Id);

            _mapper.Map(GetMaxLevelAbilities(characterDbo.Abilities), result.Character.Abilities);

            return result;
        }

        public async Task DeleteCharacterAsync(long characterId)
        {
            await _unitOfWork.Character.Delete(characterId);
        }

        public async Task<CharacterLevelBoostResponse> CharacterLevelBoostAsync(CharacterLevelBoostRequest requestObject)
        {
            await _unitOfWork.Character.CharacterLevelBoostAsync(requestObject.CharacterId, requestObject.LevelNumber);

            CharacterDbo characterDbo = await _unitOfWork.Character.GetCharacterByIdAsync(requestObject.CharacterId);
            AbilityDbo[] currentAbilities = GetMaxLevelAbilities(characterDbo.Abilities).ToArray();
            if (currentAbilities.Length < 4)
            {
                await _unitOfWork.Character.UnlockInitialAbilityAsync(characterDbo.Id, characterDbo.Level);
            }

            CharacterDbo updatedCharacterDbo = await _unitOfWork.Character.GetCharacterByIdAsync(requestObject.CharacterId);
            updatedCharacterDbo.Abilities = GetMaxLevelAbilities(updatedCharacterDbo.Abilities).ToArray();
            
            return new CharacterLevelBoostResponse
            {
                Character = _mapper.Map<Character>(updatedCharacterDbo)
            };
        }

        public async Task<CharacterAddGroupResponse> CharacterAddGroupAsync(CharacterAddGroupRequest requestObject)
        {
            CharacterDbo characterDbo = await _unitOfWork.Character.GetCharacterByIdAsync(requestObject.CharacterId);
            List<GroupDbo> groupDbos = await _unitOfWork.Group.GetArrayByIdsAsync(requestObject.GroupIds);

            groupDbos.ForEach(characterDbo.Groups.Add);
            await _unitOfWork.SaveChangesAsync();

            CharacterDbo updatedCharacterDbo = await _unitOfWork.Character.GetCharacterByIdAsync(requestObject.CharacterId);
            return new CharacterAddGroupResponse
            {
                Character = _mapper.Map<Character>(updatedCharacterDbo)
            };
        }

        public async Task<CharacterAbilityBoostResponse> CharacterBoostAbilitiesAsync(CharacterAbilityBoostRequest requestObject)
        {
            await _unitOfWork.Character.CharacterAbilityBoostAsync(requestObject.CharacterId, requestObject.AbilityId);
            
            CharacterDbo updatedCharacterDbo = await _unitOfWork.Character.GetCharacterByIdAsync(requestObject.CharacterId);
            return new CharacterAbilityBoostResponse
            {
                Character = _mapper.Map<Character>(updatedCharacterDbo)
            };
        }
    }
}
