using AutoMapper;
using CD = GameNightScoresRight.CommonDTOs;
using EF = GameNightScoresRight.EFDTOs;
using GameNightScoresRight.Enums;
using Microsoft.EntityFrameworkCore;

namespace GameNightScoresRight.MappingProfiles
{
    public class CommonEFMappingProfile : Profile
    {
        public CommonEFMappingProfile()
        {
            CreateMap<CD.CreateAccountRequest, EF.Account>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role ?? Role.User))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<EF.Account, CD.CreateAccountResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<CD.CreateUserRequest, EF.User>();

            CreateMap<EF.User, CD.CreateUserResponse>();
        }
    }
}
