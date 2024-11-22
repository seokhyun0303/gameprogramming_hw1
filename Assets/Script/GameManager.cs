using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject energyshleid;

    public int killCount = 0; 
    public int killGoal = 5;
    public bool ispause = false;
    public DeathEffect deathEffect;
    public VictoryEffect victoryEffect;

    private void Awake()
    {
     
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(killCount == 5)
        {
            Destroy(energyshleid);
            if (victoryEffect != null)
            {
                victoryEffect.TriggerVictoryEffect();
                killGoal = -1; // 효과가 반복 실행되지 않도록 설정
            }
        }
    }


    public void AddKill()
    {
        killCount++;
        Debug.Log("Kill Count: " + killCount);
    }

  
    public void WinGame()
    {
        SceneManager.LoadScene("win");
    }


    public void LoseGame()
    {
        if (deathEffect != null)
        {
            deathEffect.TriggerDeathEffect(); // 붉은 화면 효과 실행
        }
        else
        {
            Debug.LogError("DeathEffect 스크립트가 연결되지 않았습니다!");
        }

        // 씬 전환은 붉은 화면 이펙트가 끝난 뒤 코루틴으로 처리될 수 있음
        StartCoroutine(LoadLoseSceneWithDelay());
    }

    private IEnumerator LoadLoseSceneWithDelay()
    {
        yield return new WaitForSeconds(1.5f); // 이펙트 지속 시간에 맞춰 조정
        SceneManager.LoadScene("lose");
    }

}
