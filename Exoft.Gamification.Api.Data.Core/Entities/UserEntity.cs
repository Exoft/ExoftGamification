﻿using System.Collections.Generic;

namespace Exoft.Gamification.Api.Data.Core.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Status { get; set; }

        public string Avatar { get; set; }
        
        public int XP { get; set; }

        public ICollection<UserAchievementsEntity> Achievements { get; set; }


        public UserEntity()
        {
            Achievements = new List<UserAchievementsEntity>();
        }
    }
}
