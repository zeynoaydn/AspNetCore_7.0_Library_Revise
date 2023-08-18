namespace Mvc_Project.Models
{
    public interface IKitapTuruRepository:IRepository<KitapTuru>
    {
        void Update(KitapTuru kitapTuru);
        void Save();
    }
}
