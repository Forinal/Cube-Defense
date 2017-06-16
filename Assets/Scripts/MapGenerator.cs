using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    private string[,] map = new string [15,15];
    
    public GameObject mapContainer;
    public GameObject mapCudePrefab;
    public GameObject roadCubePrefab;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        string path = Application.streamingAssetsPath + "/" + "map.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            for (int i = 0; i < 15; i++)
            {
                string line = reader.ReadLine();
                string[] tmp = line.Split(' ');
                for (int j = 0; j < 15; j++)
                {
                    map[i,j] = tmp[j];
                }
            } 
        }

        using (StreamReader reader = new StreamReader(path))
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    GameObject tmpPrefab;
                    Vector3 transform = new Vector3(-(70 - j * 5), 2, - (-70 + i * 5));

                    if (map[i,j].Equals("0")) //Map
                    {
                        tmpPrefab = GameObject.Instantiate(mapCudePrefab, transform, mapContainer.transform.rotation);
                        tmpPrefab.transform.localScale = new Vector3(4, 1, 4);
                        tmpPrefab.transform.parent = mapContainer.transform;
                    }
                }
            }
        }
    }
}
