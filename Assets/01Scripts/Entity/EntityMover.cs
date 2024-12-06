using System;
using DG.Tweening;
using GGM.Core.StatSystem;
using UnityEngine;

namespace GGM.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        [field: SerializeField] public bool CanManualMove { get; set; } = true;
        public event Action<Vector2> OnMovement;

        [Header("Collision detect")]
        [SerializeField] private Transform _groundCheckerTrm;
        [SerializeField] private Vector2 _checkerSize;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _checkDistance;

        [Space] 
        [SerializeField] private StatSO _moveStat;
        
        private Rigidbody2D _rbCompo;
        private EntityRenderer _renderer;
        private EntityStat _statCompo;

        private float _movementX;
        private float _moveSpeed = 6f;
        
        private float _speedMultiplier, _originalGravity;
        
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _statCompo = entity.GetCompo<EntityStat>();

            _originalGravity = _rbCompo.gravityScale; //공중에서 느려지게 할 때 
            _speedMultiplier = 1f;
        }
        
        public void AfterInit()
        {
            _moveStat = _statCompo.GetStat(_moveStat);
            _moveStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _moveStat.Value; //초기화 한번
        }

        private void OnDestroy()
        {
            _moveStat.OnValueChange -= HandleMoveSpeedChange;   
        }

        private void HandleMoveSpeedChange(StatSO stat, float currentValue, float prevValue)
            => _moveSpeed = currentValue;
        

        private void FixedUpdate()
        {
            if(CanManualMove)
                _rbCompo.linearVelocityX = _movementX * _moveSpeed * _speedMultiplier;
            
            OnMovement?.Invoke(_rbCompo.linearVelocity);
        }
        
        public void SetMovement(float xMovement)
        {
            _movementX = xMovement;
            _renderer.FlipController(_movementX);
        }

        public void StopImmediately(bool isYAxisToo = false)
        {
            if (isYAxisToo)
                _rbCompo.linearVelocity = Vector2.zero;
            else
                _rbCompo.linearVelocityX = 0;
            
            _movementX = 0;
        }

        public void SetSpeedMultiplier(float value) => _speedMultiplier = value;
        public void SetGravityScale(float value) => _rbCompo.gravityScale = value;

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rbCompo.AddForce(force, mode);
        }

        #region KnockBack logic

        public void KnockBack(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately(true);
            AddForceToEntity(force);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }

        #endregion 

        #region Collision check 

        public virtual bool IsGroundDetected()
            => Physics2D.BoxCast(_groundCheckerTrm.position,
                _checkerSize, 0, Vector2.down, _checkDistance, _whatIsGround );

        #endregion
        
        #if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (_groundCheckerTrm != null)
            {
                Gizmos.DrawWireCube(_groundCheckerTrm.position - new Vector3(0, _checkDistance * 0.5f),
                    new Vector3(_checkerSize.x, _checkDistance, 1f));
            }
        }
        #endif
    }
}
