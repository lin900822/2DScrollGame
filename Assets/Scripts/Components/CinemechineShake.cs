using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CinemechineShakeType
{
    Normal,
    Hard
}

[System.Serializable]
struct CinemechineShakeTypeData
{
    public float ShakeDuration;
    public float ShakeAmplitude;
    public float ShakeFrequency;
}

public class CinemechineShake : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    [Space(10)]
    [Header("Shake Value")]
    [SerializeField] CinemechineShakeTypeData normalShake = default;
    [SerializeField] CinemechineShakeTypeData hardShake = default;

    void Start()
    {
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(CinemechineShakeType type)
    {
        switch (type)
        {
            case CinemechineShakeType.Normal:
                Shake(normalShake.ShakeDuration, normalShake.ShakeAmplitude, normalShake.ShakeFrequency);
                break;
            case CinemechineShakeType.Hard:
                Shake(hardShake.ShakeDuration, hardShake.ShakeAmplitude, hardShake.ShakeFrequency);
                break;
        }
    }

    void Shake(float ShakeDuration = 0.3f, float ShakeAmplitude = 1.2f, float ShakeFrequency = 2.0f)
    {
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
            virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
        }

        Invoke(nameof(resetShake), ShakeDuration);
    }

    void resetShake()
    {
        virtualCameraNoise.m_AmplitudeGain = 0f;
    }
}
