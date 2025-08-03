using UnityEngine;

namespace Gameplay
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Stop()
        {
            _animator.CrossFade("Stop", 1);
        }
    }
}