using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Player player;
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

        Player.transform.position = new Vector2(0, StageSO.StartY);
    }

    private void Start()
    {
        Player.PositionCheckerCompo.OnNearClear += (value) =>
        {
            Debug.Log("Clear!");
            if (value == 1) UIManager.Instance.OpenUI(UIType.ClearUI);
        };
    }
}
