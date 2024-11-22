using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VictoryEffect : MonoBehaviour
{
    public Volume postProcessVolume; // Post-Processing Volume
    private ColorAdjustments colorAdjustments; // ȭ�� ���� ����
    private Bloom bloom; // �� ���� ȿ��
    private LensDistortion lensDistortion; // ȭ�� �ְ� (����)

    public float flashDuration = 1.5f; // ���� ���� �ð�
    public float maxExposure = 20f; // �ִ� ���
    public float maxBloom = 50f; // �ִ� Bloom ����
    public Color flashColor = new Color(0.53f, 0.81f, 0.98f); // �ϴû� (RGB)

    void Start()
    {
        // Post-Processing ���� �ʱ�ȭ
        if (postProcessVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = 0; // �ʱ� ���� ��
            colorAdjustments.colorFilter.value = Color.white; // �ʱ� ����
        }

        if (postProcessVolume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.intensity.value = 0; // �ʱ� Bloom ��
        }

        if (postProcessVolume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            lensDistortion.intensity.value = 0; // �ʱ� �ְ� ��
        }
    }

    public void TriggerVictoryEffect()
    {
        StartCoroutine(FlashScreen());
    }

    private IEnumerator FlashScreen()
    {
        float elapsedTime = 0;

        // ���� ȿ��: ���� ����
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

        // ���� ȿ��: ���� ����
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

        // ȿ�� �ʱ�ȭ
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







