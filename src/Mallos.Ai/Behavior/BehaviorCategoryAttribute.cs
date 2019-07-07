namespace Mallos.Ai.Behavior
{
    using System;

    /// <summary>
    /// An attribute that defines the type of <see cref="BehaviorTreeNode"/>.
    /// This is only used for metadata.
    /// </summary>
    public class BehaviorCategoryAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviorCategoryAttribute"/> class.
        /// </summary>
        /// <param name="category">The behavior category.</param>
        public BehaviorCategoryAttribute(BehaviorCategory category)
        {
            this.Category = category;
        }

        /// <summary>
        /// Gets the defined behavior category.
        /// </summary>
        public BehaviorCategory Category { get; }
    }
}
