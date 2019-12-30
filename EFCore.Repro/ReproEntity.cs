using System.ComponentModel.DataAnnotations;

namespace EFCore.Repro
{
    public class ReproEntity
    {
        public int ID { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
