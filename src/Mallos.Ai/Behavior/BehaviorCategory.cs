namespace Mallos.Ai.Behavior
{
    /// <summary>
    /// Specifies constants that define the bahavior categories.
    /// </summary>
    public enum BehaviorCategory
    {
        /// <summary>
        /// These are the nodes that define the root of a branch and the base rules for how that branch is executed.
        /// </summary>
        Composite,

        /// <summary>
        /// These are the leaves of the Behavior Tree, the nodes that "do" things and don't have an output connection.
        /// </summary>
        Task,

        /// <summary>
        /// Also known as conditionals. These attach to another node and make decisions on whether or not a branch in the tree, or even a single node, can be executed.
        /// </summary>
        Decorator,
    }
}
