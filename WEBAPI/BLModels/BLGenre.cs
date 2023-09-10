using Common.DALModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEBAPI.Models;

namespace WEBAPI.BLModels
{
    public class BLGenre
    {

        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; } = null!;

        [StringLength(1024)]
        public string? Description { get; set; }

        [InverseProperty("Genre")]
        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
