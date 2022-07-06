using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Model.Race
{
    public class RaceCreate
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
