using Mvc_Project.Utility;
using System.Linq.Expressions;

namespace Mvc_Project.Models
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
    {
        private readonly ProjectDbContext _context;
        public KiralamaRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Kiralama kiralama)
        {
            _context.Update(kiralama);
        }
    }
}
