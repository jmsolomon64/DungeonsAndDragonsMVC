using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Model.Character
{
    public class CharacterDetailView
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Consitution { get; set; }
        public int Inteligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public string Description { get; set; }
    }
}
