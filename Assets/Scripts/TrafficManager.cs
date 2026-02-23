using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] Transform[] lane;
    [SerializeField] GameObject[] trafficVehicle;
    [SerializeField] CarController carController;

    [SerializeField] float minSpawnTime = 30f;
    [SerializeField] float maxSpawnTime = 60f;
    [SerializeField] float minSpawnDistance = 10f;

    private float dynamicTimer = 2f;

    void Start()
    {
        StartCoroutine(TrafficSpawner());
    }

    IEnumerator TrafficSpawner()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (carController.CarSpeed() > 20f)
            {
                dynamicTimer = Random.Range(minSpawnTime, maxSpawnTime) /
                               Mathf.Max(carController.CarSpeed(), 1f);

                TrySpawnTrafficVehicle();
            }

            yield return new WaitForSeconds(dynamicTimer);
        }
    }

    void TrySpawnTrafficVehicle()
    {
        int randomLaneIndex = Random.Range(0, lane.Length);
        Vector3 spawnPos = lane[randomLaneIndex].position;

        Collider[] hits = Physics.OverlapSphere(spawnPos, minSpawnDistance);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Traffic"))
                return; // lane occupied - skip spawn
        }

        int randomTrafficVehicleIndex = Random.Range(0, trafficVehicle.Length);

        Instantiate(
            trafficVehicle[randomTrafficVehicleIndex],
            spawnPos,
            Quaternion.identity
        );
    }
}