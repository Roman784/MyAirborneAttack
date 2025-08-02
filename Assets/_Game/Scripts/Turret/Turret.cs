using GameTick;
using R3;
using System;
using UnityEngine;
using Zenject;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    [RequireComponent(typeof(TurretRotation))]
    public class Turret : MonoBehaviour, ITickable
    {
        [SerializeField] private DamageReceiver[] _damageRecipients;
        [SerializeField] private Transform _cameraAnchor;

        [Space]

        [SerializeField] private ShootingData _shootingData;

        private const float MAX_HEALTH = 10;

        private Health _health;
        private TurretRotation _rotation;
        private TurretShooting _shooting;

        private ITurretInput _input;
        private GameTickProvider _tickProvider;

        public Health Health => _health;
        public Observable<Unit> OnDeathSignal => _health.OnDeathSignal;

        [Inject]
        public void Construct(ITurretInput input, GameTickProvider tickProvider)
        {
            _input = input;

            _tickProvider = tickProvider;
            _tickProvider.AddTickable(this);
        }

        public Turret Init(Transform anchor)
        {
            InitHealth(MAX_HEALTH);
            InitRotation();
            InitShooting();
            InitPosition(anchor);

            return this;
        }

        private void InitHealth(float health)
        {
            _health = new Health(health);
            foreach (var damageReceiver in _damageRecipients)
            {
                damageReceiver.DamageSignal.Subscribe(damage => _health.TakeDamage(damage));
            }
            _health.OnDeathSignal.Subscribe(_ => OnDeath());
        }

        private void InitRotation()
        {
            _rotation = GetComponent<TurretRotation>();
        }

        private void InitShooting()
        {
            var shooting = GetComponent<Shooting>();

            if (shooting == null)
                throw new NullReferenceException($"Failed to get {typeof(Shooting)}!");

            _shooting = new TurretShooting(_shootingData, shooting);
        }

        private void InitPosition(Transform anchor)
        {
            transform.position = anchor.position;
            transform.rotation = anchor.rotation;
            transform.SetParent(anchor, false);
        }

        private void OnDestroy()
        {
            _tickProvider.RemoveTickable(this);
        }

        public void Tick(float deltaTime)
        {
            if (!_input.IsActive()) return;
            var inputAxes = _input.GetAxes();

            _rotation.Rotate(inputAxes, deltaTime);
            _shooting.TryShoot();
        }

        public void AttachCamera(TrackingCamera camera)
        {
            var initialRotation = new Vector2(0, _rotation.InitialBarrelAngle);
            camera.Attach(_cameraAnchor, _rotation.AnglesChangedSignal, initialRotation);
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}