using System.Collections.Generic;

namespace KizhiPart1.Consts
{
    public static class Rules
    {
        public static readonly List<string> RulesForInterpretator = new List<string>
        {
            $"{KeyWords.Set}:{KeyWords.Set}=>{KeyWords.NotFixed}=>{KeyWords.NotFixed}",
            $"{KeyWords.Sub}:{KeyWords.Sub}=>{KeyWords.NotFixed}=>{KeyWords.NotFixed}",
            $"{KeyWords.Rem}:{KeyWords.Rem}=>{KeyWords.NotFixed}",
            $"{KeyWords.Print}:{KeyWords.Print}=>{KeyWords.NotFixed}",
        };
    }
}