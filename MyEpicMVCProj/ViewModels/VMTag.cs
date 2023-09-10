using Common.DALModels;

namespace MyEpicMVCProj.ViewModels
{
    public class VMTag
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();
    }
}
