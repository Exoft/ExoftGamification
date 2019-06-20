﻿using System;

namespace Exoft.Gamification.Api.Common.Models
{
    public class JwtTokenModel
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenExpiration { get; set; }
    }
}
