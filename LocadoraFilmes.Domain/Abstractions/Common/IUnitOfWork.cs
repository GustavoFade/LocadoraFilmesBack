using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Abstractions.Common
{
    public interface IUnitOfWork
    {
        Task<bool> CommitTransactionAsync();
    }
}
