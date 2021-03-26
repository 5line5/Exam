using System.Collections.Generic;

namespace KizhiPart3._2.Consts
{
    public static class Rules
    {
        public static readonly List<string> RulesForInterpreter = new List<string>
        {
            $"{KeyWords.Set}:{KeyWords.Set}=>{KeyWords.NotFixed}=>{KeyWords.NotFixed}",
            $"{KeyWords.Sub}:{KeyWords.Sub}=>{KeyWords.NotFixed}=>{KeyWords.NotFixed}",
            $"{KeyWords.Rem}:{KeyWords.Rem}=>{KeyWords.NotFixed}",
            $"{KeyWords.Print}:{KeyWords.Print}=>{KeyWords.NotFixed}",
            $"{KeyWords.Set} {KeyWords.Code}:{KeyWords.Set}=>{KeyWords.Code}",
            $"{KeyWords.Call}:{KeyWords.Call}=>{KeyWords.NotFixed}",
            $"{KeyWords.End} {KeyWords.Set} {KeyWords.Code}:{KeyWords.End}=>{KeyWords.Set}=>{KeyWords.Code}",
            $"{KeyWords.Run}:{KeyWords.Run}",
        };
        
        public static readonly List<string> RulesForDebugger = new List<string>
        {
            $"{KeyWords.Add} {KeyWords.Break}:{KeyWords.Add}=>{KeyWords.Break}=>{KeyWords.NotFixed}",
            $"{KeyWords.Step}:{KeyWords.Step}",
            $"{KeyWords.Step} {KeyWords.Over}:{KeyWords.Step}=>{KeyWords.Over}",
            $"{KeyWords.Print} {KeyWords.Mem}:{KeyWords.Print}=>{KeyWords.Mem}",
            $"{KeyWords.Print} {KeyWords.Trace}:{KeyWords.Print}=>{KeyWords.Trace}",
            $"{KeyWords.Run}:{KeyWords.Run}",
        };
    }
}