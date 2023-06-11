using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
  [SerializeField]
  private int playerHp;

  // 스테이지 씬 이름 배열    
  [SerializeField]
  private string[] sceneNames;
  // 각 스테이지 별 적의 수
  private int[] stageEnemyCounts = { 1, 25, 30 };

  // 현재 스테이지 인덱스
  private int currentStageIndex = 0;
  // 현재 스테이지에서 남은 적의 수
  private int remainingEnemies;

  //목숨 갯수
  public Image[] life;

    public bool Dueling = false;


  private void Awake()
  {
    if (_instance == null) _instance = this;
    else if (_instance != this) Destroy(gameObject);
    DontDestroyOnLoad(gameObject);
    Init();
  }

  public void Init()
  {
    playerHp = 5;
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      TogglePauseGame();
    }
    if(playerHp <= 0)
        {
            Invoke("GameOver", 1f);
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


  //restart 버튼을 누르면
  public void OnClickRestart()
  {
    //첫 장면을 가져오게 된다.
    SceneManager.LoadScene("Stage_1");
    currentStageIndex = 2;
  }

  public void SetPlayerHp(int amount)
  {
    playerHp -= amount;
    UpdateLifeIcon(playerHp);
  }

    public void RestartGame()
    {
        currentStageIndex = 2;
        LoadStage(sceneNames[currentStageIndex]);
    }

    public void GameOver()
    {
        LoadStage("GameOverScreen");
    }
    
  public void UpdateLifeIcon(int playerHp)
  {
    for (int index = 0; index < 5; index++)
    {
      life[index].color = new Color(1, 1, 1, 0);
    }
    for (int index = 0; index < playerHp; index++)
    {
      life[index].color = new Color(1, 1, 1, 1);
    }
  }

}
