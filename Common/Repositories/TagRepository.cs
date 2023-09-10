using AutoMapper;
using Common.BLModels;
using Common.DALModels;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly RwaMoviesContext _context; // Replace with your actual DbContext
        private readonly IMapper _mapper;

        public TagRepository(RwaMoviesContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<BLTag> GetAll()
        {
            var dbTags = _context.Tags;
            var blTags = _mapper.Map<IEnumerable<BLTag>>(dbTags);
            return blTags;
        }

        public BLTag GetSpecific(int id)
        {
            var dbTag = _context.Tags.Find(id);
            var blTag = _mapper.Map<BLTag>(dbTag);
            return blTag;
        }

        public void Add(BLTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var dbTag = _mapper.Map<Tag>(tag);
            _context.Tags.Add(dbTag);
            _context.SaveChanges();
        }

        public void Update(BLTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var dbTag = _mapper.Map<Tag>(tag);
            _context.Tags.Update(dbTag);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag != null)
            {
                // Remove references in VideoTag table
                var videoTagsToRemove = _context.VideoTags.Where(vt => vt.TagId == id);
                _context.VideoTags.RemoveRange(videoTagsToRemove);

                // Now you can safely delete the Tag
                _context.Tags.Remove(tag);

                // Save changes
                _context.SaveChanges();
            }
        }
    }
}
