using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Model.Equipment
{
    public class EquipmentDetail
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
    }
}
