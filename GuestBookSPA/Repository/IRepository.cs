using GuestBookSPA.Models;

namespace GuestBookSPA.Repository
{
    public interface IRepository
    { 
        Task<List<Messages>> GetAll();
        Task<Messages> GetById(int id);
        Task Create(Messages mes);
        void Update(Messages mes);
        Task Delete(int id);
        Task Save();

    }
}
