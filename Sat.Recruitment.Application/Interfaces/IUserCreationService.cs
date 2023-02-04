using Sat.Recruitment.Model.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUserCreationService
    {
        Task<Result> CreateUserAsync(string name, string email, string address, string phone, string userType, string money);
    }
}
