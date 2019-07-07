namespace Mallos.Ai
{
    using System.Linq;

    using GoRogue;
    using GoRogue.GameFramework;
    using GoRogue.MapViews;

    public static class SadRogueExtensions
    {
        public static Coord FindClosePoint(this Map map, Coord startPosition, int radius = 1)
        {
            return new RadiusAreaProvider(startPosition, radius, map.DistanceMeasurement)
                .CalculatePositions()
                .ToList()
                .RandomItem(pos => map.WalkabilityView.Contains(pos) && map.WalkabilityView[pos]);
        }
    }
}
