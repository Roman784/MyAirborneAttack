using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game Configs/Enemies/New ENemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public string NameId { get; private set; }
        [field: SerializeField] public float PathPassingRate { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
    }
}