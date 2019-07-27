namespace Mallos.Ai.Dialog
{
    public readonly struct DialogEntity
    {
        public readonly bool IsChoice;
        public readonly string Text;

        public DialogEntity(
            bool isChoice,
            string text)
        {
            this.IsChoice = isChoice;
            this.Text = text;
        }
    }
}
