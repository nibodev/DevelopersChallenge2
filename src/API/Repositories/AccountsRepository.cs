using System.Threading.Tasks;
using API.DataContext;
using API.Domain;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountsRepository : IAccoutsRepository
    {
        private ReconcileContext _context;

        public AccountsRepository(ReconcileContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> Get(string id)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}