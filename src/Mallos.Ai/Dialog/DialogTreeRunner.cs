namespace Mallos.Ai.Dialog
{
    public class DialogTreeRunner : IDialogTreeRunner
    {
        public bool IsActive { get; private set; }

        public bool IsChoice { get; private set; }

        public string[] CurrentOptions { get; private set; }

        public DialogTreeRunner(
            DialogTree dialogTree,
            ITextProcessor[] textProcessors = null)
        {
        }

        public bool Next(int index = 0)
        {
            return true;
        }
    }
}
