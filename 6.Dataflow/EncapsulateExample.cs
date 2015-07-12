using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace DotNetAsync.Dataflow
{
    public class EncapsulateExample
    {
        public void Run()
        {
            var customBlock = CreateCustomBlock();
            var actionBlock = new ActionBlock<KeyValuePair<string, long>>(kvp =>
            {
                Console.WriteLine("File '{0}' has {1} bytes.", kvp.Key, kvp.Value);
            });

            customBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

            Console.WriteLine("Listing contents of current directory using a custom block.");
            customBlock.Post(".");
            customBlock.Complete();
            actionBlock.Completion.Wait();
            Console.WriteLine("Done.");
        }

        private static IPropagatorBlock<string, KeyValuePair<string, long>> CreateCustomBlock()
        {
            var directoryBrowserBlock = new TransformManyBlock<string, string>(path =>
            {
                var dir = new DirectoryInfo(path);
                return dir.EnumerateFileSystemInfos()
                    .Select(fi => fi.FullName);
            });
            var fileSizeCalculator = new TransformBlock<string, KeyValuePair<string, long>>(fileName =>
            {
                var fi = new FileInfo(fileName);
                return new KeyValuePair<string, long>(fileName, fi.Length);
            });

            directoryBrowserBlock.LinkTo(fileSizeCalculator, new DataflowLinkOptions { PropagateCompletion = true }, File.Exists);
            var customBlock = DataflowBlock.Encapsulate(directoryBrowserBlock, fileSizeCalculator);
            return customBlock;
        }
    }
}