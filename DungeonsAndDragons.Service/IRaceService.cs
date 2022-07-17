using DungeonsAndDragons.Data.Entity;
using DungeonsAndDragons.Model.Race;

namespace DungeonsAndDragons.Service
{
    public interface IRaceService
    {
        bool CreateRace(RaceCreate model);
        bool DeleteRace(int id);
        bool EditRace(int id, RaceEdit model);
        Race FindRaceById(int id);
        RaceEdit GenerateRaceEdit(int id);
        IEnumerable<RaceDetails> GetRaces();
        bool SetUserId(Guid userId);
        RaceDetails ViewRace(int id);
    }
}