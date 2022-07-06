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
                    Race = x.Race.Name,
                    Class = x.Class.Name,
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

        public CharacterEdit CharacterEditGenerator(int id)
        {
            var entity = _ctx.Characters.FirstOrDefault(x => x.Id == id);
            var model = new CharacterEdit
            {
                CharacterId = id,
                Name = entity.Name,
                Level = entity.Level,
                Strength = entity.Strength,
                Dexterity = entity.Dexterity,
                Consitution = entity.Consitution,
                Inteligence = entity.Inteligence,
                Wisdom = entity.Wisdom,
                Charisma = entity.Charisma,
                Description = entity.Description
            };

            return model;
        }

        public CharacterDetailView FindCharacterById(int? id)
        {
            if(id == null) return null;

            Character entity = _ctx.Characters.FirstOrDefault(x => x.Id == id);

            if (entity == null) return null;


            return new CharacterDetailView
            {
                CharacterId = entity.Id,
                Name = entity.Name,
                Race = FindRaceById(entity.RaceId).Name,
                Class = FindClassById(entity.ClassId).Name,
                Level = entity.Level,
                Strength = entity.Strength,
                Dexterity = entity.Dexterity,
                Consitution = entity.Consitution,
                Inteligence = entity.Inteligence,
                Wisdom = entity.Wisdom,
                Charisma = entity.Charisma,
                Description = entity.Description
            };
        }

        public Race FindRaceById(int id)
        {
            Race entity = _ctx.Races.FirstOrDefault(x => x.Id == id);

            if (entity == null) return null;

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

        public bool UpdateCharacter(CharacterEdit model)
        {
            var entity = _ctx.Characters.FirstOrDefault(x => x.Id == model.CharacterId); //Steps through context to the character by id

            entity.Name = model.Name;
            entity.Level = model.Level;
            entity.Strength = model.Strength;
            entity.Dexterity = model.Dexterity;
            entity.Consitution = model.Consitution;
            entity.Inteligence = model.Inteligence;
            entity.Wisdom = model.Wisdom;
            entity.Charisma = model.Charisma;
            entity.Description = model.Description;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteCharacter(int id)
        {
            var entity = _ctx.Characters.FirstOrDefault(x => x.Id == id);

            _ctx.Characters.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }
    }
}
