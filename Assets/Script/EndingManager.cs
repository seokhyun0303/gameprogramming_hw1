using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public float delayBeforeExit = 5f; // ���� ���� �� ��� �ð�

    void Start()
    {
        // ���� ���Ḧ ���� �ڷ�ƾ ����
        StartCoroutine(ExitGameAfterDelay());
    }

    IEnumerator ExitGameAfterDelay()
    {
        // ������ �ð�(5��) ���� ���
        yield return new WaitForSeconds(delayBeforeExit);

        // ���� ����
        QuitGame();
    }

    void QuitGame()
    {
        // ������ ȯ�濡���� �÷��� �ߴ�
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� ���ø����̼� ����
        Application.Quit();
#endif
    }
}

