using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Data.Entity
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Character> Characters { get; set; }

        public Equipment()
        {
            Characters = new List<Character>();
        }
    }
}
