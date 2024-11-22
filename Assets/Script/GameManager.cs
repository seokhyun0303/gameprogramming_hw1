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
                killGoal = -1; // ȿ���� �ݺ� ������� �ʵ��� ����
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
            deathEffect.TriggerDeathEffect(); // ���� ȭ�� ȿ�� ����
        }
        else
        {
            Debug.LogError("DeathEffect ��ũ��Ʈ�� ������� �ʾҽ��ϴ�!");
        }

        // �� ��ȯ�� ���� ȭ�� ����Ʈ�� ���� �� �ڷ�ƾ���� ó���� �� ����
        StartCoroutine(LoadLoseSceneWithDelay());
    }

    private IEnumerator LoadLoseSceneWithDelay()
    {
        yield return new WaitForSeconds(1.5f); // ����Ʈ ���� �ð��� ���� ����
        SceneManager.LoadScene("lose");
    }

}
