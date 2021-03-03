using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    Vector3 blockPos = Vector3.zero;
    Color color;
    

    public int CreateBlockFromImage(LevelInfo levelInfo, Transform transform, GameObject obje, Transform blockContainer2)
    {
        List<GameObject> createdCubes = new List<GameObject>();

        for (int x = 0; x < levelInfo.sprite.texture.width; x++)
        {
            for (int y = 0; y < levelInfo.sprite.texture.height; y++)
            {
                color = levelInfo.sprite.texture.GetPixel(x, y);


                if (color.a == 0)
                {
                    continue;
                }

                blockPos = new Vector3(
                    levelInfo.size * (x - (levelInfo.sprite.texture.width * .5f)),
                    levelInfo.size * .5f,
                    levelInfo.size * (y - (levelInfo.sprite.texture.height * .5f)));

                GameObject cubeObj = Instantiate(obje, transform);
                cubeObj.transform.localPosition = blockPos;


                cubeObj.GetComponent<Renderer>().material.color = color;
                cubeObj.transform.localScale = Vector3.one * levelInfo.size;


                createdCubes.Add(cubeObj);
            }
        }
        CreatedCubes(levelInfo, blockContainer2, createdCubes.Count);
       

        return createdCubes.Count;
    }

    public void CreatedCubes(LevelInfo levelInfo, Transform transform, int cubeCount)
    {
        int i = 0;
        for (int y = 0; y < 8; y++)
        {
            for (int z = -8; z < 8; z++)
            {
                for (int x = -8; x < 8; x++)
                {

                    blockPos = new Vector3(
                                        x * levelInfo.size,
                                        y * levelInfo.size + 0.05f,
                                        z * levelInfo.size);
                    GameObject cubeObj = Instantiate(levelInfo.baseObj2, transform);
                    cubeObj.transform.localPosition = blockPos;
                    cubeObj.transform.parent = transform;
                    cubeObj.GetComponent<MeshRenderer>().material.color = Color.blue;/*new Color32(
                                                                                 (byte)UnityEngine.Random.Range(0, 255),         // R
                                                                                 (byte)UnityEngine.Random.Range(0, 255),        // G
                                                                                 (byte)UnityEngine.Random.Range(0, 255),       // B
                                                                                 (byte)UnityEngine.Random.Range(0, 255));     // A  */
                    i++;

                    if (i > cubeCount - 1)
                    {
                        return;
                    }
                }


            }

        }
    }
}
