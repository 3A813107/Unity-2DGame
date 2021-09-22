using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_Shake : MonoBehaviour
{
    public static CM_Shake Instance {get;private set;}
    private CinemachineVirtualCamera CM_VirtualCamera;
    private float shakeTimer;
    private void Awake()
    {
        Instance=this;
        CM_VirtualCamera=GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity,float time)
    {
        CinemachineBasicMultiChannelPerlin CM_BasicMultiChannelPerlin=
        CM_VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        CM_BasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer=time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin CM_BasicMultiChannelPerlin=
                CM_VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                CM_BasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
