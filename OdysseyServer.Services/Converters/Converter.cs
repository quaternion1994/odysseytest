using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdysseyServer.Services.Converters
{
    public static class Converter
    {
        public static AbilityDbo UpdateDboByAbility(Ability ability, AbilityDbo abilityDbo)
        {
            abilityDbo.Level = ability.Level;
            abilityDbo.Stats.Attack = ability.Stats.Attack;
            abilityDbo.Stats.Defence = abilityDbo.Stats.Defence;
            abilityDbo.RequiredLevel = ability.RequiredLevel;
            abilityDbo.Name = ability.Name;
            abilityDbo.RowVersion = ability.RowVersion.ToByteArray();
            abilityDbo.Stats.RowVersion = ability.Stats.RowVersion.ToByteArray();
            return abilityDbo;
        }
    }
}
