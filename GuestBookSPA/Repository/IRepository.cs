using GuestBookSPA.Models;

namespace GuestBookSPA.Repository
{
    public interface IRepository
    { 
        Task<List<Messages>> GetMessageList();
        Task Create(Messages mes);
        Task Save();

    }
}
