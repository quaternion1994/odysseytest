using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System.Threading.Tasks;

namespace OdysseyServer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GroupByIdResponse> GetGroupByIdAsync(long groupId)
        {
            return new GroupByIdResponse
            {
                Group = _mapper.Map<Group>(await _unitOfWork.Group.GetByID(groupId))
            };
        }

        public async Task<GroupAddResponse> CreateGroupAsync(GroupAddRequest requestObject)
        {
            GroupDbo groupDbo = _mapper.Map<GroupDbo>(requestObject.Group);
            await _unitOfWork.Group.Insert(groupDbo);

            GroupAddResponse result = new GroupAddResponse
            {
                Group = requestObject.Group
            };
            
            requestObject.Group.Id = groupDbo.Id;
            return result;
        }

        public async Task<GroupAllResponse> GetAllGroupsAsync()
        {
            GroupAllResponse result = new GroupAllResponse
            {
                Groups = new AllGroup()
            };
            _mapper.Map(await _unitOfWork.Group.Get(), result.Groups.Groups);
            return result;
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            var groupDbo = _mapper.Map<GroupDbo>(group);
            await _unitOfWork.Group.Update(groupDbo);
            var groupDboUpdated = await _unitOfWork.Group.GetByID(group.Id);
            var result = _mapper.Map<Group>(groupDboUpdated);
            return result;
        }

        public async Task DeleteGroupAsync(long groupId)
        {
            await _unitOfWork.Group.Delete(groupId);
        }
    }
}
