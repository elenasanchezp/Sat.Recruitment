using Sat.Recruitment.Api.Common;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Services
{
    public class UsersService : IUsersService
    {
        /// <summary>
        /// Get list of users from file 
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            return ReadUsersFromFile();
        }

        #region "Private methods"

        /// <summary>
        /// Read user line from file path
        /// </summary>
        /// <returns>User view model list</returns>
        private List<User> ReadUsersFromFile()
        {
            try
            {
                var output = new List<User>();

                var path = $"{Directory.GetCurrentDirectory()}{Constants.USER_FILE_PATH}";

                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        while (reader.Peek() >= 0)
                        {
                            var line = reader.ReadLineAsync().Result;

                            User user = GetUserByLine(line);

                            output.Add(user);
                        }
                        reader.Close();
                    }
                }

                return output;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get user detail from a line
        /// </summary>
        /// <param name="line"></param>
        /// <returns>User detail as Model</returns>
        private User GetUserByLine(string line)
        {
            try
            {
                var splittedLine = line.Split(Constants.USER_FILE_PARAMS_SEPARATOR);

                Enum.TryParse(splittedLine[4], out UserType userType);

                var user = new User
                {
                    Name = splittedLine[0],
                    Email = splittedLine[1],
                    Phone = splittedLine[2],
                    Address = splittedLine[3],
                    UserType = userType,
                    Money = decimal.Parse(splittedLine[5]),
                };

                return user;
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}
