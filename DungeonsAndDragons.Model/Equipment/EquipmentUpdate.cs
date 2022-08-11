using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Model.Equipment
{
    public class EquipmentUpdate
    {
        public int EquipmentId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
