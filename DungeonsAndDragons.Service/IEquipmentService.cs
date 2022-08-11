using DungeonsAndDragons.Model.Equipment;

namespace DungeonsAndDragons.Service
{
    public interface IEquipmentService
    {
        bool SetUserId(Guid userId);
        bool CreateEquipment(EquipmentCreate model);
        bool DeleteEquipment(int id);
        EquipmentUpdate GenerateUpdateEquipment(int id);
        IEnumerable<EquipmentDetail> GetAllEquipment();
        List<EquipmentDetail> GetAllItems();
        bool UpdateEquipment(EquipmentUpdate model);
        EquipmentDetail ViewItem(int? id);
        List<EquipmentDetail> GetEquipmentByCharacter(int id);
        bool AddItemToCharacter(int characterId, int itemId);
        bool DeleteItemFromCharacter(int characterId, int itemId);
        bool SeedEquipment();
    }
}