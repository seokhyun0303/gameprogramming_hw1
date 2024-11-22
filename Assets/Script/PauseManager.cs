using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public Button pauseButton; // Pause ��ư
    public TMP_Text buttonText;    // ��ư�� ǥ�õǴ� �ؽ�Ʈ

    private bool isPaused = false; // ������ ������� Ȯ��

    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ ����
        pauseButton.onClick.AddListener(TogglePause);
    }

    void TogglePause()
    {
        if (isPaused)
        {
            // ���� �簳
            Time.timeScale = 1f; // �ð� �帧�� �ٽ� ��������
            buttonText.text = "Pause"; // ��ư �ؽ�Ʈ ����
            isPaused = false;
            GameManager.instance.ispause = false;
        }
        else
        {
            // ���� ����
            Time.timeScale = 0f; // �ð� �帧�� ����
            buttonText.text = "In-Game"; // ��ư �ؽ�Ʈ ����
            isPaused = true;
            GameManager.instance.ispause = true;
        }
    }
}

