using R3;
using UnityEngine;

namespace Configs
{
    public class ScriptableObjectConfigProvider : IConfigProvider
    {
        private GameConfig _gameConfigs;

        public GameConfig GameConfigs => _gameConfigs;

        public Observable<bool> LoadGameConfig()
        {
            try
            {
                _gameConfigs = Resources.Load<GameConfig>("GameConfig");
                return Observable.Return(_gameConfigs != null);
            }
            catch { return Observable.Return(false); }
        }
    }
}
