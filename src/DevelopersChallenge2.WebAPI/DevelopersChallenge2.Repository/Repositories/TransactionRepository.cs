using DevelopersChallenge2.Domain;
using DevelopersChallenge2.Repository.Interfaces;

namespace DevelopersChallenge2.Repository.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DevelopersChallenge2Context context) : base(context) { }
    }
}
