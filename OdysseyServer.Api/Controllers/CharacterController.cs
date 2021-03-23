using Microsoft.AspNetCore.Mvc;
using OdysseyServer.Services.Contracts;
using System.Threading.Tasks;
using Google.Protobuf;
using OdysseyServer.ApiClient;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace OdysseyServer.Api.Controllers
{
    [Route("api/character")]
    [ApiController]
    public class CharacterController : ControllerBase
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
        public async Task<IActionResult> GetAllCharacter()
        {
            var characters = await _characterService.GetAllCharacters();
            var data = characters.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // GET api/character
        [HttpGet("{id}")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterGetResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharacterById(long id)
        {
            var character = await _characterService.GetCharacterById(id);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // POST api/character
        /// <summary>
        /// Accepts CharacterCreateRequest
        /// </summary>
        [HttpPost]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterCreateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterCreate()
        {
            var stream = Request.BodyReader.AsStream();
            var person = CharacterCreateRequest.Parser.ParseFrom(stream);
            var result = await _characterService.CreateCharacter(person);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // PUT api/character
        /// <summary>
        /// Accepts CharacterUpdateRequest
        /// </summary>
        [HttpPut("")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterUpdateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterUpdate()
        {           
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterUpdateRequest.Parser.ParseFrom(stream);
            var result = await _characterService.UpdateCharacter(requestObject);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // DELETE api/character/id
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterDelete(long id)
        {
            await _characterService.DeleteCharacter(id);
            return Ok();
        }

        /// <summary>
        /// Accepts CharacterLevelBoostRequest
        /// </summary>
        [HttpPut("lvlboost")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterUpdateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharecterLevelBoost()
        {
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterLevelBoostRequest.Parser.ParseFrom(stream);
            var character = await _characterService.CharacterLevelBoost(requestObject);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }

        /// <summary>
        /// Accepts CharacterAddGroupRequest
        /// </summary>
        [HttpPost("addgroup")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterAddGroupResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterAddGroup()
        {
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterAddGroupRequest.Parser.ParseFrom(stream);
            var character = await _characterService.CharacterAddGroup(requestObject);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }

        /// <summary>
        /// Accepts CharacterAbilityBoostRequest
        /// </summary>
        [HttpPut("abilityBoost")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(CharacterAbilityBoostResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CharacterBoostAbility()
        {
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterAbilityBoostRequest.Parser.ParseFrom(stream);
            var character = await _characterService.CharacterBoostAbilities(requestObject);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }
    }
}
