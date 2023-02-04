using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Application.Services
{
    public class UserValidationService : IUserValidationService
    {
        /// <summary>
        /// Validate several errors related with the empty values and incorrect formats 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="userTypeAsString"></param>
        /// <param name="money"></param>
        /// <returns>Error list</returns>
        public Result ValidateErrorsOnCreateUser(string name, string email, string address, string phone, string userTypeAsString, string money)
        {
            List<string> errorsList = new List<string>();

            //Validate if Name is null
            if (name == null)
                errorsList.Add("The name is required");

            //Validate if Email is null
            if (email == null || email == string.Empty)
                errorsList.Add("The email is required");

            //Validate if Address is null
            if (address == null)
                errorsList.Add("The address is required");

            //Validate if UserType is correct
            Enum.TryParse(userTypeAsString, out UserType userType);
            if (userType == 0)
                errorsList.Add("The userType is incorrect");

            //Validate if Phone is null
            if (phone == null)
                errorsList.Add("The phone is required");

            //Validate Money value
            try
            {
                decimal.Parse(money);
            }
            catch
            {
                errorsList.Add("The money value is not parseable");
            }

            return new Result()
            {
                IsSuccess = !errorsList.Any(),
                Errors = errorsList
            };
        }
    }
}
