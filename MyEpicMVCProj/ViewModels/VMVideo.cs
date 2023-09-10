using Common.DALModels;
using System.ComponentModel.DataAnnotations;

namespace MyEpicMVCProj.ViewModels
{
    public class VMVideo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Genre ID is required")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Total seconds is required")]
        public int TotalSeconds { get; set; }

        [Required(ErrorMessage = "Streaming URL is required")]
        public string? StreamingUrl { get; set; }

        public int? ImageId { get; set; }

        public virtual Genre Genre { get; set; } = null!;

        public virtual Image? Image { get; set; }

        public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();
    }
}
