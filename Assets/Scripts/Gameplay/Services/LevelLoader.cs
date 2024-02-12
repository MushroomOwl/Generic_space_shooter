using UnityEngine;

namespace Game
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Transform _SpawnPoint;
        [SerializeField] private HUD _HUDPrefab;
        [SerializeField] private GameManager _GameManagerPrefab;
        [SerializeField] private LevelGUI _LevelGUIPrefab;
        [SerializeField] private InputHandler _InputHandlerPrefab;
        [SerializeField] private ParallaxBackground _BackgroundPrefab;
        [SerializeField] private FollowingCameraHandler _CameraPrefab;
        [SerializeField] private Player _PlayerPrefab;
        [SerializeField] private GameObject _ReferencesPrefab;

        private void Awake()
        {
            Instantiate(_ReferencesPrefab.gameObject);
            Instantiate(_HUDPrefab);

            Instantiate(_GameManagerPrefab).Init();
            Instantiate(_LevelGUIPrefab).Init();

            LevelGUI.PauseM.Init();
            LevelGUI.LvlFailedM.Init();
            LevelGUI.LvlFinishedM.Init();

            Instantiate(_InputHandlerPrefab).Init().ConfigureControls();

            FollowingCameraHandler camera = Instantiate(_CameraPrefab, _SpawnPoint.position, Quaternion.identity);
            ParallaxBackground background = Instantiate(_BackgroundPrefab);
            Player player = Instantiate(_PlayerPrefab);

            background.SetCamera(camera);
            player.SetCamera(camera);
            player.SetSpawnPoint(_SpawnPoint.position);
        }
    }
}
