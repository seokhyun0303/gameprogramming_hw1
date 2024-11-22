using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public Button pauseButton; // Pause 버튼
    public TMP_Text buttonText;    // 버튼에 표시되는 텍스트

    private bool isPaused = false; // 게임이 멈췄는지 확인

    void Start()
    {
        // 버튼에 클릭 이벤트 연결
        pauseButton.onClick.AddListener(TogglePause);
    }

    void TogglePause()
    {
        if (isPaused)
        {
            // 게임 재개
            Time.timeScale = 1f; // 시간 흐름을 다시 정상으로
            buttonText.text = "Pause"; // 버튼 텍스트 변경
            isPaused = false;
            GameManager.instance.ispause = false;
        }
        else
        {
            // 게임 멈춤
            Time.timeScale = 0f; // 시간 흐름을 멈춤
            buttonText.text = "In-Game"; // 버튼 텍스트 변경
            isPaused = true;
            GameManager.instance.ispause = true;
        }
    }
}

