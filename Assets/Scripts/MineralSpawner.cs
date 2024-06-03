using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MineralSpawner : MonoBehaviour
{
    public GameObject spawnableObject;
    public int numberOfMinerals;
    private int curentNumberOfMinerals;
    public LayerMask obstructionMask;
    public LayerMask interactableMask;
    public float collideRadius = 1.0f;
    void Start()
    {
        numberOfMinerals = 40;
        curentNumberOfMinerals = 0;
        StartCoroutine(SpawnMinerals());
  
    }

    private IEnumerator SpawnMinerals()
    {
        while(curentNumberOfMinerals < numberOfMinerals)
        {
            float randomX = Random.Range(-93, 75);
            float randomZ = Random.Range( -95 ,1);
            Vector3 position = new Vector3(randomX, 0.2f, randomZ);
            Collider[] collidingObstructions = Physics.OverlapSphere(transform.position, collideRadius, obstructionMask);
            Collider[] collidingInteractables = Physics.OverlapSphere(transform.position, collideRadius, interactableMask);
            if (collidingObstructions.Length == 0 && collidingInteractables.Length == 0) 
            {
                Instantiate(spawnableObject, position, Quaternion.identity);
                curentNumberOfMinerals++;
            }
            yield return null;
        }
    }

}
