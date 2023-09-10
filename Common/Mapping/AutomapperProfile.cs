using AutoMapper;
using Common.BLModels;

namespace Common.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DALModels.User, BLModels.BLUser>();
            CreateMap<DALModels.Video, BLModels.BLVideo>();
            CreateMap<BLModels.BLVideo, DALModels.Video>();
            //Create Map between Tag and BLTag
            CreateMap<DALModels.Tag, BLModels.BLTag>();
            CreateMap<BLModels.BLTag, DALModels.Tag>();
            //Create Map between Country and VM COuntry
            CreateMap<DALModels.Country, BLModels.BLCountry>();
            CreateMap<BLModels.BLCountry, DALModels.Country>();
        }
    }
}
