using System;
using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Extensions
{
    public static class IVariableExtensions
    {
        public static int AddValue(this IVariables<int> vars, int variableKey, int amountToAdd)
        {
            var currentValue = vars.Get(variableKey);
            var newValue = currentValue + amountToAdd;
            vars[variableKey] = newValue;
            return newValue;
        }
        
        public static float AddValue(this IVariables<float> vars, int variableKey, float amountToAdd)
        {
            var currentValue = vars.Get(variableKey);
            var newValue = currentValue + amountToAdd;
            vars[variableKey] = newValue;
            return newValue;
        }
        
        public static int AddValue(this IVariables<int> vars, int variableKey, int amountToAdd, int min, int max)
        {
            var currentValue = vars.Get(variableKey);
            var newValue = currentValue + amountToAdd;
            if(newValue < min) { newValue = min; }
            if(newValue > max) { newValue = max; }
            vars[variableKey] = newValue;
            return newValue;
        }        
        
        public static float AddValue(this IVariables<float> vars, int variableKey, float amountToAdd, float min, float max)
        {
            var currentValue = vars.Get(variableKey);
            var newValue = currentValue + amountToAdd;
            if(newValue < min) { newValue = min; }
            if(newValue > max) { newValue = max; }
            vars[variableKey] = newValue;
            return newValue;
        }
        
        public static int GetInt(this IVariables<object> vars, int variableKey)
        { return Convert.ToInt32(vars.Get(variableKey)); }

        public static int GetIntOrDefault(this IVariables<object> vars, int variableKey, int defaultValue)
        { return vars.ContainsKey(variableKey) ? vars.GetInt(variableKey) : defaultValue; }
        
        public static float GetFloat(this IVariables<object> vars, int variableKey)
        { return Convert.ToSingle(vars.Get(variableKey)); }

        public static float GetFloatOrDefault(this IVariables<object> vars, int variableKey, float defaultValue)
        { return vars.ContainsKey(variableKey) ? vars.GetFloat(variableKey) : defaultValue; }
        
        public static byte GetByte(this IVariables<object> vars, int variableKey)
        { return Convert.ToByte(vars.Get(variableKey)); }

        public static byte GetByteOrDefault(this IVariables<object> vars, int variableKey, byte defaultValue)
        { return vars.ContainsKey(variableKey) ? vars.GetByte(variableKey) : defaultValue; }
        
        public static bool GetBool(this IVariables<object> vars, int variableKey)
        { return Convert.ToBoolean(vars.Get(variableKey)); }

        public static bool GetBoolOrDefault(this IVariables<object> vars, int variableKey, bool defaultValue)
        { return vars.ContainsKey(variableKey) ? vars.GetBool(variableKey) : defaultValue; }
        
        public static T GetAs<T>(this IVariables<object> vars, int variableKey) where T : class
        { return vars.Get(variableKey) as T; }

        public static T GetAsOrDefault<T>(this IVariables<object> vars, int variableKey, Func<T> defaultValueFactory) where T : class
        {
            if (vars.ContainsKey(variableKey))
            { return vars.GetAs<T>(variableKey) ?? defaultValueFactory(); }
            return  defaultValueFactory();
        }
    }
}