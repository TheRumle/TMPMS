﻿using System.Collections.ObjectModel;
using TMPMS.Common.JsonTofms.Models;
using TMPMS.Common.Move;

namespace TMPMS.Common.Factories;

public class MoveActionFactory : ITofmsFactory
{
    public IReadOnlyCollection<MoveAction> CreateMoveActions(TofmJsonSystem tofmJsonSystem)
    {
        var locationStructures = tofmJsonSystem.Components.SelectMany(e => e.Locations).ToArray();
        var locations = CreateLocations(locationStructures);
        return CreateMoveAction(tofmJsonSystem.Components.SelectMany(e => e.Moves), locations);
    }

    private ReadOnlyCollection<MoveAction> CreateMoveAction(IEnumerable<MoveActionDefinition> moveActionStructures,
        Location[] locations)
    {
        var kvs = locations.Select(e => new KeyValuePair<string, Location>(e.Name, e));
        var locationByName = new Dictionary<string, Location>(kvs);

        return moveActionStructures.Select(structure =>
        {
            var emptyBef = structure.EmptyBefore.Select(e => locationByName[e]);
            var emptyAf = structure.EmptyAfter.Select(e => locationByName[e]);
            var parts = structure.Parts.Select(e => new KeyValuePair<string, int>(e.PartType, e.Amount));
            return CreateMoveAction(emptyBef, emptyAf, parts, structure, locationByName);
        }).ToList().AsReadOnly();
    }

    private MoveAction CreateMoveAction(IEnumerable<Location> emptyBef, IEnumerable<Location> emptyAf,
        IEnumerable<KeyValuePair<string, int>> parts, MoveActionDefinition definition,
        Dictionary<string, Location> locationByName)
    {
        var move = new MoveAction
        {
            EmptyBefore = new HashSet<Location>(emptyBef),
            EmptyAfter = new HashSet<Location>(emptyAf),
            From = locationByName[definition.From],
            To = locationByName[definition.To],
            PartsToMove = new HashSet<KeyValuePair<string, int>>(parts),
            Name = definition.Name
        };
        return move;
    }

    private static Location[] CreateLocations(LocationDefinition[] locations)
    {
        return locations.Select(locationStructure =>
            new Location(
                locationStructure.Name,
                locationStructure.Capacity,
                locationStructure.Invariants.Select(e => new Invariant(e.Part, e.Min, e.Max)),
                locationStructure.IsProcessing
            )
        ).ToArray();
    }
}