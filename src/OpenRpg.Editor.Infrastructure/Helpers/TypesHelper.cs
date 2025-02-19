using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.CurveFunctions;
using OpenRpg.Editor.Infrastructure.Extensions;
using OpenRpg.Editor.Infrastructure.Models;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Quests.Types;

namespace OpenRpg.Editor.Infrastructure.Helpers
{
    public static class TypesHelper
    {
        public static OptionData[] GetTypesFor(Type typesObject)
        {
            var optionData = new List<OptionData>();
            var fields = typesObject.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            foreach (var property in fields)
            {
                var value = (int)property.GetValue(null);
                optionData.Add(new OptionData(value, property.Name.MakeReadable()));
            }

            return optionData.ToArray();
        }
        
        public static readonly OptionData[] GetItemTypes = GetTypesFor(typeof(FantasyItemTypes)).OrderBy(x => x.Id).ToArray();
        public static readonly OptionData[] GetItemQualityTypes = GetTypesFor(typeof(FantasyItemQualityTypes)).OrderBy(x => x.Id).ToArray();
        public static readonly OptionData[] GetRequirementTypes = GetTypesFor(typeof(FantasyRequirementTypes)).OrderBy(x => x.Id).ToArray();
        public static readonly OptionData[] GetModificationTypes = GetTypesFor(typeof(FantasyModificationTypes)).OrderBy(x => x.Id).ToArray();
        public static readonly OptionData[] GetEffectTypes = GetTypesFor(typeof(FantasyEffectTypes)).OrderBy(x => x.Id).ToArray();
        public static readonly OptionData[] GetRewardTypes = GetTypesFor(typeof(FantasyRewardTypes)).OrderBy(x => x.Id).ToArray();
        public static readonly OptionData[] GetObjectiveTypes = GetTypesFor(typeof(ObjectiveTypes)).OrderBy(x => x.Id).ToArray();
    }
}