using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsAndDragons.Data;
using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Character;

namespace DungeonsAndDragons.Service
{
    public class CharacterService
    {
        //dependencies
        private Guid _userId;
        private readonly ApplicationDbContext _ctx;

        public CharacterService(Guid userId, ApplicationDbContext context)
        {
            _ctx = context;
            _userId = userId;
        }

        public bool SetUserId(Guid userId)
        {
            if (userId == null)
            {
                return false;
            }

            _userId = userId;
            return true;
        }

        public IEnumerable<CharacterQuickView> GetCharacters()
        {
            var query = _ctx.Characters.Where(x => x.OwnerId == _userId)
                .Select(x => new CharacterQuickView
                {
                    Id = x.Id,
                    Name = x.Name,
                    RaceId = x.RaceId,
                    ClassId = x.ClassId,
                    Level = x.Level
                });

            return query.ToArray();
        }

        public bool CreateCharacter(CharacterCreate model)
        {
            var entity = new Character()
            {
                OwnerId = _userId,
                Name = model.Name,
                RaceId = model.RaceId,
                Race = FindRaceById(model.RaceId),
                ClassId = model.ClassId,
                Class = FindClassById(model.ClassId),
                Level = model.Level,
                Strength = model.Strength,
                Dexterity = model.Dexterity,
                Consitution = model.Consitution,
                Inteligence = model.Inteligence,
                Wisdom = model.Wisdom,
                Charisma = model.Charisma,
                Description = model.Description,
            };

            _ctx.Characters.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public Race FindRaceById(int id)
        {
            Race entity = _ctx.Races.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return entity;            
        }

        public Classes FindClassById(int id)
        {
            Classes entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }
    }
}
