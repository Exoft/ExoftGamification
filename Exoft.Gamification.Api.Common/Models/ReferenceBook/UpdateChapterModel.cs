﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exoft.Gamification.Api.Common.Models.ReferenceBook
{
    public class UpdateChapterModel : BaseChapterModel
    {
        public Guid Id { get; set; }

        public ICollection<UpdateArticleModel> Articles { get; set; }

        public int OrderId { get; set; }
    }
}
