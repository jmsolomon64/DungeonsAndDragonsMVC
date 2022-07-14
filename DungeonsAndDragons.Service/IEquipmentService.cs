using DungeonsAndDragons.Model.Equipment;

namespace DungeonsAndDragons.Service
{
    public interface IEquipmentService
    {
        bool CreateEquipment(EquipmentCreate model);
        bool DeleteEquipment(int id);
        EquipmentUpdate GenerateUpdateEquipment(int id);
        IEnumerable<EquipmentDetail> GetAllEquipment();
        bool UpdateEquipment(EquipmentUpdate model);
        EquipmentDetail ViewItem(int? id);
    }
}