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
        [Range(1, 20)]
        public int Level { get; set; }
        [Required]
        [Range(1, 24)]
        public int Strength { get; set; }
        [Required]
        [Range(1, 24)]
        public int Dexterity { get; set; }
        [Required]
        [Range(1, 24)]
        public int Consitution { get; set; }
        [Required]
        [Range(1, 24)]
        public int Inteligence { get; set; }
        [Required]
        [Range(1, 24)]
        public int Wisdom { get; set; }
        [Required]
        [Range(1, 24)]
        public int Charisma { get; set; }
        [MaxLength(50000)]
        public string Description { get; set; }
    }
}
