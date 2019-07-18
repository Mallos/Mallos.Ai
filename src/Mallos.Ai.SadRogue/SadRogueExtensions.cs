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
            return new RadiusAreaProvider(centerPosition, radius, map.DistanceMeasurement)
                .CalculatePositions()
                .SelectMany(e => map.GetEntities<TEntityType>(e))
                // TODO: Waiting on GoRouge solution
                // .Where(entity => map.FOV.CanSee(centerPosition, entity.Position))
                .ToList();
        }

        public static void ToggleOnOff(this FOVVisibilityHandler visibilityHandler)
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
