namespace Mallos.Ai.Behavior
{
    /// <summary>
    /// Specifies constants that define the bahavior return code.
    /// </summary>
    public enum BehaviorReturnCode
    {
        /// <summary>
        /// Defines that the behavior tree should go to the next node.
        /// </summary>
        Failure,

        /// <summary>
        /// Defines that the behavior tree found what it was looking for.
        /// </summary>
        Success,

        /// <summary>
        /// Defines that the behavior tree stops the executing until it returns something else.
        /// </summary>
        Running,
    }
}
