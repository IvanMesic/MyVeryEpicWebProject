using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WEBAPI.Models;

namespace WEBAPI.BLModels
{
    public class BLVideoTags
    {
        [Key]
        public int Id { get; set; }

        public int VideoId { get; set; }

        public int TagId { get; set; }
    }
}
