﻿using Common.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BLModels
{
    public class BLVideo
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int GenreId { get; set; }

        public int TotalSeconds { get; set; }

        public string? StreamingUrl { get; set; }

        public int? ImageId { get; set; }

        public virtual Genre Genre { get; set; } = null!;

        public virtual Image? Image { get; set; }

        public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();
    }
}
