using GameRoot;

namespace GameplayRoot
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public readonly int LevelNumber;
        public readonly string TurretNameId;

        public GameplayEnterParams(int levelNumber, string turretNameId) : base(Scenes.GAMEPLAY)
        {
            LevelNumber = levelNumber;
            TurretNameId = turretNameId;
        }
    }
}