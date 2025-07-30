using R3;

namespace Configs
{
    public interface IConfigProvider
    {
        public GameConfig GameConfigs { get; }
        public Observable<bool> LoadGameConfig();
    }
}
