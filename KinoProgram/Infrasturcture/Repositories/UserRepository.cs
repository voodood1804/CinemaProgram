using KinoProgram.Application.models;
using KinoProgram.Infrasturcture;

namespace KinoProgram.Application.Infrasturcture.Repositories
{
    public class UserRepository : Repository<User, int>
    {
        private readonly ICryptService _cryptService;
        public UserRepository(CinemaContext db, ICryptService cryptService) : base(db)
        {
            _cryptService = cryptService;
        }
    }

}
