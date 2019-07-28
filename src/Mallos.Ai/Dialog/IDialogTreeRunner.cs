namespace Mallos.Ai.Dialog
{
    public interface IDialogTreeRunner
    {
        bool IsActive { get; }

        bool IsChoice { get; }

        string CurrentText { get; }

        string[] CurrentOptions { get; }

        bool Next(int index = 0, Blackboard blackboard = null);
    }
}
