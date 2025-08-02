using Gameplay;
using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameplayLevelConfig", 
                     menuName = "Game Configs/Gameplay level/New Gameplay Level Config", order = 1)]
    public class GameplayLevelConfig : ScriptableObject
    {
        [field: SerializeField] public string NameId { get; private set; }
        [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public string ViewNameId { get; private set; }
    }
}