using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Service
{
    public interface ICharacterService
    {
        bool SetUserId(Guid userId);
        IEnumerable<CharacterQuickView> GetCharacters();
        bool CreateCharacter(CharacterCreate model);
        CharacterEdit CharacterEditGenerator(int id);
        CharacterDetailView FindCharacterById(int? id);
        Race FindRaceById(int id);
        bool UpdateCharacter(CharacterEdit model);
        bool DeleteCharacter(int id);
        List<Classes> ClassesList();
        List<Race> RacesList();
    }
}
