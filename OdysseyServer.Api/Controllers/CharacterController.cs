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
        //private IMemoryCache _cache;
        private ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        // GET: api/<CharacterController>
        [HttpGet("allcharacters")]
        public async Task<IActionResult> GetAllCharacter()
        {
            var characters = await _characterService.GetAllCharacters();
            var data = characters.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // GET api/character
        [HttpGet("")]
        public async Task<IActionResult> GetCharacterById(long id)
        {
            var character = await _characterService.GetCharacterById(id);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // POST api/character
        [HttpPost]
        public async Task<IActionResult> Post()
        {            
            
            var stream = Request.BodyReader.AsStream();
            var person = Character.Parser.ParseFrom(stream);
            await _characterService.CreateCharacter(person);
           
            return Ok();
        }

        // PUT api/character
        [HttpPut("")]
        public async Task<IActionResult> CharacterUpdate()
        {           
            var stream = Request.BodyReader.AsStream();
            var person = Character.Parser.ParseFrom(stream);
            var result = await _characterService.UpdateCharacter(person);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // DELETE api/character
        [HttpDelete("")]
        public async Task<IActionResult> CharacterDelete(long id)
        {
            await _characterService.DeleteCharacter(id);
            return Ok();
        }

        // PUT api/character/lvlboost
        [HttpPut("lvlboost")]
        public async Task<IActionResult> CharecterLevelBoost(long id, int lvlNumber)
        {
            var character = await _characterService.CharacterLevelBoost(id, lvlNumber);
            var data = character.ToByteArray();
            return File(data, "application/octet-stream");
        }
    }
}
