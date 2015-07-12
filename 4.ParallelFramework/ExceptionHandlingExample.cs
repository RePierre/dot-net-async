using System;
using System.Threading.Tasks;

namespace DotNetAsync.ParallelFramework
{
    public class ExceptionHandlingExample
    {
        public void Run()
        {
            Console.WriteLine("Running a task which throws an exeption.");
            var task = Task.Factory.StartNew(() => { throw null; });
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                ex.Handle(e =>
                {
                    if (e is NullReferenceException)
                    {
                        Console.WriteLine("{0} was caught from the task.", nameof(NullReferenceException));
                        return true;
                    }
                    return false;
                });
            }
        }
    }
}