using R3;
using System;

namespace Gameplay
{
    public class Health
    {
        private readonly float _max;
        private float _current;

        private Subject<(float, float)> _damageSignalSubj = new(); // <(damage, current)>
        private Subject<Unit> _onDeathSignalSubj = new();

        public float Ratio => _current / _max;

        public Observable<(float, float)> DamageSignal => _damageSignalSubj;
        public Observable<Unit> OnDeathSignal => _onDeathSignalSubj;

        public Health(float max)
        {
            _max = max;
            _current = _max;
        }

        public void TakeDamage(float damage)
        {
            _current -= damage;
            _current = Math.Clamp(_current, 0, _max);

            _damageSignalSubj.OnNext((damage, _current));

            if (_current == 0)
                OnDeath();
        }

        private void OnDeath()
        {
            _onDeathSignalSubj.OnNext(Unit.Default);
            _onDeathSignalSubj.OnCompleted();
        }
    }
}