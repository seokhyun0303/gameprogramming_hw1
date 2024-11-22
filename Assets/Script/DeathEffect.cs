using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DeathEffect : MonoBehaviour
{
    public Image redOverlay; // 붉은 화면 UI Image
    public float fadeSpeed = 2f; // 페이드 속도
    public float targetAlpha = 0.5f; // 최종 Alpha 값

    public Volume postProcessVolume; // Post-Processing Volume
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        // 초기 상태 설정
        redOverlay.color = new Color(1, 0, 0, 0); // 초기 투명
        if (postProcessVolume.profile.TryGet<ChromaticAberration>(out chromaticAberration))
        {
            chromaticAberration.intensity.value = 0; // 초기에는 효과 비활성화
        }
    }

    public void TriggerDeathEffect()
    {
        StartCoroutine(FadeToRedWithChromaticAberration());
    }

    private IEnumerator FadeToRedWithChromaticAberration()
    {
        float alpha = 0;
        float chromaticIntensity = 0;

        // Alpha 값과 Chromatic Aberration 점진적 활성화
        while (alpha < targetAlpha || chromaticIntensity < 1)
        {
            if (alpha < targetAlpha)
            {
                alpha += Time.deltaTime * fadeSpeed;
                redOverlay.color = new Color(1, 0, 0, Mathf.Clamp(alpha, 0, targetAlpha));
            }

            if (chromaticIntensity < 1)
            {
                chromaticIntensity += Time.deltaTime * fadeSpeed;
                chromaticAberration.intensity.value = Mathf.Clamp(chromaticIntensity, 0, 1);
            }

            yield return null;
        }

        // 유지 시간
        yield return new WaitForSeconds(1f);

        // Chromatic Aberration 비활성화
        chromaticIntensity = 1;
        while (chromaticIntensity > 0)
        {
            chromaticIntensity -= Time.deltaTime * fadeSpeed;
            chromaticAberration.intensity.value = Mathf.Clamp(chromaticIntensity, 0, 1);
            yield return null;
        }
    }
}


