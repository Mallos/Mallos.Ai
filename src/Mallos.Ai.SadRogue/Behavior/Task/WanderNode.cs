namespace Mallos.Ai.Behavior.Task
{
    using GoRogue;
    using GoRogue.GameFramework;
    using GoRogue.MapViews;
    using System.Linq;

    public class WanderNode : BehaviorTreeNode
    {
        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard is RogueBlackboard rb)
            {
                var walkTo = (Coord)blackboard.Properties["WalkTo"];
                if (walkTo == rb.Entity.Position)
                {
                    return BehaviorReturnCode.Success;
                }

                var closePoint = FindClosePoint(rb.Map, rb.Entity.Position);
                if (closePoint != rb.Entity.Position)
                {
                    rb.Entity.Position = closePoint;
                    return BehaviorReturnCode.Success;
                }
            }

            return BehaviorReturnCode.Failure;
        }

        private Coord FindClosePoint(Map map, Coord start)
        {
            return new RadiusAreaProvider(start, 1, map.DistanceMeasurement)
                .CalculatePositions()
                .ToList()
                .RandomItem(pos => map.WalkabilityView.Contains(pos) && map.WalkabilityView[pos]);
        }
    }
}
