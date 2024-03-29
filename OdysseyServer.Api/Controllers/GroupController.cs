﻿using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OdysseyServer.ApiClient;
using OdysseyServer.Services.Contracts;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OdysseyServer.Api.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : OdysseyControllerBase
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
        public async Task<IActionResult> GetAllGroupsAsync()
        {
            var cachedGroups = await _distributedCache.GetAsync(cacheKey);
            if (cachedGroups != null)
            {
                return File(cachedGroups, "application/octet-stream");
            }
            else
            {
                GroupAllResponse groups = await _groupService.GetAllGroupsAsync();
                byte[] data = groups.ToByteArray();
                DistributedCacheEntryOptions cacheExpirationOptions = new DistributedCacheEntryOptions()
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
        public async Task<IActionResult> GetGroupByIdAsync(long id)
        {
            return Protobuf(await _groupService.GetGroupByIdAsync(id));
        }

        /// <summary>
        /// Accepts GroupAddRequest
        /// </summary>
        [HttpPost]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(GroupAddResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGroupAsync(GroupAddRequest requestObject)
        {
            return Protobuf(await _groupService.CreateGroupAsync(requestObject));
        }

        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GroupDeleteAsync(long id)
        {
            await _groupService.DeleteGroupAsync(id);
            return Ok();
        }
    }
}
