using Effects;
using R3;
using UnityEngine;

namespace Gameplay
{
    public class Hitbox : MonoBehaviour
    {
        private Subject<HitData> _onHitSignalSubj = new();
        public Observable<HitData> OnHitSignal => _onHitSignalSubj;

        public void Hit(RaycastHit hit, float damage = 0)
        {
            var hitData = new HitData()
            {
                Point = hit.point,
                Normal = hit.normal,
                Damage = damage,
            };

            _onHitSignalSubj.OnNext(hitData);
        }
    }
}