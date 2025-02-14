using AutoMapper;
using CON = GameNightScoresRight.ControllerDTOs;
using CD = GameNightScoresRight.CommonDTOs;

namespace GameNightScoresRight.MappingProfiles
{
    public class ControllerCommonMappingProfile : Profile
    {
        public ControllerCommonMappingProfile()
        {
            CreateMap<CON.CreateAccountRequest, CD.CreateAccountRequest>()
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<CD.CreateAccountResponse, CON.CreateAccountResponse>()
                .ForAllMembers(opt => opt.Ignore());
        }
    }
}
