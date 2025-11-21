using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Player player;
    [SerializeField] private CinemachineCamera cinemachineCam;
    public Player Player
    {
        get
        {
            if (player == null)
            {
                player = GameObject.Find("Player").GetComponent<Player>();
            }
            return player;
        }
    }
    [field: SerializeField] public StageInfoSO StageSO { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Vector2 startPoint = new Vector2(0, StageSO.StartY);
        Player.transform.position = startPoint;
        cinemachineCam.transform.position = startPoint;
    }

    private void Start()
    {
        Player.PositionCheckerCompo.OnNearClear += CheckGameOver;
        Player.PositionCheckerCompo.OnNearGround += CheckGameClear;
    }
    private void CheckGameOver(float value)
    {
        if (value == 1)
        {
            UIManager.Instance.OpenUI(UIType.ClearUI);
            Player.PositionCheckerCompo.OnNearClear -= CheckGameOver;
        }
    }

    private void CheckGameClear(float value)
    {
        if (value == 1)
        {
            UIManager.Instance.OpenUI(UIType.GameOverUI);
            Player.PositionCheckerCompo.OnNearGround -= CheckGameClear;
        }
    }
}
