using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance => instance;

    public System.Action LevelCompleted;

    [Space]
    [SerializeField] LevelInfoAsset levelInfoAsset;

    [Space]
    [SerializeField] Slider slider;

    [Space]
    [SerializeField] private Transform blockContainer;
    [SerializeField]
    private Transform blockContainer2;

    private static LevelManager instance;

    int currentLevelIndex = 0;

    BlockSpawner blockSpawner = new BlockSpawner();
    
    List<BlockController> createdBlocks = new List<BlockController>();
    List<BlockController> collectedBlocks = new List<BlockController>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        blockSpawner = GetComponent<BlockSpawner>();
    }

    public bool HandleCreateNextLevel()
    {
        if (createdBlocks.Count > 0)
        {
            for (int i = 0; i < createdBlocks.Count; i++)
            {
                Destroy(createdBlocks[i]);
            }
        }

        ++currentLevelIndex;

        if (levelInfoAsset.levelInfos.Count >= currentLevelIndex)
        {
            CreateNextLevel();
            return true;
        }

        return false;
    }

    void CreateNextLevel()
    {

        int size = blockSpawner.CreateBlockFromImage(levelInfoAsset.levelInfos[currentLevelIndex - 1], blockContainer, levelInfoAsset.levelInfos[currentLevelIndex - 1].baseObj, blockContainer2);
        slider.maxValue = size;
    }

    public void OnBlockCreated(BlockController blockController)
    {
        createdBlocks.Add(blockController);

    }
    

    public void OnBlockCollected(BlockController blockController)
    {
        
        collectedBlocks.Add(blockController);
        
        slider.value = collectedBlocks.Count;
        Debug.Log($"{slider.maxValue} / {slider.value} <- Collected Block Count");
        if (collectedBlocks.Count == createdBlocks.Count)
        {
            LevelCompleted?.Invoke();
        }
    }

    



}
