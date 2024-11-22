using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VictoryEffect : MonoBehaviour
{
    public Volume postProcessVolume; // Post-Processing Volume
    private ColorAdjustments colorAdjustments; // 화면 색상 조정
    private Bloom bloom; // 빛 퍼짐 효과
    private LensDistortion lensDistortion; // 화면 왜곡 (선택)

    public float flashDuration = 1.5f; // 섬광 지속 시간
    public float maxExposure = 20f; // 최대 밝기
    public float maxBloom = 50f; // 최대 Bloom 강도
    public Color flashColor = new Color(0.53f, 0.81f, 0.98f); // 하늘색 (RGB)

    void Start()
    {
        // Post-Processing 설정 초기화
        if (postProcessVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = 0; // 초기 노출 값
            colorAdjustments.colorFilter.value = Color.white; // 초기 색상
        }

        if (postProcessVolume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.intensity.value = 0; // 초기 Bloom 값
        }

        if (postProcessVolume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            lensDistortion.intensity.value = 0; // 초기 왜곡 값
        }
    }

    public void TriggerVictoryEffect()
    {
        StartCoroutine(FlashScreen());
    }

    private IEnumerator FlashScreen()
    {
        float elapsedTime = 0;

        // 섬광 효과: 강도 증가
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;

            if (colorAdjustments != null)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(0, maxExposure, elapsedTime / (flashDuration / 2));
                colorAdjustments.colorFilter.value = Color.Lerp(Color.white, flashColor, elapsedTime / (flashDuration / 2));
            }

            if (bloom != null)
                bloom.intensity.value = Mathf.Lerp(0, maxBloom, elapsedTime / (flashDuration / 2));

            if (lensDistortion != null)
                lensDistortion.intensity.value = Mathf.Lerp(0, -0.2f, elapsedTime / (flashDuration / 2));

            yield return null;
        }

        elapsedTime = 0;

        // 섬광 효과: 강도 감소
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;

            if (colorAdjustments != null)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(maxExposure, 0, elapsedTime / (flashDuration / 2));
                colorAdjustments.colorFilter.value = Color.Lerp(flashColor, Color.white, elapsedTime / (flashDuration / 2));
            }

            if (bloom != null)
                bloom.intensity.value = Mathf.Lerp(maxBloom, 0, elapsedTime / (flashDuration / 2));

            if (lensDistortion != null)
                lensDistortion.intensity.value = Mathf.Lerp(-0.2f, 0, elapsedTime / (flashDuration / 2));

            yield return null;
        }

        // 효과 초기화
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = 0;
            colorAdjustments.colorFilter.value = Color.white;
        }

        if (bloom != null)
            bloom.intensity.value = 0;

        if (lensDistortion != null)
            lensDistortion.intensity.value = 0;
    }
}







