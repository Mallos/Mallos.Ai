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
        /// <param name="selector">A function that evaluates all the found entities in the radius.</param>
        /// <param name="fieldOfView">Whether it should only count entities that are visible.</param>
        /// <param name="spottedKey">Blackboard Property key for storing if we spotted something.</param>
        /// <param name="spottedCoordKey">Blackboard Property key for storing where we spotted something.</param>
        public SpotEntityNode(
            Func<BasicEntity, int> radiusFunc,
            Func<BasicEntity, bool> evaluator = null,
            Func<List<BasicEntity>, List<BasicEntity>> selector = null,
            bool fieldOfView = true,
            string spottedKey = null,
            string spottedCoordKey = null)
            : base(radiusFunc, evaluator, selector, fieldOfView, spottedKey, spottedCoordKey)
        {
        }
    }

    /// <summary>
    /// A node that checks if we spot another entity type.
    /// </summary>
    /// <typeparam name="TEntityType">The entity type we are spotting.</typeparam>
    /// <remarks>
    /// Are there more then one entity in the list then the first one will be marked as spotted.
    /// </remarks>
    public class SpotEntityNode<TEntityType> : BehaviorTreeNode
        where TEntityType : IGameObject
    {
        private readonly Func<BasicEntity, int> radiusFunc;
        private readonly Func<TEntityType, bool> evaluator = null;
        private readonly Func<List<TEntityType>, List<TEntityType>> selector = null;
        private readonly bool fieldOfView;
        private readonly string spottedKey;
        private readonly string spottedCoordKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotEntityNode{TEntityType}"/> class.
        /// </summary>
        /// <param name="radiusFunc">A function that returns the entities fov radius.</param>
        /// <param name="evaluator">A function that evaluates if the entity should be counted.</param>
        /// <param name="selector">A function that evaluates all the found entities in the radius.</param>
        /// <param name="fieldOfView">Whether it should only count entities that are visible.</param>
        /// <param name="spottedKey">Blackboard Property key for storing if we spotted something.</param>
        /// <param name="spottedCoordKey">Blackboard Property key for storing where we spotted something.</param>
        public SpotEntityNode(
            Func<BasicEntity, int> radiusFunc,
            Func<TEntityType, bool> evaluator = null,
            Func<List<TEntityType>, List<TEntityType>> selector = null,
            bool fieldOfView = true,
            string spottedKey = null,
            string spottedCoordKey = null)
        {
            this.radiusFunc = radiusFunc ?? throw new ArgumentNullException(nameof(radiusFunc));
            this.evaluator = evaluator;
            this.selector = selector;
            this.fieldOfView = fieldOfView;
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

                if (this.selector != null)
                {
                    entities = this.selector(entities);
                }

                if (entities.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(spottedCoordKey))
                    {
                        // TODO: Do we want to select them based on distance?
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

        private List<TEntityType> GetEntities(RogueBlackboard rb, int radius)
        {
            var entities = rb.Map.EntitiesInArea<TEntityType>(rb.Entity.Position, radius, fieldOfView);

            if (this.evaluator != null)
            {
                return entities.Where(this.evaluator).ToList();
            }
            else
            {
                return entities.ToList();
            }
        }
    }
}
