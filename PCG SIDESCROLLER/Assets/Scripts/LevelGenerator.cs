using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] int levelSize;
    [SerializeField] int offsetX = 4;
    [SerializeField] Vector2 perlinSample;
    [SerializeField] float noiseGranularity;
    [SerializeField] float noiseSensitivity;


    [SerializeField] Transform startPoint;


    [SerializeField] SliceHolder sliceHolder;
    
    [SerializeField] GameObject endSlice;

    Dictionary<int, ValuePair> positions = new Dictionary<int, ValuePair>();
    Vector2 startingPerlinSamplePos;

    void Start()
    {
        startingPerlinSamplePos = perlinSample;
        GenerateNoise();
        GenerateLevel();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Reset();
    }

    //Fills upp position with either "dead" or "alive" status for the spawns
    private void GenerateNoise()
    {
        perlinSample = startingPerlinSamplePos;
        int counter;
        for (counter = 0; counter <= levelSize; counter++)
        {
            float sample = Mathf.PerlinNoise(perlinSample.x, perlinSample.y);
            sample = sample < noiseSensitivity ? 0 : 1;
            positions.Add(counter, new ValuePair((int)sample, new Vector2(startPoint.position.x + (offsetX * counter), 0)));
            perlinSample.x += noiseGranularity;
        }
        for(int i = 0; i < positions.Count; i++)
        {
            Debug.Log(positions[i].life);
        }
        
    }

    //Places prefabs of Splices based on the values set in GenerateNoise
    private void GenerateLevel()
    {
        int counter;
        for(counter = 0; counter <= levelSize; counter++)
        {
            Instantiate(sliceHolder.GetSlice(positions[counter].life), positions[counter].position, transform.rotation, transform);
        }

        Instantiate(endSlice, new Vector2(startPoint.position.x + (offsetX * counter), startPoint.position.y), transform.rotation, transform);
        Debug.Log("LEVEL DONE");
    }

    private void Reset()
    {
        positions.Clear();

        var children = transform.GetComponentInChildren<Transform>();
        foreach (Transform child in children)
        {
            if(!child.GetComponent<SliceHolder>())
                Destroy(child.gameObject);
        }

        GenerateNoise();
        GenerateLevel();
    }
}

public class ValuePair
{
    public int life;
    public Vector3 position;

    public ValuePair(int i, Vector3 v) {life = i; position = v;}
}
