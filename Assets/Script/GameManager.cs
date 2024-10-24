using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject energyshleid;

    public int killCount = 0; 
    public int killGoal = 5; 

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
        SceneManager.LoadScene("lose");
    }
}
