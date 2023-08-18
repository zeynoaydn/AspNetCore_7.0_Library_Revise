namespace Mvc_Project.Models
{
    public interface IKiralamaRepository:IRepository<Kiralama>
    {
        void Update(Kiralama kiralama);
        void Save();
    }
}
