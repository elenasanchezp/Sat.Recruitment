using Sat.Recruitment.Api.Common;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Services
{
    public class UserCreationService : IUserCreationService
    {
        private readonly IUsersService userRepository;

        public UserCreationService(IUsersService userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Result> CreateUserAsync(string name, string email, string address, string phone, string userTypeString, string money)
        {
            Enum.TryParse(userTypeString, out UserType userType);

            var newMoney = CalculateMoney(userType, decimal.Parse(money));
            
            var normalizedEmail = NormalizeEmailOnCreateUser(email);
            if (normalizedEmail == string.Empty)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { Constants.USER_EMAIL_INCORRECT }
                };
            }
            
            var newUser = new UserViewModel
            {
                Name = name,
                Email = normalizedEmail,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = newMoney
            };

            var users = await userRepository.GetUsersAsync();

            var isUserDuplicated = (users.Any(u => IsUserDuplicated(u, newUser)));

            return new Result()
            {
                IsSuccess = !isUserDuplicated,
                Errors = isUserDuplicated ? new List<string>() { Constants.USER_DUPLICATED } : new List<string>()
            };
        }

        #region "Private methods"

        /// <summary>
        /// Get new value of money by userType
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="money"></param>
        /// <returns>Calculated value of money</returns>
        private static decimal CalculateMoney(UserType userType, decimal money)
        {
            decimal percentage = 0;

            switch (userType)
            {
                case UserType.Normal:
                    if (money > 100)
                    {
                        percentage = Convert.ToDecimal(0.12);
                    }
                    else if (money > 10 && money < 100)
                    {
                        percentage = Convert.ToDecimal(0.8);
                    }
                    break;

                case UserType.SuperUser:
                    percentage = money > 100 ? Convert.ToDecimal(0.2) : 0;
                    break;
                case UserType.Premium:
                    percentage = money > 100 ? 2 : 0;
                    break;
                default:
                    break;
            }

            var gif = money * percentage;
            return (money + gif);
        }

        /// <summary>
        /// Normalize the email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Email normalized</returns>
        private static string NormalizeEmailOnCreateUser(string email)
        {
            try
            {
                var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                return string.Join("@", new string[] { aux[0], aux[1] });
            }
            catch
            {
                return string.Empty;
            }
            
        }

        /// <summary>
        /// Check if user is duplicated
        /// </summary>
        /// <param name="u1"></param>
        /// <param name="u2"></param>
        /// <returns>True if user is duplicate, False in another case</returns>
        private static bool IsUserDuplicated(UserViewModel u1, UserViewModel u2)
        {
            return u1.Email == u2.Email || u1.Phone == u2.Phone || (u1.Name == u2.Name && u1.Address == u2.Address);
        }

        #endregion 
    }
}
