using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Model.Race
{
    public class RaceEdit
    {
        public int RaceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
