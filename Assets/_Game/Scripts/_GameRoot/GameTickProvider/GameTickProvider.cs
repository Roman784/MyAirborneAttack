using System.Collections.Generic;
using UnityEngine;

namespace GameTick
{
    public class GameTickProvider : MonoBehaviour
    {
        private HashSet<ITickable> _tickables = new();

        private HashSet<ITickable> _toAddTickables = new();
        private HashSet<ITickable> _toRemoveTickables = new();

        private bool _isPaused = false;

        public void AddTickable(ITickable tickable) => _toAddTickables.Add(tickable);
        public void RemoveTickable(ITickable tickable) => _toRemoveTickables.Add(tickable);

        private void Update()
        {
            if (_isPaused) return;

            float deltaTime = Time.deltaTime;

            foreach (var tickable in _tickables)
            {
                tickable.Tick(deltaTime);
            }

            // To avoid modifying the collection while the main loop is running.
            AddRequiredTickables(_toAddTickables);
            RemoveRequiredTickables(_toRemoveTickables);
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }

        private void AddRequiredTickables(HashSet<ITickable> requiredTickables)
        {
            if (requiredTickables.Count == 0) return;
            _tickables.UnionWith(requiredTickables);
            requiredTickables.Clear();
        }

        private void RemoveRequiredTickables(HashSet<ITickable> requiredTickables)
        {
            if (requiredTickables.Count == 0) return;
            _tickables.ExceptWith(requiredTickables);
            requiredTickables.Clear();
        }
    }
}