using OdysseyServer.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services.Contracts
{
    public interface IGroupService
    {
        Task<GroupAddResponse> CreateGroupAsync(GroupAddRequest requestObject);
        Task<GroupByIdResponse> GetGroupByIdAsync(long groupId);
        Task DeleteGroupAsync(long groupId);
        Task<GroupAllResponse> GetAllGroupsAsync();
    }
}
