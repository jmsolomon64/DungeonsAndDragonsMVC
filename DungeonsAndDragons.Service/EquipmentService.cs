using DungeonsAndDragons.Data;
using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Service
{
    public class EquipmentService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly Guid _userId;
        public EquipmentService(ApplicationDbContext ctx, Guid userId)
        {
            _ctx = ctx;
            _userId = userId;
        }

        //CRUD Services
        public IEnumerable<EquipmentDetail> GetAllEquipment()
        {
            var query = _ctx.Items.Select(x => new EquipmentDetail
            {
                EquipmentId = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                Cost = x.Cost,
                Description = x.Description
            });

            return query.ToArray();
        }

        //I want to add a method that pulls all weapons in a  players inventory

        public bool CreateEquipment(EquipmentCreate model)
        {
            var entity = new Equipment()
            {
                Name = model.Name,
                Weight = model.Weight,
                Cost = model.Cost,
                Description = model.Description
            };

            _ctx.Items.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public bool UpdateEquipment(EquipmentUpdate model)
        {
            var entity = _ctx.Items.FirstOrDefault(x => x.Id == model.EquipmentId);

            entity.Name = model.Name;
            entity.Weight = model.Weight;
            entity.Cost = model.Cost;
            entity.Description = model.Description;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteEquipment(int id)
        {
            var entity = _ctx.Items.FirstOrDefault(x => x.Id == id);

            _ctx.Items.Remove(entity);
            return _ctx.SaveChanges() == 1;
        }
    }
}
