using Configs;
using GameTick;
using UnityEngine;
using Zenject;
using R3;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    public class Enemy : MonoBehaviour, ITickable
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private DamageReceiver[] _damageRecipients;

        private Health _health;
        private EnemyMovement _movement;
        private EnemyShooting _shooting;

        private bool _isEnabled = true;
        private bool _canShoot = false;

        private GameTickProvider _tickProvider;

        public Observable<Unit> OnDeathSignal => _health.OnDeathSignal;

        [Inject]
        private void Construct(GameTickProvider tickProvider)
        {
            _tickProvider = tickProvider;
            _tickProvider.AddTickable(this);
        }

        public Enemy Init(EnemyConfig config, EnemyPath path, Turret turret)
        {
            _canShoot = config.CanShoot;

            InitHealth(config.Health);
            InitMovement(path, config.PathPassingRate);
            if (_canShoot) InitShooting(config.ShootingData, turret);

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

        private void InitMovement(EnemyPath path, float pathPassingRate)
        {
            _movement = new EnemyMovement(transform, path, pathPassingRate);
        }

        private void InitShooting(EnemyShootingData shootingData, Turret turret)
        {
            if (TryGetComponent<Shooting>(out var shooting))
                _shooting = new EnemyShooting(transform, shooting, shootingData, turret);
        }

        private void OnDestroy()
        {
            _isEnabled = false;
            _tickProvider.RemoveTickable(this);
        }

        public void Enable(bool setActiveView = false)
        {
            _isEnabled = true;

            if (setActiveView)
                _view.SetActive(true);
        }

        public void Disable(bool setActiveView = false)
        {
            _isEnabled = false;

            if (setActiveView)
                _view.SetActive(false);
        }

        public void Tick(float deltaTime)
        {
            if (!_isEnabled) return;

            _movement.MoveAlongPath(deltaTime);
            if (_canShoot) _shooting.TryShoot();
        }

        private void OnDeath()
        {
            Disable();

            new FallingAnimation(gameObject, _movement.Velocity);
        }
    }
}