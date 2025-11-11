using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    
    
    private bool[] levelClear = new bool[10];
        
    public static event Action<int> OnStageCleared; //호출 예시 : StageManager.Instance.ClearLevel(stageLevel);
    public static event Action<int> OnStageUnlocked; 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public bool CanOpenLevel(int level)
    {
        if (level == 1) return true; // 1레벨은 기본 레벨이니까 접근 가능
        return levelClear[level - 1]; // 이전 레벨 클리어했으면 가능
    }
    
    public void ClearLevel(int level)//레벨 클리어 처리
    {
        if (!levelClear[level]) //매개변수로 들어온 레벨이 클리어되지 않은 처리일때
        {
            levelClear[level] = true;
            SaveProgress();
            
            OnStageCleared?.Invoke(level);// 이벤트 발동
            
            if (level < 9)//최대 레벨을 넘지 않았는지
            {
                OnStageUnlocked?.Invoke(level + 1); //다음 스테이지 잠금 해제 처리
            }
            
            Debug.Log($"{level} 레벨");
        }
    }
    
    public bool IsLevelCleared(int level) //레벨이 클리어 되었는지 확인용
    {
        return levelClear[level];
    }
    
    private void SaveProgress() //레벨 저장
    {
        for (int i = 1; i <= 9; i++)
        {
            PlayerPrefs.SetInt($"Level_{i}_Clear", levelClear[i] ? 1 : 0); //PlayerPrefs로 현재 레벨 저장
        }
        PlayerPrefs.Save();
    }
    private void LoadProgress()
    {
        for (int i = 1; i <= 9; i++)
        {
            levelClear[i] = PlayerPrefs.GetInt($"Level_{i}_Clear", 0) == 1;//PlayerPrefs 기반으로 저장된값 불러오기
        }
    }
    
   
    public void ResetAllProgress() // 테스트용 초기화
    {
        for (int i = 1; i <= 9; i++)
        {
            levelClear[i] = false;
            PlayerPrefs.DeleteKey($"Level_{i}_Clear");
        }
        PlayerPrefs.Save();
    }
    void Update() //Test
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            ClearLevel(1);
            Debug.Log("레벨 1 클리어");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            ClearLevel(2);
            Debug.Log("레벨 2 클리어");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            ClearLevel(3);
            Debug.Log("레벨 3 클리어");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            ClearLevel(4);
            Debug.Log("레벨 4 클리어");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) 
        {
            ClearLevel(5);
            Debug.Log("레벨 5 클리어");
        }
        
        // ESC키로 리셋
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetAllProgress();
            Debug.Log("리셋");
        }
    }
}
