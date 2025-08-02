using UnityEngine;

namespace Gameplay
{
    public class GameplayLevelView : MonoBehaviour
    {
        [field: SerializeField] public TrackingCamera Camera { get; private set; }
        [field: SerializeField] public Transform TurretAnchor { get; private set; }
    }
}