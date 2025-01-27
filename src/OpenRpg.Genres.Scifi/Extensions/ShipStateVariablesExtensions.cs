using System;
using System.Linq;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipStateVariablesExtensions
    {
        public static int Armour(this ShipStateVariables state) => (int)state.Get(ScifiShipStateVariableTypes.Armour);
        public static void Armour(this ShipStateVariables state, int value) => state[ScifiShipStateVariableTypes.Armour] = value;
        
        public static void AddArmour(this ShipStateVariables state, int change, int? max = null)
        {
            var newValue = state.Armour() + change;
            if(newValue <= 0) { newValue = 0; }
            
            if(max == null) 
            { state.Armour(newValue); }
            else 
            {state.EnsureArmourInBounds(newValue, max.Value); }
        }

        public static void DeductArmour(this ShipStateVariables state, int change, int? max = null)
        {
            var newValue = state.Armour() - change;
            if(newValue <= 0) { newValue = 0; }
            
            if(max == null) 
            { state.Armour(newValue); }
            else 
            {state.EnsureArmourInBounds(newValue, max.Value); }
        }

        public static void EnsureArmourInBounds(this ShipStateVariables state, int value, int max)
        {
            if(value > max)
            { state[ScifiShipStateVariableTypes.Armour] = max; }
            else if(value <= 0)
            { state[ScifiShipStateVariableTypes.Armour] = 0; }
            else
            { state[ScifiShipStateVariableTypes.Armour] = value; }
        }
        
        public static int Shield(this ShipStateVariables state) => (int)state.Get(ScifiShipStateVariableTypes.Shield);
        public static void Shield(this ShipStateVariables state, int value) => state[ScifiShipStateVariableTypes.Shield] = value;
        
        public static void AddShield(this ShipStateVariables state, int change, int? max = null)
        {
            var newValue = state.Shield() + change;
            if(newValue <= 0) { newValue = 0; }
            
            if(max == null) 
            { state.Shield(newValue); }
            else 
            {state.EnsureShieldInBounds(newValue, max.Value); }
        }

        public static void DeductShield(this ShipStateVariables state, int change, int? max = null)
        {
            var newValue = state.Shield() - change;
            if(newValue <= 0) { newValue = 0; }
            
            if(max == null) 
            { state.Shield(newValue); }
            else 
            {state.EnsureShieldInBounds(newValue, max.Value); }
        }

        public static void EnsureShieldInBounds(this ShipStateVariables state, int value, int max)
        {
            if(value > max)
            { state[ScifiShipStateVariableTypes.Shield] = max; }
            else if(value <= 0)
            { state[ScifiShipStateVariableTypes.Shield] = 0; }
            else
            { state[ScifiShipStateVariableTypes.Shield] = value; }
        }
        
        public static int Energy(this ShipStateVariables state) => (int)state.Get(ScifiShipStateVariableTypes.Shield);
        public static void Energy(this ShipStateVariables state, int value) => state[ScifiShipStateVariableTypes.Shield] = value;
        
        public static void AddEnergy(this ShipStateVariables state, int change, int? max = null)
        {
            var newValue = state.Energy() + change;
            if(newValue <= 0) { newValue = 0; }
            
            if(max == null) 
            { state.Energy(newValue); }
            else 
            {state.EnsureEnergyInBounds(newValue, max.Value); }
        }

        public static void DeductEnergy(this ShipStateVariables state, int change, int? max = null)
        {
            var newValue = state.Energy() - change;
            if(newValue <= 0) { newValue = 0; }
            
            if(max == null) 
            { state.Energy(newValue); }
            else 
            {state.EnsureEnergyInBounds(newValue, max.Value); }
        }

        public static void EnsureEnergyInBounds(this ShipStateVariables state, int value, int max)
        {
            if(value > max)
            { state[ScifiShipStateVariableTypes.Energy] = max; }
            else if(value <= 0)
            { state[ScifiShipStateVariableTypes.Energy] = 0; }
            else
            { state[ScifiShipStateVariableTypes.Energy] = value; }
        }
        
        public static void ApplyDamageToTarget(this ShipStateVariables state, ProcessedAttack attack)
        {
            var summedAttack = attack.DamageDone.Sum(x => x.Value);
            var totalDamage = (int)Math.Round(summedAttack);
            if (totalDamage < 0) { totalDamage = 0; }
            state.DeductArmour(totalDamage);
        }
        
        public static bool IsDead(this ShipStateVariables state)
        { return state.Armour() <= 0; }
    }
}