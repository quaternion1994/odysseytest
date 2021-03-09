using Microsoft.AspNetCore.Mvc;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using OdysseyServer.ApiClient;

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
        public async Task<IActionResult> GetAllCharacter()
        {
            var characters = await _characterService.GetAllCharacters();
            var data = characters.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // GET api/character
        [HttpPost("getbyid")]
        public async Task<IActionResult> GetCharacterById()
        {
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterGetRequest.Parser.ParseFrom(stream);           
            var character = await _characterService.GetCharacterById(requestObject);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // POST api/character
        [HttpPost("")]
        public async Task<IActionResult> CharacterCreate()
        {
            var stream = Request.BodyReader.AsStream();
            var person = CharacterCreateRequest.Parser.ParseFrom(stream);
            var result = await _characterService.CreateCharacter(person);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // PUT api/character
        [HttpPut("")]
        public async Task<IActionResult> CharacterUpdate()
        {           
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterUpdateRequest.Parser.ParseFrom(stream);
            var result = await _characterService.UpdateCharacter(requestObject);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // DELETE api/character
        [HttpPost("delete")]
        public async Task<IActionResult> CharacterDelete()
        {
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterDeleteRequest.Parser.ParseFrom(stream);
            await _characterService.DeleteCharacter(requestObject);
            return Ok();
        }

        [HttpPut("lvlboost")]
        public async Task<IActionResult> CharecterLevelBoost()
        {
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterLevelBoostRequest.Parser.ParseFrom(stream);
            var character = await _characterService.CharacterLevelBoost(requestObject);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }

        [HttpPost("addgroup")]
        public async Task<IActionResult> CharacterAddGroup()
        {
            var stream = Request.BodyReader.AsStream();
            var requestObject = CharacterAddGroupRequest.Parser.ParseFrom(stream);
            var character = await _characterService.CharacterAddGroup(requestObject);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }
       

        [HttpPut("abilityBoost")]
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
