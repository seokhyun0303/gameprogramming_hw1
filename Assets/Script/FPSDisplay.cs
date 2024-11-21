using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime = 0.0f;

    void Update()
    {
        // deltaTime ���
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // FPS ���
        float fps = 1.0f / deltaTime;

        // Text ������Ʈ
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }
}
