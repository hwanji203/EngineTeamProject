using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    private Player player;
    [field: SerializeField] public CinemachineCamera CinemachineCam { get; private set; }
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

        try
        {
            Debug.Log("sdfsf");
            Vector2 startPoint = new Vector2(0, StageSO.StartY);
            Player.transform.position = new Vector3(startPoint.x, startPoint.y, Player.transform.position.z);
            CinemachineCam.transform.position = new Vector3(startPoint.x, startPoint.y, CinemachineCam.transform.position.z);
        }
        catch (NullReferenceException)
        {
            Debug.Log("Intro, Main Scene");
        }
    }
    private void Start()
    {
        SoundManager.Instance.Play(StageSO.StageBGM);
        try
        {
            Player.PositionCheckerCompo.OnNearClear += CheckGameClear;
            Player.PositionCheckerCompo.OnNearOutOfCam += CheckGameOver;
        }
        catch
        {
            Debug.Log("Intro, Main Scene");
        }
    }

    protected override void OnDestroy()
    {
        try
        {
            Player.PositionCheckerCompo.OnNearClear -= CheckGameClear;
            Player.PositionCheckerCompo.OnNearOutOfCam -= CheckGameOver;
        }
        catch
        {
            Debug.Log("Intro, Main Scene");
        }
        base.OnDestroy();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && TutorialManager.Instance.IsTutorialing == false && Time.timeScale != 0)
        {
            UIManager.Instance.OpenUI(UIType.SettingUI);
        }
    }

    private void CheckGameOver(float value)
    {
        if (value == 1)
        {
            SoundManager.Instance.Play(SFXSoundType.GameOver);
            UIManager.Instance.CloseAllUI(UIType.GameOverUI);
            Player.PositionCheckerCompo.OnNearOutOfCam -= CheckGameOver;
        }
    }

    private void CheckGameClear(float value)
    {
        if (value == 1)
        {
            SoundManager.Instance.Play(SFXSoundType.Clear);
            UIManager.Instance.CloseAllUI(UIType.ClearUI);
            Player.PositionCheckerCompo.OnNearClear -= CheckGameClear;
            StageManager.Instance.ClearLevel(StageSO.StageLevel);
        }
    }
}
