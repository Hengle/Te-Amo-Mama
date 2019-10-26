using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float shakeDuration = 0.5f;    //Time that the shake will last
    [SerializeField] private float shakeAmplitude = 1.2f;   //Parameter on the Cinemachine Noise Profile
    [SerializeField] private float shakeFrequency = 2.0f;   //Parameter on the Cinemachine Noise Profile

    private float shakeElapsedTime = 0f;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;


    // Start is called before the first frame update
    void Start()
    {
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShakeCamera();
    }

    void ShakeCamera()
    {
        //Triggers the Camera Shake
        if (GameManager.instance.henIsCharging)
        {
            shakeElapsedTime = shakeDuration;
        }

        // If the Cinemachine component is not set, avoid update
        if (virtualCamera != null && virtualCameraNoise != null)
        {
            //If Camera Shake is still playing
            if (shakeElapsedTime > 0)
            {
                //Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                //Update Shake Timer
                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                //If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }
}
