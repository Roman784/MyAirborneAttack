using GameRoot;

namespace GameplayRoot
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public readonly int LevelNumber;

        public GameplayEnterParams(int levelNumber) : base(Scenes.GAMEPLAY)
        {
            LevelNumber = levelNumber;
        }
    }
}