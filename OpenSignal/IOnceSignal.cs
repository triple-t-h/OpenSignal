namespace OpenSignal
{
    interface IOnceSignal<T>
    {
        void AddOnceAction(T action);

        void Dispatch(params object[] obj);

        void RemoveAction(T action);
    }
}
