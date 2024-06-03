using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{

    public AudioSource footsteps;
    public AudioSource hit;
    public EnemyBehavior enemyBehavior;
    public bool justHit;
    public bool footstepsPlaying;
    void Start()
    {
        footsteps = gameObject.GetComponent<AudioSource>();
        hit = gameObject.GetComponent<AudioSource>();
        footsteps.playOnAwake = true;
        footsteps.loop = true;
        footsteps.pitch = 2.0f;
        enemyBehavior = gameObject.GetComponent<EnemyBehavior>();
        justHit = false;
        footstepsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBehavior.isAttacking && !justHit)
        {
            justHit = true;
            footstepsPlaying = false;
            hit.Play();
            footsteps.Stop();
            Invoke(nameof(ResetBools), 3.0f);
        }

    }

    private void ResetBools()
    {
        justHit = false;
        footstepsPlaying = true;
        footsteps.Play();
    }

}
