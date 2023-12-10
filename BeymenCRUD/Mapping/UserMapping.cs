using AutoMapper;
using BeymenCRUD.Data;
using BeymenCRUD.Models.Requests;

namespace BeymenCRUD.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            // CreateMap<Source, Target>

            CreateMap<User, UserPostRequest>();
            CreateMap<UserPostRequest, User>();

            CreateMap<User, UserPutRequest>();
            CreateMap<UserPutRequest, User>();

        }
    }
}
