using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Model.Race
{
    public class RaceDetails
    {
        public int RaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name="Active")]
        public bool IsActive { get; set; }
    }
}
