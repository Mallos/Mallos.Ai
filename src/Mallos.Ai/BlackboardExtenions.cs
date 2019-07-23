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

        public static int Increment(this Blackboard blackboard, string name, int amount = 1)
        {
            if (!blackboard.Properties.ContainsKey(name))
            {
                blackboard.Properties[name] = amount;
                return amount;
            }

            var newNumber = (int)blackboard.Properties[name] + amount;
            blackboard.Properties[name] = newNumber;
            return newNumber;
        }

        public static float Increment(this Blackboard blackboard, string name, float amount = 1.0f)
        {
            if (!blackboard.Properties.ContainsKey(name))
            {
                blackboard.Properties[name] = amount;
                return amount;
            }

            var newNumber = (float)blackboard.Properties[name] + amount;
            blackboard.Properties[name] = newNumber;
            return newNumber;
        }
    }
}
