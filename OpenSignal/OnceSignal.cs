using System;

namespace OpenSignal
{
    public abstract class OnceSignal<T> : IOnceSignal<T> where T : class
    {
        private SlotList _slots;

        public OnceSignal()
        {
            if (!typeof(T).IsSubclassOf(typeof(Delegate)))
            {
                throw new InvalidOperationException(typeof(T).Name + " is not a delegate type");
            }

            _slots = _slots ?? SlotList.GetEmtySlotList();
        }

        public void AddOnceAction(T action) => RegisterAction(action as Delegate, true);

        protected void RegisterAction(Delegate action, bool once = false)
        {
            if (!RegistrationPossible(action, once))
                return;

            _slots?.Append(new Slot(action, once));
        }

        private bool RegistrationPossible(Delegate action, bool once)
        {
            if (!_slots.NonEmpty) return true;

            ISlot existingSlot = _slots?.FindAction(action);

            if (existingSlot == null) return true;

            if (existingSlot.Once != once)
            {
                // If the Action was previously added, definitely don't add it again.
                // But throw an exception if their once values differ.
                throw new InvalidOperationException("You cannot addOnce() then add() the same Action without removing the relationship first.");
            }

            return false; // Action was already registered.
        }

        public void Dispatch(params object[] obj)
        {
            int slotsLength = _slots.Length;

            for(int i = 0; i < slotsLength; ++i)
            {
                ISlot activeSlot = _slots[i];

                if (obj.Length > 0)
                {
                    activeSlot?.Execute1(obj);
                }
                else
                    activeSlot?.Execute();
            }

            if (_slots.HasRemovedAllSlotsWithOnceAction) return;
            _slots.RemoveAllSlotsWithOnceAction();
        }

        public void RemoveAction(T action) => _slots?.RemoveSlotWithThisAction(action as Delegate);

        public void RemoveAll() => _slots = SlotList.GetEmtySlotList();

        public int NumListeners => _slots.Length;        
    }
}
