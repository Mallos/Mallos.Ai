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
    [BehaviorCategory(BehaviorCategory.Task)]
    public class EnvironmentQueryNode : EnvironmentQueryNode<BasicEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentQueryNode"/> class.
        /// </summary>
        /// <param name="radiusFunc">A function that returns the entities fov radius.</param>
        /// <param name="evaluator">A function that evaluates if the entity should be counted.</param>
        /// <param name="selector">A function that evaluates all the found entities in the radius.</param>
        /// <param name="fieldOfView">Whether it should only count entities that are visible.</param>
        /// <param name="spottedKey">Blackboard Property key for storing if we spotted something.</param>
        /// <param name="spottedCoordKey">Blackboard Property key for storing where we spotted something.</param>
        /// <param name="failureCode">The code that will return if failed.</param>
        public EnvironmentQueryNode(
            Func<BasicEntity, int> radiusFunc,
            Func<BasicEntity, bool> evaluator = null,
            Func<List<BasicEntity>, List<BasicEntity>> selector = null,
            bool fieldOfView = true,
            string spottedKey = null,
            string spottedCoordKey = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Running)
            : base(radiusFunc, evaluator, selector, fieldOfView, spottedKey, spottedCoordKey, failureCode)
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
    [BehaviorCategory(BehaviorCategory.Task)]
    public class EnvironmentQueryNode<TEntityType> : BehaviorTreeNode
        where TEntityType : IGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentQueryNode{TEntityType}"/> class.
        /// </summary>
        /// <param name="radiusFunc">A function that returns the entities fov radius.</param>
        /// <param name="evaluator">A function that evaluates if the entity should be counted.</param>
        /// <param name="selector">A function that evaluates all the found entities in the radius.</param>
        /// <param name="fieldOfView">Whether it should only count entities that are visible.</param>
        /// <param name="spottedKey">Blackboard Property key for storing if we spotted something.</param>
        /// <param name="spottedCoordKey">Blackboard Property key for storing where we spotted something.</param>
        /// <param name="failureCode">The code that will return if failed.</param>
        public EnvironmentQueryNode(
            Func<BasicEntity, int> radiusFunc,
            Func<TEntityType, bool> evaluator = null,
            Func<List<TEntityType>, List<TEntityType>> selector = null,
            bool fieldOfView = true,
            string spottedKey = null,
            string spottedCoordKey = null,
            BehaviorReturnCode failureCode = BehaviorReturnCode.Running)
        {
            this.RadiusFunction = radiusFunc ?? throw new ArgumentNullException(nameof(radiusFunc));
            this.Evaluator = evaluator;
            this.Selector = selector;
            this.FieldOfView = fieldOfView;
            this.SpottedKey = spottedKey;
            this.SpottedCoordKey = spottedCoordKey;
            this.FailureCode = failureCode;
        }

        /// <summary>
        /// Gets the function that returns the entities fov radius.
        /// </summary>
        public Func<BasicEntity, int> RadiusFunction { get; }

        /// <summary>
        /// Gets the function that evaluates if the entity should be counted.
        /// </summary>
        public Func<TEntityType, bool> Evaluator { get; }

        /// <summary>
        /// Gets the function that evaluates all the found entities in the radius.
        /// </summary>
        public Func<List<TEntityType>, List<TEntityType>> Selector { get; }

        /// <summary>
        /// Gets whether it should only count entities that are visible.
        /// </summary>
        public bool FieldOfView { get; }

        /// <summary>
        /// Gets the blackboard Property key for storing if we spotted something.
        /// </summary>
        public string SpottedKey { get; }

        /// <summary>
        /// Gets the blackboard Property key for storing where we spotted something.
        /// </summary>
        public string SpottedCoordKey { get; }

        /// <summary>
        /// Gets the code that will return if failed.
        /// </summary>
        public BehaviorReturnCode FailureCode { get; }

        /// <inheritdoc />
        protected override BehaviorReturnCode Behave(Blackboard blackboard)
        {
            if (blackboard is RogueBlackboard rb)
            {
                var radius = this.RadiusFunction(rb.Entity);
                var entities = GetEntities(rb, radius);

                if (this.Selector != null)
                {
                    entities = this.Selector(entities);
                }

                if (entities.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(this.SpottedCoordKey))
                    {
                        // TODO: Do we want to select them based on distance?
                        blackboard.Properties[this.SpottedCoordKey] = entities[0].Position;
                    }

                    if (!string.IsNullOrWhiteSpace(this.SpottedKey))
                    {
                        blackboard.Properties[this.SpottedKey] = true;
                    }

                    return BehaviorReturnCode.Success;
                }
            }

            if (!string.IsNullOrWhiteSpace(this.SpottedKey))
            {
                blackboard.Properties[this.SpottedKey] = false;
            }

            return this.FailureCode;
        }

        private List<TEntityType> GetEntities(RogueBlackboard rb, int radius)
        {
            var entities = rb.Map.EntitiesInArea<TEntityType>(rb.Entity.Position, radius, this.FieldOfView);

            if (this.Evaluator != null)
            {
                return entities.Where(this.Evaluator).ToList();
            }
            else
            {
                return entities.ToList();
            }
        }
    }
}
