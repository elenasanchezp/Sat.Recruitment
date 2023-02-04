﻿using Sat.Recruitment.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUsersService
    {
        Task<List<UserViewModel>> GetUsersAsync();
    }
}
