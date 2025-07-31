using System.Collections.Generic;
using UnityEngine;

namespace GameTick
{
    public class GameTickProvider : MonoBehaviour
    {
        private List<ITickable> _tickables = new();

        public void AddTickable(ITickable tickable) => _tickables.Add(tickable);

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            foreach (var tickable in _tickables)
            {
                tickable?.Tick(deltaTime);
            }
        }
    }
}