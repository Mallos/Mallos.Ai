namespace Mallos.Ai
{
    using System;
    using System.Diagnostics;
    using Rant;

    public class RantTextProcessor : ITextProcessor
    {
        public RantEngine RantEngine { get; }

        public RantTextProcessor(RantEngine rantEngine = null)
        {
            this.RantEngine = rantEngine ?? new RantEngine();
        }

        public string Process(string text, Blackboard blackboard)
        {
            // FIXME: Add the blackboard context to rant engine.

            try
            {
                var pgm = RantProgram.CompileString(text);
                var output = RantEngine.Do(pgm);
                return output.Main;
            }
            catch (RantRuntimeException exception)
            {
                PrintRuntimeError(exception);
                return text;
            }
            catch (RantCompilerException exception)
            {
                PrintCompileError(exception);
                return text;
            }
        }

        [Conditional("DEBUG")]
        private void PrintCompileError(RantCompilerException exception)
        {
            Console.WriteLine("Compilation failed!");
            foreach (var msg in exception.GetErrors())
            {
                Console.WriteLine(msg.Message);
                Console.WriteLine("  - Line: {0}", msg.Line);
                Console.WriteLine("  - Column: {0}", msg.Column);
                Console.WriteLine("  - Index: {0}", msg.Index);
            }
        }

        [Conditional("DEBUG")]
        private void PrintRuntimeError(RantRuntimeException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
