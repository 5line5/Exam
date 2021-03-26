﻿using System;
using KizhiPart2.Consts;
using KizhiPart2.ResultPattern;

namespace KizhiPart2.Commands
{
    public class RunCommand : ICommand
    {
        private readonly Action _starter;

        public RunCommand(Action starter) => _starter = starter;

        public Result<string[]> Execute(string[] args)
        {
            _starter();
            
            return Result<string[]>.Ok(args);
        }
    }
}