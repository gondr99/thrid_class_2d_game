using GGM.Entities;
using Unity.Behavior;
using UnityEngine;

namespace GGM.Enemies
{
    public class BTEnemy : Entity
    {
        [SerializeField] protected LayerMask _whatIsPlayer;
        protected BehaviorGraphAgent _btAgent;
            
        protected override void Awake()
        {
            base.Awake();
            _btAgent = GetComponent<BehaviorGraphAgent>();
        }

        public BlackboardVariable<T> GetVariable<T>(string variableName)
        {
            if(_btAgent.GetVariable(variableName, out BlackboardVariable<T> variable))
            {
                return variable;
            }
            return null;
        }

        public Transform CheckPlayerInRadius(float radius)
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, radius, _whatIsPlayer);
            return target != null ? target.transform : null;
        }
    }
    
}
