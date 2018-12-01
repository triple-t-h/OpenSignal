namespace OpenSignal
{
    interface ISignal<T> : IOnceSignal<T>
    {
        void AddAction(T action);
    }
}
