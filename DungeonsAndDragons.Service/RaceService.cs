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
    public class RaceService
    {
        private Guid _userId;
        private readonly ApplicationDbContext _ctx;

        public RaceService(Guid userId, ApplicationDbContext ctx)
        {
            _userId = userId;
            _ctx = ctx;
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
                Description= model.Description,
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
    }
}
