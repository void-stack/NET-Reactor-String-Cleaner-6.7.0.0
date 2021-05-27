using System.Reflection;
using dnlib.DotNet;
using dnlib.DotNet.Writer;

namespace DNR.Core
{
    public class Context
    {
        public Context(CtxOptions ctxOptions)
        {
            Options = ctxOptions;
            Module = ModuleDefMD.Load(Options.FilePath);
            Asm = Assembly.UnsafeLoadFrom(Options.FilePath);
        }

        public CtxOptions Options { get; }
        public ModuleDefMD Module { get; }
        public Assembly Asm { set; get; }

        public void Save()
        {
            var writerOptions = new ModuleWriterOptions(Module)
            {
                Logger = DummyLogger.NoThrowInstance,
                MetadataOptions =
                {
                    Flags = MetadataFlags.PreserveAll & MetadataFlags.KeepOldMaxStack
                }
            };

            Module.Write(Options.OutputPath, writerOptions);
        }
    }
}