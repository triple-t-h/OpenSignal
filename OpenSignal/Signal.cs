using System;

namespace OpenSignal
{
    public class Signal<T> : OnceSignal<T> , ISignal<T> where T : class
    {
        public Signal() : base(){}

        public void AddAction(T action) => base.RegisterAction(action as Delegate, false);
    }
}
