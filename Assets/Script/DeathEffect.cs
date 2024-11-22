using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DeathEffect : MonoBehaviour
{
    public Image redOverlay; // ���� ȭ�� UI Image
    public float fadeSpeed = 2f; // ���̵� �ӵ�
    public float targetAlpha = 0.5f; // ���� Alpha ��

    public Volume postProcessVolume; // Post-Processing Volume
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        // �ʱ� ���� ����
        redOverlay.color = new Color(1, 0, 0, 0); // �ʱ� ����
        if (postProcessVolume.profile.TryGet<ChromaticAberration>(out chromaticAberration))
        {
            chromaticAberration.intensity.value = 0; // �ʱ⿡�� ȿ�� ��Ȱ��ȭ
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

        // Alpha ���� Chromatic Aberration ������ Ȱ��ȭ
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

        // ���� �ð�
        yield return new WaitForSeconds(1f);

        // Chromatic Aberration ��Ȱ��ȭ
        chromaticIntensity = 1;
        while (chromaticIntensity > 0)
        {
            chromaticIntensity -= Time.deltaTime * fadeSpeed;
            chromaticAberration.intensity.value = Mathf.Clamp(chromaticIntensity, 0, 1);
            yield return null;
        }
    }
}


