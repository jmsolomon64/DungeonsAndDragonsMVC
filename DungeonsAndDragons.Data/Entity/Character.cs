using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Data.Entity
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Race))]
        public int RaceId { get; set;}

        public virtual Race Race { get; set; }

        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }

        public virtual Classes Class { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int Strength { get; set; }
        [Required]
        public int Dexterity { get; set; }
        [Required]
        public int Consitution { get; set; }
        [Required]
        public int Inteligence { get; set; }
        [Required]
        public int Wisdom { get; set; }
        [Required]
        public int Charisma { get; set; }

        public virtual ICollection<Equipment> Inventory { get; set; }

        [MaxLength(50000)]
        public string Description { get; set; }

        public Character()
        {
            Inventory = new List<Equipment>();
        }
    }
}
