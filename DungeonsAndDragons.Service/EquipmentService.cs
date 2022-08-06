using DungeonsAndDragons.Data;
using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Equipment;
using Microsoft.EntityFrameworkCore;
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

        //VIew all
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

        //View specific
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

        //POST
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

        //PUT
        public bool UpdateEquipment(EquipmentUpdate model)
        {
            var entity = _ctx.Items.FirstOrDefault(x => x.Id == model.EquipmentId);

            entity.Name = model.Name;
            entity.Weight = model.Weight;
            entity.Cost = model.Cost;
            entity.Description = model.Description;

            return _ctx.SaveChanges() == 1;
        }

        //DELETE
        public bool DeleteEquipment(int id)
        {
            var entity = _ctx.Items.FirstOrDefault(x => x.Id == id);

            _ctx.Items.Remove(entity);
            return _ctx.SaveChanges() == 1;
        }

        //Inventory Management Methods
        //GET all by character
        public List<EquipmentDetail> GetEquipmentByCharacter(int id)
        {
            //Finds character by id, and creats a list of items from Icollection 
            var character = _ctx.Characters
                .Include(x => x.Inventory) //will populate virtual list
                .FirstOrDefault(x => x.Id == id);
            var items = character.Inventory.ToList();

            //Formats items from list of equipment into a list of Equipment details
            var formatItems = new List<EquipmentDetail>();
            foreach (Equipment item in items)
            {
                var formatItem = ViewItem(item.Id);
                formatItems.Add(formatItem);
            }

            return formatItems;
        }

        //PUT Item to character
        public bool AddItemToCharacter(int characterId, int itemId)
        {
            var character = _ctx.Characters.FirstOrDefault(x => x.Id == characterId);
            if (character == null) return false;

            var item = _ctx.Items.FirstOrDefault(x => x.Id == itemId);
            if (item == null) return false;

            character.Inventory.Add(item);
            return _ctx.SaveChanges() == 1;

            return false;
        }

        //Delete Item from inventory
        public bool DeleteItemFromCharacter(int characterId, int itemId)
        {
            var character = _ctx.Characters
                .Include(_x => _x.Inventory)
                .FirstOrDefault(x => x.Id == characterId);
            if (character == null) return false;

            var item = _ctx.Items.FirstOrDefault(x => x.Id == itemId);
            if (item == null) return false;

            character.Inventory.Remove(item);
            return _ctx.SaveChanges() == 1;

            return false;
        }


        //Extra methods
        //Generate update equipment model
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

        public bool SeedEquipment()
        {
            if(_ctx.Items.Count() == 0)
            {
                List<Equipment> equipment = new List<Equipment>();
                equipment.Add(new Equipment() { Name = "Padded Armor", Cost = 5, Weight = 8, Description = "Light Armor" });
                equipment.Add(new Equipment() { Name = "Leather Armor", Cost = 10, Weight = 10, Description = "Light Armor" });
                equipment.Add(new Equipment() { Name = "Chain Shirt", Cost = 50, Weight = 20, Description = "Medium Armor" });
                equipment.Add(new Equipment() { Name = "Scale Mail", Cost = 50, Weight = 45, Description = "Medium Armor" });
                equipment.Add(new Equipment() { Name = "Splint", Cost = 200, Weight = 60, Description = "Heavy Armor" });
                equipment.Add(new Equipment() { Name = "Plate", Cost = 1500, Weight = 65, Description = "Heavy Armor" });
                equipment.Add(new Equipment() { Name = "Shield", Cost = 10, Weight = 6, Description = "Standard Shield" });
                equipment.Add(new Equipment() { Name = "Club", Cost = 1, Weight = 2, Description = "Simple Bludgeoning Weapon" });
                equipment.Add(new Equipment() { Name = "Dagger", Cost = 2, Weight = 1, Description = "Simple Piercing Weapon" });
                equipment.Add(new Equipment() { Name = "Handaxe", Cost = 5, Weight = 2, Description = "Simple Slashing Weapon" });
                equipment.Add(new Equipment() { Name = "Javeline", Cost = 5, Weight = 2, Description = "Simple Piercing Weapon" });
                equipment.Add(new Equipment() { Name = "Quarter Staff", Cost = 2, Weight = 4, Description = "Simple Bludgeoning Weapon" });
                equipment.Add(new Equipment() { Name = "Light Crossbow", Cost = 25, Weight = 5, Description = "Simple Ranged, Piercing Weapon" });
                equipment.Add(new Equipment() { Name = "Dart", Cost = 5, Weight = .25, Description = "Simple Ranged, Piercing Weapon" });
                equipment.Add(new Equipment() { Name = "Shortbow", Cost = 25, Weight = 2, Description = "Simple Ranged, Piercing Weapon" });
                equipment.Add(new Equipment() { Name = "Sling", Cost = 1, Weight = 0, Description = "Simple Ranged, Bludgeoning Weapon" });

                foreach (var item in equipment) _ctx.Items.Add(item);
                _ctx.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
