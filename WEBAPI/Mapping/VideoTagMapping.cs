using Common.DALModels;
using WEBAPI.BLModels;
using WEBAPI.Models;

namespace WEBAPI.Mapping
{
    public class VideoTagMapping
    {
        public static IEnumerable<BLVideoTags> MapToBl(IEnumerable<VideoTag> videotags) => videotags.Select(x => MapToBl(x));


        public static BLVideoTags MapToBl(VideoTag videotag) =>
            new BLVideoTags
            {
                Id = videotag.Id,
                TagId = videotag.TagId,
                VideoId = videotag.VideoId,
            };
        public static IEnumerable<VideoTag> MapToDAL(IEnumerable<BLVideoTags> blvideotags)
            => (IEnumerable<VideoTag>)blvideotags.Select(x => MapToDAL(x));


        public static VideoTag MapToDAL(BLVideoTags videotags) =>
            new VideoTag
            {
                Id = videotags.Id,
                TagId = videotags.TagId,
                VideoId = videotags.VideoId
            };


    }
}
