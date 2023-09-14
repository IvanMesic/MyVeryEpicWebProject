using AutoMapper;
using Common.BLModels;
using Common.DALModels;

namespace WEBAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BLVideo, Video>().ReverseMap();
            CreateMap<Video, BLVideo>().ReverseMap();
            // Add other mappings as needed
        }
    }
}
