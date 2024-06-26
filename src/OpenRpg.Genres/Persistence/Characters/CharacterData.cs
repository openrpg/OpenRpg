using System;
using System.Collections.Generic;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items.Equipment;

namespace OpenRpg.Genres.Persistence.Characters
{
    public class CharacterData
    {
        public Guid Id { get; }
        public string NameLocaleId { get; }
        public string DescriptionLocaleId { get; }
        public IReadOnlyDictionary<int, float> StateVariables { get; }
        public IReadOnlyDictionary<int, object> Variables { get; }

        public CharacterData(Guid id, string nameLocaleId, string descriptionLocaleId, IReadOnlyDictionary<int, float> stateVariables, IReadOnlyDictionary<int, object> variables = null)
        {
            Id = id;
            NameLocaleId = nameLocaleId;
            DescriptionLocaleId = descriptionLocaleId;
            StateVariables = stateVariables;
            Variables = variables ?? new Dictionary<int, object>();
        }
    }
}