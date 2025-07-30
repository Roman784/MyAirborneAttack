using GameRoot;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameplayLevel
{
    public class GameplayLevelFactory
    {
        private readonly DiContainer _container;

        private Dictionary<string, GameplayLevel> _levelPrefabsMap = new();

        [Inject]
        public GameplayLevelFactory(DiContainer container)
        {
            _container = container;
        }

        public GameplayLevel Create(string nameId)
        {
            if (!_levelPrefabsMap.TryGetValue(nameId, out var prefab))
            {
                prefab = Resources.Load<GameplayLevel>(ResourcePaths.GAMEPLAY_LEVEL_PREFABS + nameId);

                if (prefab == null)
                    throw new NullReferenceException($"Level prefab '{nameId}' not found!");

                _levelPrefabsMap[nameId] = prefab;
            }

            return _container.InstantiatePrefabForComponent<GameplayLevel>(prefab);
        }
    }
}