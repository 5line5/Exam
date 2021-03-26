using System.Collections.Generic;

namespace KizhiPart2.Consts
{
    public static class Rules
    {
        public static readonly List<string> RulesForInterpretator = new List<string>
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
    }
}