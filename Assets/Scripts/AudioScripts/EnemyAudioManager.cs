using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{

    public AudioSource footsteps;
    public AudioSource hit;
    void Start()
    {
        footsteps = gameObject.AddComponent<AudioSource>();
        hit = gameObject.AddComponent<AudioSource>();
        footsteps.playOnAwake = true;
        footsteps.loop = true;
        footsteps.pitch = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
