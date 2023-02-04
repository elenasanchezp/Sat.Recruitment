using Sat.Recruitment.Application.ViewModels;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUserValidationService
    {
        Result ValidateErrorsOnCreateUser(string name, string email, string address, string phone, string userType, string money);
    }
}
