using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OdysseyServer.ApiClient;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OdysseyServer.Api.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IDistributedCache _distributedCache;
        private const string cacheKey = "allGroups";

        public GroupController(IGroupService groupService, IDistributedCache distributedCache)
        {
            _groupService = groupService;
            _distributedCache = distributedCache;
        }

        [HttpGet("/api/groups")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(GroupAllResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGroups()
        {            
            var cachedGroups = await _distributedCache.GetAsync(cacheKey);
            if (cachedGroups != null)
            {
                return File(cachedGroups, "application/octet-stream");
            }
            else
            {
                var groups = await _groupService.GetAllGroups();
                var data = groups.ToByteArray();
                var cacheExpirationOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                await _distributedCache.SetAsync(cacheKey, data, cacheExpirationOptions);
                return File(data, "application/octet-stream");
            }         
        }

        [HttpGet]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(GroupByIdResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGroupById(long id)
        {
            var ability = await _groupService.GetGroupById(id);
            var data = ability.ToByteArray();
            return File(data, "application/octet-stream");
        }

        /// <summary>
        /// Accepts GroupAddRequest
        /// </summary>
        [HttpPost]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(GroupAddResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGroup()
        {
            var stream = Request.BodyReader.AsStream();
            var requestObject = GroupAddRequest.Parser.ParseFrom(stream);
            var result = await _groupService.CreateGroup(requestObject);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GroupDelete(long id)
        {
            await _groupService.DeleteGroup(id);
            return Ok();
        }
    }
}
