using System.Threading.Tasks;

namespace Shop.Core.DataAccess.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}