using System;

namespace OpenSignal
{
    sealed class SlotList
    {
        private ISlot[] _slots;

        private SlotList() => _slots = _slots ?? new ISlot[0];

        static public SlotList GetEmtySlotList() => new SlotList();

        public void Append(ISlot slot)
        {
            int indexOfArray = _slots.Length;
            int newLength = indexOfArray + 1;
            Array.Resize(ref _slots, newLength);
            _slots[indexOfArray] = slot;
            NonEmpty = true;
        }        

        public void RemoveAllSlotsWithOnceAction()
        {
            _slots = Array.FindAll<ISlot>(_slots, s => !s.Once);
            HasRemovedAllSlotsWithOnceAction = true;
        }

        public void RemoveSlotWithThisAction(Delegate action)
            => _slots = Array.FindAll<ISlot>(_slots, s => s.Action != action);
                
        public ISlot FindAction(Delegate action)
            => Array.Find<ISlot>(_slots, s => s.Action == action);

        public bool NonEmpty { get; private set; } = false;
        public bool HasRemovedAllSlotsWithOnceAction { get; private set; } = false;        
        public int Length => _slots.Length;
        public ISlot this[int i] => _slots[i];
    }
}
