using DungeonsAndDragons.Data;
using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Service
{
    public class RaceService : IRaceService
    {
        private Guid _userId;
        private readonly ApplicationDbContext _ctx;

        public RaceService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool SetUserId(Guid userId)
        {
            if (userId == null) return false;

            _userId = userId;
            return true;
        }

        public IEnumerable<RaceDetails> GetRaces()
        {
            var query = _ctx.Races.Select(x => new RaceDetails
            {
                RaceId = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
            });

            return query.ToArray();
        }

        public RaceDetails ViewRace(int id)
        {
            var entity = FindRaceById(id);
            var model = new RaceDetails()
            {
                RaceId = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
            };

            return model;
        }

        public bool CreateRace(RaceCreate model)
        {
            var entity = new Race()
            {
                Name = model.Name,
                Description = model.Description,
                IsActive = true
            };

            _ctx.Races.Add(entity);

            return _ctx.SaveChanges() == 1;
        }

        public bool EditRace(int id, RaceEdit model)
        {
            var entity = FindRaceById(id);

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.IsActive = model.IsActive;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteRace(int id)
        {
            var entity = FindRaceById(id);

            _ctx.Races.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }


        public RaceEdit GenerateRaceEdit(int id)
        {
            var entity = FindRaceById(id);
            var model = new RaceEdit()
            {
                RaceId = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return model;
        }

        public Race FindRaceById(int id)
        {
            var entity = _ctx.Races.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public bool SeedRaces()
        {
            if (_ctx.Races.Count() == 0)
            {
                List<Race> races = new List<Race>();
                races.Add(new Race() { Name = "Draganborn", Description = "Humanoid Dragons", IsActive = true });
                races.Add(new Race() { Name = "Dwarf", Description = "Short Humans", IsActive = true });
                races.Add(new Race() { Name = "Elf", Description = "Stuck up, pointy eared humans", IsActive = true });
                races.Add(new Race() { Name = "Gnome", Description = "Dwarfs with spice", IsActive = true });
                races.Add(new Race() { Name = "Half-Elf", Description = "Slightly less stuck up humans", IsActive = true });
                races.Add(new Race() { Name = "Halfling", Description = "I have no idea what this is", IsActive = true });
                races.Add(new Race() { Name = "Half-Orc", Description = "Strong Humans", IsActive = true });
                races.Add(new Race() { Name = "Human", Description = "Literally a human", IsActive = true });
                races.Add(new Race() { Name = "Tiefling", Description = "Demonic Human?", IsActive = true });

                foreach (var item in races) _ctx.Races.Add(item);

                _ctx.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
