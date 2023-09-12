using AutoMapper;
using Common.BLModels;
using Common.DALModels;
using MyEpicMVCProj.ViewModels;

namespace MyEpicMVCProj.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Common.BLModels.BLUser, Models.VMUser>();
            CreateMap<Common.BLModels.BLVideo, ViewModels.VMVideo>();
            CreateMap<ViewModels.VMVideo, Common.BLModels.BLVideo>();
            //create tag between BLTag and VMTag
            CreateMap<Common.BLModels.BLTag, ViewModels.VMTag>();
            CreateMap<ViewModels.VMTag, Common.BLModels.BLTag>();
            CreateMap<Country, BLCountry>();
            CreateMap<BLCountry, Country>();
            //create Map between BLCountry and VMCountry
            CreateMap<Common.BLModels.BLCountry, ViewModels.VMCountry>();
            CreateMap<ViewModels.VMCountry, Common.BLModels.BLCountry>();
        }
    }
}
