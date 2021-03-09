using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using OdysseyServer.Services.Converters;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<GroupByIdResponse> GetGroupById(GroupByIdRequest requestObject)
        {
            var groupDbo = await _unitOfWork.Group.GetByID(requestObject.GroupId);
            var group = new Group();
            group = Converter.GroupDboToGroup(group, groupDbo);            
            var result = new GroupByIdResponse
            {
                Group = group
            };
            return result;
        }

        public async Task<GroupAddResponse> CreateGroup(GroupAddRequest requestObject)
        {
            var groupDbo = _mapper.Map<GroupDbo>(requestObject.Group);
            await _unitOfWork.Group.Insert(groupDbo);
            var group = new Group();
            group = Converter.GroupDboToGroup(group, groupDbo);
            var result = new GroupAddResponse
            {
                Group = group
            };
            return result;
        }

        public async Task<GroupAllResponse> GetAllGroups()
        {
            var groupsDbo = await _unitOfWork.Group.Get();
            var listOfGroup = new List<Group>();
            foreach (var elem in groupsDbo)
            {
                var group = new Group();
                group = Converter.GroupDboToGroup(group, elem);
                listOfGroup.Add(group);
            }
            var result = new GroupAllResponse
            {
                Groups = new AllGroup()
            };
            result.Groups.Group.AddRange(listOfGroup);
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

        public async Task DeleteGroup(GroupDeleteRequest requestObject)
        {
            await _unitOfWork.Group.Delete(requestObject.GroupId);
        }
    }
}
