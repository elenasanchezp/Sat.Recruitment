using Sat.Recruitment.Api.Common;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

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
        public Result ValidateErrorsOnCreateUser(string name, string email, string address, string phone, string userTypeAsString, string moneyAsString)
        {
            List<string> errorsList = new List<string>();

            //Validate if Name is null
            if (string.IsNullOrEmpty(name))
                errorsList.Add(Constants.USER_NAME_REQUIRED);

            //Validate if Email is null or Empty
            if (string.IsNullOrEmpty(email))
            {
                errorsList.Add(Constants.USER_EMAIL_REQUIRED);
            }
            else if (!IsValidEmail(email))
                //Validate if Email is correct
                errorsList.Add(Constants.USER_EMAIL_INCORRECT);

            //Validate if Address is null
            if (string.IsNullOrEmpty(address))
                errorsList.Add(Constants.USER_ADDRESS_REQUIRED);

            //Validate if UserType is null or Empty
            Enum.TryParse(userTypeAsString, out UserType userType);
            if (string.IsNullOrEmpty(userTypeAsString))
            {
                errorsList.Add(Constants.USER_USERTYPE_REQUIRED);
            }
            else if (!Enum.IsDefined(typeof(UserType), userType))
                //Validate if UserType is correct
                errorsList.Add(Constants.USER_USERTYPE_INCORRECT);

            //Validate if Phone is null
            if (string.IsNullOrEmpty(phone))
                errorsList.Add(Constants.USER_PHONE_REQUIRED);

            //Validate Money value
            if (string.IsNullOrEmpty(moneyAsString))
            {
                errorsList.Add(Constants.USER_MONEY_REQUIRED);
            } 
            else if (!int.TryParse(moneyAsString, out int money))
                //Validate if Money is correct
                errorsList.Add(Constants.USER_MONEY_INCORRECT);
    
            return new Result()
            {
                IsSuccess = !errorsList.Any(),
                Errors = errorsList
            };
        }

        #region "Private methods"

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch 
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        #endregion
    }
}
