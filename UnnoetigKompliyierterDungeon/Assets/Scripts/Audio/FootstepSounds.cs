using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FootstepSounds : MonoBehaviour
{
    #region Fields
    GameObject _player;
    [SerializeField] Rigidbody _rb;
    [SerializeField] AudioSource footstepAudioSource;
    [SerializeField] AudioClip[] footstepSounds;

    #endregion
    private void Start()
    {
        _player = GameObject.Find("Player");
        _rb = _player.GetComponent<Rigidbody>();
    }
    void PerformFootsteps()
    {
        if(footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            footstepAudioSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }

    void AddFootstepSounds()
    {
        footstepSounds = new AudioClip[6];

        footstepSounds[0] = Resources.Load<AudioClip>("Audio/Footsteps_Rock/Footsteps_Rock_Walk/Footsteps_Rock_Walk_01");
        footstepSounds[1] = Resources.Load<AudioClip>("Audio/Footsteps_Gravel/Footsteps_Gravel_Walk/Footsteps_Gravel_Walk_01");
        footstepSounds[2] = Resources.Load<AudioClip>("Audio/Footsteps_Rock/Footsteps_Rock_Walk/Footsteps_Rock_Walk_05");
        footstepSounds[3] = Resources.Load<AudioClip>("Audio/Footsteps_Gravel/Footsteps_Gravel_Walk/Footsteps_Gravel_Walk_05");
        footstepSounds[4] = Resources.Load<AudioClip>("Audio/Footsteps_Rock/Footsteps_Rock_Walk/Footsteps_Rock_Walk_09");
        footstepSounds[5] = Resources.Load<AudioClip>("Audio/Footsteps_Gravel/Footsteps_Gravel_Walk/Footsteps_Gravel_Walk_09");

    }
}
