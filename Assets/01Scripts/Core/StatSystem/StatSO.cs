using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GGM.Core.StatSystem
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSO")]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValueChangeHandler(StatSO stat, float currentValue, float prevValue);
        
        public event ValueChangeHandler OnValueChange;

        public string statName;
        public string description;
        public string displayName;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _baseValue, _minValue, _maxValue;
        
        private Dictionary<object, float> _modifyValueByKey = new Dictionary<object, float>();
        
        [field:SerializeField] public bool IsPercent { get; private set; }
        
        private float _modifyValue = 0;
        public Sprite Icon => _icon;

        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public float MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }
        
        public float Value => Mathf.Clamp(_baseValue + _modifyValue, _minValue, _maxValue);
        public bool IsMax => Mathf.Approximately(Value, _maxValue);
        public bool IsMin => Mathf.Approximately(Value, _minValue);

        public float BaseValue
        {
            get => _baseValue;
            set
            {
                float prevValue = Value;
                _baseValue = Mathf.Clamp(value, MinValue, MaxValue);
                TryInvokeValueChangeEvent(Value, prevValue);
            }
        }

        public void AddModifier(object key, float value)
        {
            if (_modifyValueByKey.ContainsKey(key)) return; //이미 해당 모디파이어가 적용중이라면
            
            float prevValue = Value;

            _modifyValue += value;
            _modifyValueByKey.Add(key, value);
            
            TryInvokeValueChangeEvent(Value, prevValue);
        }

        public void RemoveModifier(object key)
        {
            if (_modifyValueByKey.TryGetValue(key, out float value))
            {
                float prevValue = Value;
                _modifyValue -= value;
                _modifyValueByKey.Remove(key);
                
                TryInvokeValueChangeEvent(Value, prevValue);
            }
        }

        public void ClearModifiers()
        {
            float prevValue = Value;
            _modifyValueByKey.Clear();
            _modifyValue = 0;
            TryInvokeValueChangeEvent(Value, prevValue);
        }
        
        private void TryInvokeValueChangeEvent(float currentValue, float prevValue)
        {
            if (Mathf.Approximately(currentValue, prevValue) == false)
            {
                OnValueChange?.Invoke(this, currentValue, prevValue);
            }
        }

        public virtual object Clone()
        {
            return Instantiate(this);
        }
    }
}
