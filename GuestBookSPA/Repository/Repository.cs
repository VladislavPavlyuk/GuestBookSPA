using Microsoft.EntityFrameworkCore;
using GuestBookSPA.Models;

namespace GuestBookSPA.Repository
{
    public class Repository : IRepository
    {
        private readonly GuestBookContext _context;
        public Repository(GuestBookContext context)
        {
            _context = context;
        }

        public async Task<List<Messages>> GetAll()
        {
            return await _context.Messages.ToListAsync();

        }
        public async Task<Messages> GetById(int id)
        {
            return await _context.Messages.FindAsync(id);
        }


        public async Task Create(Messages mes)
        {
            await _context.Messages.AddAsync(mes);
        }
        public void Update(Messages mes)
        {
            _context.Entry(mes).State = EntityState.Modified;            
        }
        public async Task Delete(int id)
        {
            Messages? mes = await _context.Messages.FindAsync(id);
            if (mes != null)
                _context.Messages.Remove(mes);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
