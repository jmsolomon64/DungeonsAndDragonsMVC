using DungeonsAndDragons.Data;
using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Class;
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
            Classes entity = new Classes()
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
            Classes entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            ClassDetails model = new ClassDetails()
            {
                Name = entity.Name,
                Description = entity.Description
            };

            return model;
        }

        public bool UpdateClass(int id, ClassCreate model)
        {
            Classes entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);
            
            entity.Name = model.Name;
            entity.Description = model.Description;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteClass(int id)
        {
            Classes entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            _ctx.Classes.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }

        //Alternative to delete
        public bool DisableClass(int id)
        {
            Classes entity = _ctx.Classes.FirstOrDefault(x => x.Id == id);

            if(entity.IsActive)
            {
                entity.IsActive = false;
            } else
            {
                entity.IsActive = true;
            }

            return _ctx.SaveChanges() == 1;
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
