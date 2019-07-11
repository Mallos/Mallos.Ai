namespace Mallos.Ai
{
    using System.Collections.Generic;
    using System.Linq;

    using GoRogue;
    using GoRogue.GameFramework;
    using GoRogue.MapViews;

    public static class SadRogueExtensions
    {
        public static Coord FindClosePoint(this Map map, Coord centerPosition, int radius = 1)
        {
            return new RadiusAreaProvider(centerPosition, radius, map.DistanceMeasurement)
                .CalculatePositions()
                .ToList()
                .RandomItem(pos => map.WalkabilityView.Contains(pos) && map.WalkabilityView[pos]);
        }

        public static IList<TEntityType> EntitiesInArea<TEntityType>(this Map map, Coord centerPosition, int radius)
             where TEntityType : IGameObject
        {
            return new RadiusAreaProvider(centerPosition, radius, map.DistanceMeasurement)
                .CalculatePositions()
                .SelectMany(e => map.GetEntities<TEntityType>(e))
                .ToList();
        }
    }
}
