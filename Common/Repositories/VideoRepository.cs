using AutoMapper;
using Common.BLModels;
using Common.DALModels;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Common.Repositories
{
    public class VideoRepository : IVideoRepo
    {
        private readonly RwaMoviesContext _context;
        private readonly IMapper _mapper;

        public VideoRepository(RwaMoviesContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<BLVideo> GetAll(int page, int pageSize)
        {
            var dbVideos = _context.Videos;
            int itemsToSkip = (page - 1) * pageSize;

            // Apply paging to the query
            var pagedQuery = dbVideos.Skip(itemsToSkip).Take(pageSize);

            // Project the results into BLVideo using AutoMapper
            var blVideos = _mapper.Map<IEnumerable<BLVideo>>(pagedQuery);
            return blVideos;
        }

        public BLVideo GetSpecific(int id)
        {
            var dbVideo = _context.Videos.Find(id);
            var blVideo = _mapper.Map<BLVideo>(dbVideo);
            return blVideo;
        }

        public void Add(BLVideo video)
        {
            var dbVideo = _mapper.Map<Video>(video);
            _context.Videos.Add(dbVideo);
            _context.SaveChanges();
        }

        public void Update(BLVideo video)
        {
            var dbVideo = _mapper.Map<Video>(video);
            _context.Videos.Update(dbVideo);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbVideo = _context.Videos.Find(id);
            _context.Videos.Remove(dbVideo);
            _context.SaveChanges();
        }
        public IEnumerable<Video> GetVideosForPage(int page, int pageSize)
        {
            return _context.Videos
                .OrderBy(v => v.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalCount()
        {
            return _context.Videos.Count();
        }

        public IEnumerable<Video> GetVideosForPageAndFilter(int page, int pageSize, string? filterName = null, bool ascendingOrder = true)
        {
            var query = _context.Videos.AsQueryable();

            if (!string.IsNullOrEmpty(filterName))
            {
                query = query.Where(v => v.Name.Contains(filterName));
            }

            if (ascendingOrder)
            {
                query = query.OrderBy(v => v.Name);
            }
            else
            {
                query = query.OrderByDescending(v => v.Name);
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Video> GetFilteredVideos(string filterName, int page, int pageSize)
        {
            return _context.Videos
                .Where(v => v.Name.Contains(filterName))
                .OrderBy(v => v.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public int GetTotalCount(string filterName) => _context.Videos.Where(v => v.Name.Contains(filterName)).Count();
    }
}
