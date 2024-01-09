using Models;

namespace DatabaseProj.Services;

public interface IFightLogicService
{
    public List<Round>? ProcessGame(Fight fight);
}