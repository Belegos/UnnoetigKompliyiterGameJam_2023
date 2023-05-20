using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FootstepSounds : MonoBehaviour
{
    #region Fields
    [SerializeField] AudioSource footstepAudioSource;

    private float footstepDelay = 0.35f;
    private float nextFootstepTime = 0f;

    #endregion

    public void PlayFootstepSounds()
    {
        if (Time.time >= nextFootstepTime)
        {
            footstepAudioSource.Play();
            nextFootstepTime = Time.time + footstepDelay;
        }
    }

    public void StopPlayFootSounds()
    {
        footstepAudioSource.Stop();
    }
}
