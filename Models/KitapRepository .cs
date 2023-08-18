using Mvc_Project.Utility;
using System.Linq.Expressions;

namespace Mvc_Project.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        private readonly ProjectDbContext _context;
        public KitapRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Kitap kitap)
        {
            _context.Update(kitap);
        }
    }
}
