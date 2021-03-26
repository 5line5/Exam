using System.Collections.Generic;

namespace Kizhi.Consts
{
    public class Rules
    {
        public const string NotFixedVariable = "*";

        public static readonly List<string> RulesForInterpretator = new List<string>
        {
            $"{Commands.SetCode}:set=>code",
            $"{Commands.Set}:set=>{NotFixedVariable}=>{NotFixedVariable}",
            $"{Commands.Sub}:sub=>{NotFixedVariable}",
            $"{Commands.Rem}:rem=>{NotFixedVariable}",
            $"{Commands.Print}:print=>{NotFixedVariable}",
            $"{Commands.Call}:call=>{NotFixedVariable}",
            $"{Commands.EndSetCode}:end=>set=>code",
            $"{Commands.Run}:run=>{NotFixedVariable}",
            $"{Commands.AddBreak}:add=>break=>{NotFixedVariable}",
            $"{Commands.Step}:step",
            $"{Commands.StepOver}:step=>over",
            $"{Commands.PrintMem}:print=>mem",
            $"{Commands.PrintTrace}:print=>trace",
        };

        public static readonly List<string> RulesForDebugger = new List<string>
        {
            $"{Commands.AddBreak}:add=>break=>{NotFixedVariable}",
            $"{Commands.StepOver}:step=>over",
            $"{Commands.Step}:step",
            $"{Commands.PrintMem}:print=>mem",
            $"{Commands.PrintTrace}:print=>trace",
            $"{Commands.Run}:run=>{NotFixedVariable}"
        };
    }
}