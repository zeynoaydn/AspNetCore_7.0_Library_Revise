using Mvc_Project.Utility;
using System.Linq.Expressions;

namespace Mvc_Project.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
    {
        private readonly ProjectDbContext _context;
        public KitapTuruRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(KitapTuru kitapTuru)
        {
            _context.Update(kitapTuru);
        }
    }
}
