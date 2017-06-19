using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    private string[,] map = new string [15,15];
    private List<Vector3> wayPoints = new List<Vector3>();
     
    public GameObject mapContainer;
    public GameObject roadContainer;

    public GameObject mapCudePrefab;
    public GameObject roadCubePrefab;

    public GameObject wayPointsContainer;

    void Start()
    {
        GenerateMap();
    }

    /// <summary>
    /// 生成地形
    /// </summary>
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
            int startRow = -1, startCol = -1; // road的起始位置
            int endRow = -1, endCol = -1; // road的结束位置
             
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    GameObject tmpPrefab;
                    
                    if (map[i, j].Equals("0")) //Map
                    {
                        Vector3 transform = new Vector3(-(70 - j * 5), 2, -(-70 + i * 5));

                        //wayPoints.Add(transform);

                        tmpPrefab = GameObject.Instantiate(mapCudePrefab, transform, mapContainer.transform.rotation);
                        tmpPrefab.transform.localScale = new Vector3(4, 1, 4);
                        tmpPrefab.transform.parent = mapContainer.transform;
                         
                        // 横向的road结束，增加road预制体
                        if (startRow != -1)
                        {
                            endRow = i;
                            endCol = j ;
                             
                            // 增加预制体

                            // 大小
                            Vector3 roadScale = new Vector3(4f * (endCol - startCol) + 1 * (endCol - startCol -1), 1, 4f);

                            // 坐标
                            Vector3 roadTransform;

                            if (endCol - startCol > 1)
                            {
                                roadTransform = new Vector3(-(70 - startCol * 5) + roadScale.x * 0.5f - 2, 0, -(-70 + startRow * 5));
                            }
                            else
                            {
                                roadTransform = new Vector3(-(70 - startCol * 5), 0, -(-70 + startRow * 5));
                            }
                            
                            tmpPrefab = GameObject.Instantiate(roadCubePrefab, roadTransform, roadCubePrefab.transform.rotation);

                            // 计算预制体x 
                            // roadCube大小为(4,1,4)  两个road之间距离为1
                            tmpPrefab.transform.localScale = roadScale;     // new Vector3(4f * (endCol - startCol + 1) + 0.5f * (endCol - startCol) , 1, 4f);
                            tmpPrefab.transform.parent = roadContainer.transform;
                             
                            startRow = -1;
                            startCol = -1;
                            endRow = -1;
                            endCol = -1;
                        }
                    }
                    else // road
                    {
                        //Vector3 transform = new Vector3(-(70 - j * 5), 0, -(-70 + i * 5));
                        //tmpPrefab = GameObject.Instantiate(roadCubePrefab, transform, roadCubePrefab.transform.rotation);
                        //tmpPrefab.transform.localScale = new Vector3(4.5f,1,4.5f);

                        // road 开始
                        if (startRow == -1)
                        {
                            //wayPoints.Add(new Vector3(-(70 - j * 5), 2, -(-70 + i * 5)););
                            startRow = i;
                            startCol = j;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 生成道路
    /// </summary>
    public void GenerateRoad(string[,] map)
    {
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                GameObject tmpPrefab;
                Vector3 transform = new Vector3(-(70 - j * 5), 2, -(-70 + i * 5));

                if (map[i, j].Equals("0")) //Map
                {
                    tmpPrefab = GameObject.Instantiate(roadCubePrefab, transform, mapContainer.transform.rotation);
                    tmpPrefab.transform.localScale = new Vector3(4, 1, 4);
                    tmpPrefab.transform.parent = mapContainer.transform;
                }
            }
        }
    }
}
