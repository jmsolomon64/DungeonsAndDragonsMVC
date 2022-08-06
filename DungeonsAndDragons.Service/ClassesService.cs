using DungeonsAndDragons.Data;
using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Class;
using DungeonsAndDragons.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Service
{
    public class ClassesService : IClassesService
    {
        private Guid _id;
        private readonly ApplicationDbContext _ctx;

        public ClassesService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool SetUserId(Guid userId)
        {
            if (userId == null) return false;

            _id = userId;
            return true;
        }

        public bool CreateClasses(ClassCreate model)
        {
            var entity = new Classes()
            {
                Name = model.Name,
                Description = model.Description,
                IsActive = true
            };

            _ctx.Classes.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public ClassDetails ViewClass(int id)
        {
            var entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            ClassDetails model = new ClassDetails()
            {
                ClassId = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return model;
        }

        public bool UpdateClass(int id, ClassUpdate model)
        {
            var entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            entity.Name = model.Name;
            entity.Description = model.Description;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteClass(int id)
        {
            var entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            _ctx.Classes.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }

        //Alternative to delete
        public bool DisableClass(int id)
        {
            var entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            if (entity.IsActive)
            {
                entity.IsActive = false;
            }
            else
            {
                entity.IsActive = true;
            }

            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<ClassDetails> GetClasses()
        {
            //I want all Classes to be pulled from DB
            var query = _ctx.Classes
                .Select(x => new ClassDetails
                {
                    ClassId = x.Id,
                    Name = x.Name,
                    Description = x.Description
                });

            return query.ToArray();
        }

        public ClassUpdate GenerateClassUpdate(int id)
        {
            var entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            var model = new ClassUpdate()
            {
                ClassId = id,
                Name = entity.Name,
                Description = entity.Description
            };

            return model;
        }

        public Classes FindClassById(int id)
        {
            var entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public bool SeedClasses()
        {
            if (_ctx.Classes.Count() == 0)
            {
                List<Classes> classes = new List<Classes>();
                classes.Add(new Classes() { Name = "Barbarian", Description = "Fierce primitive warrior", IsActive = true });
                classes.Add(new Classes() { Name = "Bard", Description = "Musical Magician", IsActive = true });
                classes.Add(new Classes() { Name = "Cleric", Description = "Priestly warrior with divine magic", IsActive = true });
                classes.Add(new Classes() { Name = "Druid", Description = "Priests of the old faith, in tune with nature", IsActive = true });
                classes.Add(new Classes() { Name = "Fighter", Description = "Master of martial combat", IsActive = true });
                classes.Add(new Classes() { Name = "Monk", Description = "Master of martial arts", IsActive = true });
                classes.Add(new Classes() { Name = "Paladin", Description = "Holy warrior bound by an oath", IsActive = true });
                classes.Add(new Classes() { Name = "Ranger", Description = "Warrior who combats threats on edge of civilization", IsActive = true });
                classes.Add(new Classes() { Name = "Sorcerer", Description = "Naturally powerful spellcaster", IsActive = true });
                classes.Add(new Classes() { Name = "Warlock", Description = "Magic user who's power derives from an extraplanar entity", IsActive = true });
                classes.Add(new Classes() { Name = "Wizard", Description = "Scholarly magic user", IsActive = true });

                foreach (var item in classes) _ctx.Classes.Add(item);

                _ctx.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
