using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Data.Entity
{
    public class Race
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(10000)]
        public string Description { get; set; }

        public ICollection<Character> Characters { get; set; } //Denotes a many relationship

        public Race()
        {
            Characters = new HashSet<Character>();
        }
    }
}
