using System;

namespace Combat
{
    public class Condition
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string StartMessage { get; set; }

        public Action<Unit> OnAfterTurn { get; set; }
    }
}
    
