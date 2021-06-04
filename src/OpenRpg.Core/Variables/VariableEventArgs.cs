using System;

namespace OpenRpg.Core.Variables
{
    public class VariableEventArgs<K,T> : EventArgs
    {
        public K VariableType { get; }
        public T OldValue { get; }
        public T NewValue { get; }

        public VariableEventArgs(K variableType, T oldValue, T newValue)
        {
            VariableType = variableType;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}