using TMPMS.Common.Move;

namespace TMPMS.Common.JsonTofms.Models;

public interface ITofmsFactory
{
    public IReadOnlyCollection<MoveAction> CreateMoveActions(TofmJsonSystem tofmJsonSystem);
}