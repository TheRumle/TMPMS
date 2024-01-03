using TMPMS.Common.JsonTofms.Models;

namespace TMPMS.Common.JsonTofms.ConsistencyCheck.Validators;


public record MoveActionStructureValidationContext(IEnumerable<LocationDefinition> LocationStructures,
    IEnumerable<string> parts);