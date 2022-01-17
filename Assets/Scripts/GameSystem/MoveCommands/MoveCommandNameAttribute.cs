using System;
using System.Runtime.CompilerServices;

namespace GameSystem.Models.MoveCommands
{
    public class MoveCommandNameAttribute : Attribute
    {
        public string Name
        {
            get;
        }

        public MoveCommandNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}