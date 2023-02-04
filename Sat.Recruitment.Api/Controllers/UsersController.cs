using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Sat.Recruitment.Model.Models;
using Sat.Recruitment.Application.Interfaces;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserCreationService userCreationService;

        private readonly IUserValidationService userValidationService;

        public UsersController(IUserCreationService userCreationService, IUserValidationService userValidationService)
        {
            this.userCreationService = userCreationService;
            this.userValidationService = userValidationService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            // Valide error before creating User
            var result = this.userValidationService.ValidateErrorsOnCreateUser(name, email, address, phone, userType, money);

            if (!result.IsSuccess)
            {
                return result;
            }

            // Create User
            return await this.userCreationService.CreateUserAsync(name, email, address, phone, userType, money);
        }
    }
}
