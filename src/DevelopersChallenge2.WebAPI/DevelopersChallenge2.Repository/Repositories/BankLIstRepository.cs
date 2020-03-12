using DevelopersChallenge2.Domain;
using DevelopersChallenge2.Repository.Interfaces;

namespace DevelopersChallenge2.Repository.Repositories
{
    public class BankLIstRepository : BaseRepository<BankList>, IBankListRepository
    {
        public BankLIstRepository(DevelopersChallenge2Context context) : base(context) { } 
    }
}
