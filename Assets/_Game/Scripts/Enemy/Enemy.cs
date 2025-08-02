using Configs;
using GameTick;
using System;
using UnityEngine;
using Zenject;
using R3;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    public class Enemy : IDisposable, ITickable
    {
        private EnemyView _view;
        private Health _health;

        private EnemyPath _path;
        private float _pathPassingRate;
        private float _pathPassingProgress;

        private GameTickProvider _tickProvider;

        public Observable<Unit> OnDeathSignal => _health.OnDeathSignal;

        [Inject]
        public void Construct(GameTickProvider tickProvider)
        {
            _tickProvider = tickProvider;
            _tickProvider.AddTickable(this);
        }

        public Enemy(EnemyView view, EnemyConfig config, EnemyPath path)
        {
            _view = view;
            _health = new Health(config.Health);

            foreach (var damageReceiver in _view.DamageRecipients)
                damageReceiver.DamageSignal.Subscribe(damage => _health.TakeDamage(damage));
            _health.OnDeathSignal.Subscribe(_ => OnDeath());

            _path = path;
            _pathPassingRate = config.PathPassingRate;
        }

        public void Tick(float deltaTime)
        {
            MoveAlongPath(deltaTime);
        }

        public void Dispose()
        {
            _tickProvider.RemoveTickable(this);
        }

        private void MoveAlongPath(float deltaTime)
        {
            _pathPassingProgress += deltaTime * _pathPassingRate;
            
            if (_path.IsClosed)
                _pathPassingProgress = Mathf.Repeat(_pathPassingProgress, 1f);

            var position = _path.EvaluatePosition(_pathPassingProgress);
            var tangent = _path.EvaluateTangent(_pathPassingProgress);
            var upVector = _path.EvaluateUpVector(_pathPassingProgress);
            var rotation = Quaternion.LookRotation(tangent, upVector);

            _view.Move(position);
            _view.Rotate(rotation);
        }

        private void OnDeath()
        {
            _view.Destroy();
            Dispose();
        }
    }
}