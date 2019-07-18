namespace Mallos.Ai
{
    using System.Collections.Generic;
    using System.Linq;

    using GoRogue;
    using GoRogue.GameFramework;
    using GoRogue.MapViews;
    using SadConsole;

    public static class SadRogueExtensions
    {
        public static Coord FindClosePoint(this Map map, Coord centerPosition, int radius = 1)
        {
            return new RadiusAreaProvider(centerPosition, radius, map.DistanceMeasurement)
                .CalculatePositions()
                .ToList()
                .RandomItem(pos => map.WalkabilityView.Contains(pos) && map.WalkabilityView[pos]);
        }

        public static IList<TEntityType> EntitiesInArea<TEntityType>(this Map map, Coord centerPosition, int radius, bool fieldOfView)
             where TEntityType : IGameObject
        {
            var query = new RadiusAreaProvider(centerPosition, radius, map.DistanceMeasurement)
                .CalculatePositions()
                .SelectMany(e => map.GetEntities<TEntityType>(e));

            if (fieldOfView)
            {
                return query.Where(e => CanSee(map, centerPosition, e.Position)).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public static bool CanSee(this Map map, Coord position1, Coord position2)
        {
            // OPTIMIZE: This could be optimized.
            var points = GoRogue.Lines.Get(position1, position2);
            foreach (var point in points)
            {
                if (!map.WalkabilityView.Contains(point.X, point.Y))
                {
                    return false;
                }
            }

            return true;
        }

        public static void ToggleState(this FOVVisibilityHandler visibilityHandler)
        {
            if (visibilityHandler.Enabled)
            {
                visibilityHandler.Disable();
            }
            else
            {
                visibilityHandler.Enable();
            }
        }
    }
}
