namespace Mvc_Project.Models
{
    public interface IKitapRepository:IRepository<Kitap>
    {
        void Update(Kitap kitap);
        void Save();
    }
}
