using AutoMapper;
using Common.BLModels;
using Common.DALModels;
using Common.Interfaces;

namespace Common.Repositories
{
    public class UserReposi : IUserReposi
    {
        private readonly RwaMoviesContext _context;
        private readonly IMapper _mapper;
        public UserReposi(RwaMoviesContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<BLUser> GetAll()
        {
            var dbUsers = _context.Users;

            var blUsers = _mapper.Map<IEnumerable<BLUser>>(dbUsers);
            return blUsers;
        }

        public BLUser GetSpecific(int id)
        {
            var dbUser = _context.Users.Find(id);
            var blUser = _mapper.Map<BLUser>(dbUser);
            return blUser;
        }

        public void Add(BLUser user)
        {
            var dbUser = _mapper.Map<User>(user);
            _context.Users.Add(dbUser);
            _context.SaveChanges();
        }

        public void Update(BLUser user)
        {
            var dbUser = _mapper.Map<User>(user);
            _context.Users.Update(dbUser);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbUser = _context.Users.Find(id);
            _context.Users.Remove(dbUser);
            _context.SaveChanges();
        }
    }
}
