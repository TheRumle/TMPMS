using TMPMS.Common.JsonTofms.Models;

namespace TMPMS.Common.JsonTofms.ConsistencyCheck.Validators;

public interface INamingValidator : IValidator<IEnumerable<LocationDefinition>, IEnumerable<MoveActionDefinition>>
{
}