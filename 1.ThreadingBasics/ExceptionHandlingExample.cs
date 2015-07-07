using System;
using System.Threading;

namespace DotNetAsync.ThreadingBasics
{
    public class ExceptionHandlingExample
    {
        public void Run()
        {
            try
            {
                var thread = new Thread(DoSomeImportantStuff);
                thread.Start();
            }
            catch (Exception)
            {
                Console.WriteLine("Not going to get here.");
            }
        }

        private void DoSomeImportantStuff()
        {
            try
            {
                Console.WriteLine("Doing something important...");
                DoSomeStuffThatThrowsException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Remember to always catch the exceptions within the context of the thread!");
            }
        }

        private void DoSomeStuffThatThrowsException()
        {
            throw new Exception(message: @"Something nasty happened and I don't know what.");
        }
    }
}
