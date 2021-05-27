using System;
using System.Linq;
using System.Reflection;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DNR.Core
{
    public static class StringsDecrypter
    {
        public static int DecryptedStrings { get; private set; }

        public static void Execute(Context ctx)
        {
            var logger = ctx.Options.Logger;

            foreach (var typeDef in ctx.Module.GetTypes().Where(x => x.HasMethods && !x.IsGlobalModuleType))
            foreach (var methodDef in typeDef.Methods.Where(x => x.HasBody)) {
                var Instr = methodDef.Body.Instructions;

                methodDef.Body.SimplifyBranches();
                methodDef.Body.SimplifyMacros(methodDef.Parameters);
                
                for (var i = 0; i < Instr.Count; i++)
                    if (Instr[i].OpCode == OpCodes.Call && Instr[i].ToString().Contains("System.String") &&
                        Instr[i].Operand != null && Instr[i - 1].IsLdcI4()
                    )
                        try {
                            var ldcI4Arg = Instr[i - 1].GetLdcI4Value();
                            var DecMethod = (IMethod) Instr[i].Operand;
                            var decrypter =
                                ctx.Asm.ManifestModule.ResolveMethod((int) DecMethod.MDToken.Raw) as MethodInfo;

                            StacktracePatcher.PatchStackTraceGetMethod.MethodToReplace = decrypter;

                            var value = decrypter.Invoke(null, new object[] {ldcI4Arg});

                            logger.Success($"Restored string: {value} | ldc: {ldcI4Arg}");

                            Instr[i - 1].OpCode = OpCodes.Nop;
                            Instr[i].OpCode = OpCodes.Ldstr;
                            Instr[i].Operand = value.ToString();

                            DecryptedStrings++;
                        }
                        catch (Exception e) {
                            logger.Error(e.Message);
                        }
                
                methodDef.Body.OptimizeBranches();
                methodDef.Body.OptimizeMacros();
            }
        }
    }
}