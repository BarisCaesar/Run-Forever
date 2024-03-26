using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChunkManager : MonoBehaviour
{
    public static RoadChunkManager Instance {  get; private set; }

    [Header("Elements")]
    [SerializeField]
    private List<LevelSO> levels;

    private GameObject finishLine;

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GenerateLevel();

        finishLine = GameObject.FindWithTag("Finish");
    }

    private void GenerateLevel()
    {
        int currentLevel = GetCurrentLevel();

        currentLevel = currentLevel % levels.Count;

        List<RoadChunk> chunks = levels[currentLevel].chunks;

        CreateLevel(chunks);
    }

    private void CreateLevel(List<RoadChunk> chunks)
    {     

        Vector3 position = Vector3.zero;

        for (int i = 0; i < chunks.Count; i++)
        {
            RoadChunk chunk = chunks[i];

            if (i > 0)
            {
                position.z += chunk.GetLength() / 2;
            }

            Instantiate(chunk, position, Quaternion.identity, transform);

            position.z += chunk.GetLength() / 2;
        }
    }

    public float GetFinishPositionZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("level", 0);
    }
}
