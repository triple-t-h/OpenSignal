using System;

namespace OpenSignal
{
    interface ISlot
    {
        Delegate Action { get; }
        bool Once { get; }
                
        void Execute();
        void Execute1(params object[] obj);
        string ToString();
    }
}
