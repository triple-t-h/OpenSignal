using System;

namespace OpenSignal
{
    struct Slot : ISlot
    {
        public Slot(Delegate action, bool once = false)
        {
            Action = action;
            Once = once;
        }

        public void Execute1(object[] obj) => Action.DynamicInvoke(obj);

        public void Execute() => Action.DynamicInvoke();

        public override string ToString() => "[Slot action: " + Action + ", once: " + Once + "]";

        public Delegate Action { get; }
        public bool Once { get; }        
    }
}
