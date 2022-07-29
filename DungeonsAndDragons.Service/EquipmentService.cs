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
    public class EquipmentService : IEquipmentService
    {
        private readonly ApplicationDbContext _ctx;
        private Guid _userId;
        public EquipmentService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool SetUserId(Guid userId)
        {
            if (userId == null) return false;

            _userId = userId;
            return true;
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

        public List<EquipmentDetail> GetAllItems()
        {
            var query = _ctx.Items.Select(x => new EquipmentDetail
            {
                EquipmentId = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                Cost = x.Cost,
                Description = x.Description
            });

            return query.ToList();
        }

        public EquipmentDetail ViewItem(int? id)
        {
            var entity = _ctx.Items.FirstOrDefault(x => x.Id == id);

            EquipmentDetail model = new EquipmentDetail
            {
                EquipmentId = entity.Id,
                Name = entity.Name,
                Weight = entity.Weight,
                Cost = entity.Cost,
                Description = entity.Description
            };

            return model;
        }

        public List<EquipmentDetail> GetEquipmentByCharacter(int id)
        {
            //Finds character by id, and creats a list of items from Icollection 
            var character = _ctx.Characters.FirstOrDefault(x => x.Id == id);
            var items = character.Inventory.ToList();

            //Formats items from list of equipment into a list of Equipment details
            var formatItems = new List<EquipmentDetail>();
            foreach(Equipment item in items)
            {
                var formatItem = ViewItem(item.Id);
                formatItems.Add(formatItem);
            }

            return formatItems;
        }

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

        //Extra methods
        public EquipmentUpdate GenerateUpdateEquipment(int id)
        {
            var entity = _ctx.Items.FirstOrDefault(x => x.Id == id);
            var model = new EquipmentUpdate
            {
                EquipmentId = id,
                Name = entity.Name,
                Weight = entity.Weight,
                Cost = entity.Cost,
                Description = entity.Description
            };

            return model;
        }
    }
}
