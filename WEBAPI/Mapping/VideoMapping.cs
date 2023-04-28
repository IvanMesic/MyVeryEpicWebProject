using WEBAPI.BLModels;
using WEBAPI.Models;

namespace WEBAPI.Mapping
{
    public class VideoMapping
    {
        public static IEnumerable<BLVideo> MapToBl(IEnumerable<Video> videos) => videos.Select(x => MapToBl(x));


        public static BLVideo MapToBl(Video video) =>
            new BLVideo
            {
                Id = video.Id,
                Name = video.Name,
                Description = video.Description,
                GenreId = video.GenreId,
                TotalSeconds = video.TotalSeconds,
                StreamingUrl = video.StreamingUrl,
                ImageId = video.ImageId,
            };
        public static IEnumerable<Video> MapToDAL(IEnumerable<BLVideo> blvideos) 
            => (IEnumerable<Video>)blvideos.Select(x => MapToDAL(x));


        public static Video MapToDAL(BLVideo video) =>
            new  Video
            {
                Id = video.Id,
                Name = video.Name,
                Description = video.Description,
                GenreId = video.GenreId,
                TotalSeconds = video.TotalSeconds,
                StreamingUrl = video.StreamingUrl,
                ImageId = video.ImageId,
            };


    };
}
