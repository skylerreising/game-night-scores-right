using AutoMapper;
using CD = GameNightScoresRight.CommonDTOs;
using EF = GameNightScoresRight.EFDTOs;
using GameNightScoresRight.Enums;

namespace GameNightScoresRight.MappingProfiles
{
    public class CommonEFMappingProfile : Profile
    {
        public CommonEFMappingProfile()
        {
            CreateMap<CD.CreateAccountRequest, EF.Account>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress.Trim()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role ?? Role.User))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<EF.Account, CD.CreateAccountResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<CD.CreateUserRequest, EF.User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.AccountId, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Pronouns, opt => opt.MapFrom(src => src.Pronouns))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            CreateMap<EF.User, CD.CreateUserResponse>();
        }
    }
}
