using Microsoft.EntityFrameworkCore;
using GuestBookSPA.Models;

namespace GuestBookSPA.Repository
{
    public class GBookRepository : IRepository
    {
        private readonly UserContext _context;
        public GBookRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<List<Messages>> GetMessageList()
        {
            return await _context.Messages.Include(p => p.User).ToListAsync();

        }
        public async Task Create(Messages mes)
        {
            await _context.Messages.AddAsync(mes);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
