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
