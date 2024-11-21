using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime = 0.0f;

    void Update()
    {
        // deltaTime 계산
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // FPS 계산
        float fps = 1.0f / deltaTime;

        // Text 업데이트
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }
}
