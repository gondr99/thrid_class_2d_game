using System.Linq;
using GGM.Core.StatSystem;
using UnityEngine;

namespace GGM.Entities
{
    public class EntityStat : MonoBehaviour, IEntityComponent
    {
        [SerializeField] protected StatOverride[] _statOverrides; //여기에 들어갈 스탯 넣어주고

        protected StatSO[] _stats; //실제 오버라이드된 스텟이 들어갈 곳들
        
        protected Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _stats = _statOverrides.Select(x => x.CreateStat()).ToArray();
        }

        public StatSO GetStat(StatSO stat)
        {
            Debug.Assert(stat != null, "Stat : Getstat - stat cannot be null");
            return _stats.FirstOrDefault(x => x.statName == stat.statName);
        }

        public bool TryGetStat(StatSO stat, out StatSO outStat)
        {
            Debug.Assert(stat != null, "TryGetStat : Getstat - stat cannot be null");
            outStat = _stats.FirstOrDefault(x => x.statName == stat.statName);
            return outStat != null;
        }

        public void SetBaseValue(StatSO stat, float value)
            => GetStat(stat).BaseValue = value;

        public float GetBaseValue(StatSO stat)
            => GetStat(stat).BaseValue;

        public float IncreaseBaseValue(StatSO stat, float value)
            => GetStat(stat).BaseValue += value;

        public void AddModifier(StatSO stat, object key, float value)
            => GetStat(stat).AddModifier(key, value);

        public void RemoveModifier(StatSO stat, object key)
            => GetStat(stat).RemoveModifier(key);

        public void ClearAllModifiers()
        {
            foreach (StatSO stat in _stats)
            {
                stat.ClearModifiers();
            }
        }
    }
}
