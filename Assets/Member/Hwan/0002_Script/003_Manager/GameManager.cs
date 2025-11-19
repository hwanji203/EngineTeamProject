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
}
