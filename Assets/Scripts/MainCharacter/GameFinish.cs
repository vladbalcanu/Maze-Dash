using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    private float winRadius;
    public LayerMask starShipMask;

    // Start is called before the first frame update

    void Start()
    {
        winRadius = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, winRadius, starShipMask);
        if (rangeChecks.Length > 0 )
        {
            Invoke(nameof(FinishGame), 1.0f);
        }
    }

    private void FinishGame()
    {
        SceneManager.LoadScene("Finish");
    }
}
