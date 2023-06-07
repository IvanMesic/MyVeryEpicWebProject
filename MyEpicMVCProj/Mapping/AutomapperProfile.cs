using AutoMapper;


namespace MyEpicMVCProj.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Common.BLModels.BLUser, Models.VMPerson>();
        }
    }
}
