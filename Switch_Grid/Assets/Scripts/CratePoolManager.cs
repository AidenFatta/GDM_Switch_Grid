using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CratePoolManager : MonoBehaviour
{
    public GameObject cratePrefab;

    private ObjectPool cratePool;
    private List<Vector3> crateSpawns;
    private List<GameObject> activeCrates;
    //Crate pool manager for handling crates in the scene. Removing crates is currently unused but will be immplemented in the future for when crates can destroyed by the player.

    void Awake()
    {
        GameObject[] existingCrates = GameObject.FindGameObjectsWithTag("Crate");
        crateSpawns = new List<Vector3>();

        foreach (GameObject crate in existingCrates)
        {
            crateSpawns.Add(crate.transform.position);
            Destroy(crate);
        }

        cratePool = new ObjectPool(cratePrefab, crateSpawns.Count);
        activeCrates = new List<GameObject>();

        SpawnCrates();
    }

    public void SpawnCrates()
    {
        foreach (Vector3 spawn in crateSpawns)
        {
            GameObject crate = cratePool.Get();
            crate.transform.position = spawn;
            crate.SetActive(true);
            activeCrates.Add(crate);
        }
    }

    public void DestroyCrate(GameObject crate)
    {
        cratePool.Return(crate);
        activeCrates.Remove(crate);
    }
}
