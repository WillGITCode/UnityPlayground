using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATBBlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public float spawnRate = 1.5f;
    public int waveSize = 10;
    public int blockStartSpeed = 5;
    public Vector2 sizes = new Vector2(0.4f, 1.7f);

    int blockSpawnedThisRound = 0; 
    float nextSpawnTime;
    Vector2 screenHalfSizeInWorldUnits;
    void Start()
    {
        screenHalfSizeInWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBlock();
    }

    void SpawnBlock()
    {
        if (Time.time > nextSpawnTime)
        {   
            blockSpawnedThisRound++;
            CheckDiff();
            nextSpawnTime = Time.time + spawnRate;
            float scale = Random.Range(sizes.x, sizes.y);
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeInWorldUnits.x, screenHalfSizeInWorldUnits.x), screenHalfSizeInWorldUnits.y + scale/2f);
            FallAngle fallAngle = (FallAngle)Random.Range(0, 3);

            GameObject newBlock = (GameObject)Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
            newBlock.transform.localScale = new Vector3(scale, scale, 1);
            var block = newBlock.GetComponent<ATBBlock>();
            block.fallAngle = fallAngle;
            block.speed = blockStartSpeed;
        }
    }

    void CheckDiff(){
        if(blockSpawnedThisRound >= waveSize && spawnRate > 0.3f){
            spawnRate -= 0.1f;
            blockStartSpeed++;
            blockSpawnedThisRound = 0;
        }
    }
}

public enum FallAngle
{
    left,
    right,
    none
};
