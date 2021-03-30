using Microsoft.AspNetCore.Mvc;
using OdysseyServer.Services.Contracts;
using System.Threading.Tasks;
using OdysseyServer.ApiClient;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace OdysseyServer.Api.Controllers
{
    [Route("api/character")]
    [ApiController]
    public class CharacterController : OdysseyControllerBase
    {
        private ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // GET: api/<CharacterController>
        [HttpGet("/api/characters")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterAllResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCharacterAsync()
        {
            return Protobuf(await _characterService.GetAllCharactersAsync());
        }

        // GET api/character
        [HttpGet("{id}")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterGetResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharacterByIdAsync(long id)
        {
            return Protobuf(await _characterService.GetCharacterByIdAsync(id));
        }

        // POST api/character
        /// <summary>
        /// Accepts CharacterCreateRequest
        /// </summary>
        [HttpPost]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterCreateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterCreateAsync(CharacterCreateRequest person)
        {
            return Protobuf(await _characterService.CreateCharacterAsync(person));
        }

        // PUT api/character
        /// <summary>
        /// Accepts CharacterUpdateRequest
        /// </summary>
        [HttpPut("")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterUpdateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterUpdateAsync(CharacterUpdateRequest requestObject)
        {           
            return Protobuf(await _characterService.UpdateCharacterAsync(requestObject));
        }

        // DELETE api/character/id
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterDeleteAsync(long id)
        {
            await _characterService.DeleteCharacterAsync(id);
            return Ok();
        }

        /// <summary>
        /// Accepts CharacterLevelBoostRequest
        /// </summary>
        [HttpPut("lvlboost")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterUpdateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharecterLevelBoostAsync(CharacterLevelBoostRequest requestObject)
        {
            return Protobuf(await _characterService.CharacterLevelBoostAsync(requestObject));
        }

        /// <summary>
        /// Accepts CharacterAddGroupRequest
        /// </summary>
        [HttpPost("addgroup")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterAddGroupResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterAddGroupAsync(CharacterAddGroupRequest requestObject)
        {
            return Protobuf(await _characterService.CharacterAddGroupAsync(requestObject));
        }

        /// <summary>
        /// Accepts CharacterAbilityBoostRequest
        /// </summary>
        [HttpPut("abilityBoost")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterAbilityBoostResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterBoostAbilityAsync(CharacterAbilityBoostRequest requestObject)
        {
            return Protobuf(await _characterService.CharacterBoostAbilitiesAsync(requestObject));
        }
    }
}
