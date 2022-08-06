using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Class;
using DungeonsAndDragons.Model.Classes;

namespace DungeonsAndDragons.Service
{
    public interface IClassesService
    {
        bool SetUserId(Guid userId);
        bool CreateClasses(ClassCreate model);
        bool DeleteClass(int id);
        bool DisableClass(int id);
        Classes FindClassById(int id);
        ClassUpdate GenerateClassUpdate(int id);
        IEnumerable<ClassDetails> GetClasses();
        bool UpdateClass(int id, ClassUpdate model);
        ClassDetails ViewClass(int id);
        bool SeedClasses();
    }
}