using Configs;
using GameTick;
using System;
using UnityEngine;
using Zenject;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    public class Enemy : IDisposable, ITickable
    {
        private EnemyView _view;

        private EnemyPath _path;
        private float _pathPassingRate;
        private float _pathPassingProgress;

        private GameTickProvider _tickProvider;

        [Inject]
        public void Construct(GameTickProvider tickProvider)
        {
            _tickProvider = tickProvider;
            _tickProvider.AddTickable(this);
        }

        public Enemy(EnemyView view, EnemyConfig config, EnemyPath path)
        {
            _view = view;

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
            _pathPassingProgress = Mathf.Repeat(_pathPassingProgress + deltaTime * _pathPassingRate, 1f);

            var position = _path.EvaluatePosition(_pathPassingProgress);
            var tangent = _path.EvaluateTangent(_pathPassingProgress);
            var upVector = _path.EvaluateUpVector(_pathPassingProgress);
            var rotation = Quaternion.LookRotation(tangent, upVector);

            _view.Move(position);
            _view.Rotate(rotation);
        }
    }
}