using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public float delayBeforeExit = 5f; // 게임 종료 전 대기 시간

    void Start()
    {
        // 게임 종료를 위한 코루틴 시작
        StartCoroutine(ExitGameAfterDelay());
    }

    IEnumerator ExitGameAfterDelay()
    {
        // 지정된 시간(5초) 동안 대기
        yield return new WaitForSeconds(delayBeforeExit);

        // 게임 종료
        QuitGame();
    }

    void QuitGame()
    {
        // 에디터 환경에서는 플레이 중단
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션 종료
        Application.Quit();
#endif
    }
}

