namespace Mallos.Ai.Behavior.Task
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GoRogue.GameFramework;
    using SadConsole;

    /// <summary>
    /// A node that checks if we spot another <see cref="BasicEntity"/>.
    /// </summary>
    public class SpotEntityNode : SpotEntityNode<BasicEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotEntityNode"/> class.
        /// </summary>
        /// <param name="radiusFunc">A function that returns the entities fov radius.</param>
        /// <param name="evaluator">A function that evaluates if the entity should be counted.</param>
        /// <param name="spottedKey">Blackboard Property key for storing if we spotted something.</param>
        /// <param name="spottedCoordKey">Blackboard Property key for storing where we spotted something.</param>
        public SpotEntityNode(
            Func<BasicEntity, int> radiusFunc,
            Func<BasicEntity, bool> evaluator,
            string spottedKey = null,
            string spottedCoordKey = null)
            : base(radiusFunc, evaluator, spottedKey, spottedCoordKey)
        {
        }
    }

    /// <summary>
    /// A node that checks if we spot another entity type.
    /// </summary>
    /// <typeparam name="TEntityType">The entity type we are spotting.</typeparam>
    public class SpotEntityNode<TEntityType> : BehaviorTreeNode
        where TEntityType : IGameObject
    {
        private readonly Func<BasicEntity, int> radiusFunc;
        private readonly Func<TEntityType, bool> evaluator;
        private readonly string spottedKey;
        private readonly string spottedCoordKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotEntityNode{TEntityType}"/> class.
        /// </summary>
        /// <param name="radiusFunc">A function that returns the entities fov radius.</param>
        /// <param name="evaluator">A function that evaluates if the entity should be counted.</param>
        /// <param name="spottedKey">Blackboard Property key for storing if we spotted something.</param>
        /// <param name="spottedCoordKey">Blackboard Property key for storing where we spotted something.</param>
        public SpotEntityNode(
            Func<BasicEntity, int> radiusFunc,
            Func<TEntityType, bool> evaluator,
            string spottedKey = null,
            string spottedCoordKey = null)
        {
            this.radiusFunc = radiusFunc ?? throw new ArgumentNullException(nameof(radiusFunc));
            this.evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
            this.spottedKey = spottedKey;
            this.spottedCoordKey = spottedCoordKey;
        }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard is RogueBlackboard rb)
            {
                var radius = this.radiusFunc(rb.Entity);
                var entities = GetEntities(rb, radius);

                if (entities.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(spottedCoordKey))
                    {
                        // FIXME: Maybe there should be a function for picking if there are multiple ones.
                        //        Maybe we even want to pass the entity to the blackboard too.


                        blackboard.Properties[spottedCoordKey] = entities[0].Position;
                    }

                    if (!string.IsNullOrWhiteSpace(spottedKey))
                    {
                        blackboard.Properties[spottedKey] = true;
                    }

                    return BehaviorReturnCode.Success;
                }
            }

            if (!string.IsNullOrWhiteSpace(spottedKey))
            {
                blackboard.Properties[spottedKey] = false;
            }

            return BehaviorReturnCode.Failure;
        }

        private IList<TEntityType> GetEntities(RogueBlackboard rb, int radius)
        {
            // FIXME: Check POV


            return rb.Map
                .EntitiesInArea<TEntityType>(rb.Entity.Position, radius)
                .Where(this.evaluator) // FIXME: Do we have to send the evaluator if we know the type?
                .ToList();
        }
    }
}
