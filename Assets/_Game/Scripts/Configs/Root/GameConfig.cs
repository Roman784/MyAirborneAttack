using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Configs/New Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public GameplayLevelsConfig LevelsConfig { get; private set; }
    }
}
