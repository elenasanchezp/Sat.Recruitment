﻿using System.Collections.Generic;

namespace Sat.Recruitment.Model.Models
{
    /// <summary>
    /// Contains data relative to a results
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; }
    }
}