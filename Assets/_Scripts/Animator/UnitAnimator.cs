using UnityEngine;

namespace Units
{
    public class UnitAnimator : MonoBehaviour
    {
        private readonly int ShootAnimation = Animator.StringToHash("Shoot");
        private readonly int MoveAnimation = Animator.StringToHash("Move");


        [SerializeField]
        private Animator _animator;

        public void SetAnimatorParams(AnimatorOverrideController animatorOverrideController, Avatar avatar)
        {
            _animator.runtimeAnimatorController = animatorOverrideController;
            _animator.avatar = avatar;
        }

        public void Move(bool state)
        {
            _animator.SetBool(MoveAnimation, state);
        }

        public void Shoot()
        {
            _animator.SetTrigger(ShootAnimation);
        }
        
    }
}