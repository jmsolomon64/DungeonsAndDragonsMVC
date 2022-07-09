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
    public class ClassesService
    {
        private readonly Guid _id;
        private readonly ApplicationDbContext _ctx;

        public ClassesService(Guid id, ApplicationDbContext ctx)
        {
            _id = id;
            _ctx = ctx;
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

            if(entity.IsActive)
            {
                entity.IsActive = false;
            } else
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
    }
}
