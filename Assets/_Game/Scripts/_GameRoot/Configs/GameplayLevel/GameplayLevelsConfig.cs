using UnityEngine;
using System;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameplayLevelsConfig", 
                     menuName = "Game Configs/Gameplay level/New Gameplay Levels Config", order = 0)]
    public class GameplayLevelsConfig : ScriptableObject
    {
        [field: SerializeField] public GameplayLevelConfig[] LevelConfigs { get; private set; }

        public GameplayLevelConfig GetLevelConfig(int number)
        {
            foreach (var levelConfig in LevelConfigs)
            {
                if (levelConfig.Number == number)
                    return levelConfig;
            }

            throw new ArgumentNullException($"Level number {number} not found!");
        }
    }
}