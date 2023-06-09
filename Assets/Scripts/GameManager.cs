using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
  private static GameManager _instance;
  public static GameManager Instance
  {
    get
    {
      if (!_instance)
      {
        _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

        if (_instance == null)
          Debug.Log("no Singleton object");
      }
      return _instance;
    }
  }

  private int playerHp;

  // 스테이지 씬 이름 배열    
  public string[] sceneNames;
  // 각 스테이지 별 적의 수
  private int[] stageEnemyCounts = { 20, 25, 30 };

  // 현재 스테이지 인덱스
  private int currentStageIndex = 0;
  // 현재 스테이지에서 남은 적의 수
  private int remainingEnemies;


  private void Awake()
  {
    if (_instance = null) _instance = this;
    else if (_instance != this) Destroy(gameObject);
    DontDestroyOnLoad(gameObject);
    Init();
  }

  public void Init()
  {
    playerHp = 5;
  }

  private void Start()
  {
    remainingEnemies = stageEnemyCounts[currentStageIndex];
    LoadStage(sceneNames[currentStageIndex]);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      TogglePauseGame();
    }
  }

  public void LoadNextStage()
  {
    currentStageIndex++;
    if (currentStageIndex >= sceneNames.Length)
    {
      // 마지막 스테이지를 클리어했을 때의 동작
      Debug.Log("Game Cleared!");
      // 게임 클리어 UI를 표시하거나 게임 종료
      return;
    }
    remainingEnemies = stageEnemyCounts[currentStageIndex];
    LoadStage(sceneNames[currentStageIndex]);
  }

  private void LoadStage(string sceneName)
  {
    SceneManager.LoadScene(sceneName);

  }

  public void TogglePauseGame()
  {
    // TODO: 게임 일시 정지 로직 구현
  }

  public void PlayerDamaged(int damage)
  {
    playerHp -= damage;
  }

  public bool CheckAllEnemiesDefeated()
  {
    if (remainingEnemies <= 0)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

    public void SetPlayerHp(int amount)
    {
        playerHp -= amount;
    }
}
