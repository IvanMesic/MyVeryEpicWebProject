using WEBAPI.BLModels;
using WEBAPI.Models;

namespace WEBAPI.Mapping
{
    public class Tag_Mapping
    {

        public static IEnumerable<BLTag> MapToBl(IEnumerable<Tag> tags) => tags.Select(x => MapToBl(x));

        public static BLTag MapToBl(Tag tag) =>
        new()
        {
            Id = tag.Id,
            Name = tag.Name
        };

        public static IEnumerable<Tag> MapToDAL(IEnumerable<BLTag> bltags)
            => (IEnumerable<Tag>)bltags.Select(x => MapToDAL(x));


        public static Tag MapToDAL(BLTag tag) =>
            new()
            {
                Id = tag.Id,
                Name = tag.Name,
            };
    }
}

