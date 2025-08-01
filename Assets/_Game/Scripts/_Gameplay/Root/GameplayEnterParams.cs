using GameRoot;

namespace GameplayRoot
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public readonly int LevelNumber;
        public readonly string TurretId;

        public GameplayEnterParams(int levelNumber, string turretId) : base(Scenes.GAMEPLAY)
        {
            LevelNumber = levelNumber;
            TurretId = turretId;
        }
    }
}