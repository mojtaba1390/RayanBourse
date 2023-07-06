using AutoMapper;
using RayanBourse.Domain.Entities;
using RayanBourse.Models;

namespace RayanBourse.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserModel>();

        }
    }
}
