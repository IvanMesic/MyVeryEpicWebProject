using Common.DALModels;

namespace MyEpicMVCProj.Models
{
    public class VMPerson
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;


        public string? Phone { get; set; }


        public virtual Country CountryOfResidence { get; set; } = null!;
    }
}
