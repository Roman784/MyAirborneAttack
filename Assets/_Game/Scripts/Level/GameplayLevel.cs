using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayLevel : MonoBehaviour
    {
        [SerializeField] private Transform _turretPoint;

        private TurretFactory _turretFactory;

        [Inject]
        private void Construct(TurretFactory turretFactory)
        {
            _turretFactory = turretFactory;
        }

        public Turret CreateTurret(string nameId)
        {
            return _turretFactory.Create(nameId, _turretPoint.position);
        }
    }
}