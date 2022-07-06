using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Model.Character
{
    public class CharacterCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="Race")]
        public int RaceId { get; set; }
        [Required]
        [Display(Name="Class")]
        public int ClassId { get; set; }
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
        [MaxLength(50000)]
        public string Description { get; set; }
    }
}
