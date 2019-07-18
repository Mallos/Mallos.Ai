namespace Mallos.Ai
{
    public static class BlackboardExtenions
    {
        public static T GetProperty<T>(this Blackboard blackboard, string name)
        {
            return (T)blackboard.Properties[name];
        }

        public static bool HasProperty<T>(this Blackboard blackboard, string name)
        {
            return blackboard.Properties.ContainsKey(name) && blackboard.Properties[name] is T;
        }
    }
}
