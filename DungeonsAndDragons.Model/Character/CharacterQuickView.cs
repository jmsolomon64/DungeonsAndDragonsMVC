using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Model.Character
{
    public class CharacterQuickView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RaceId { get; set; }
        public int ClassId { get; set; }
        public int Level { get; set; }
    }
}
