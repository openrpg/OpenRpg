using System;

namespace OpenRpg.Core.Variables
{
    public class VariableChangedEventArgs<T> : EventArgs
    {
        public int VariableType { get; }
        public T OldValue { get; }
        public T NewValue { get; }

        public VariableChangedEventArgs(int variableType, T oldValue, T newValue)
        {
            VariableType = variableType;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}