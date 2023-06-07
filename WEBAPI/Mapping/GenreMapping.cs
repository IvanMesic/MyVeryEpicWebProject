using WEBAPI.BLModels;
using WEBAPI.Models;

namespace WEBAPI.Mapping
{
    public class GenreMapping
    {
        public static IEnumerable<BLGenre> MapToBl(IEnumerable<Genre> genres) => genres.Select(x => MapToBl(x));

        public static BLGenre MapToBl(Genre genre) =>
            new BLGenre
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description,
            };

        public static IEnumerable<Genre> MapToDAL(IEnumerable<BLGenre> blgenre)
                => (IEnumerable<Genre>)blgenre.Select(x => MapToDAL(x));


        public static Genre MapToDAL(BLGenre genre) =>
                new Genre
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Description = genre.Description
                };

    }
}

