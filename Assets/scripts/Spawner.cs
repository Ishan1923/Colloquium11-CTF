using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private List<string> ids = new List<string>();
    private List<string> questions = new List<string>();


    [SerializeField] public GameObject checkpointPrefab;
    public LayerMask terrainLayer;
    public int spawnRange = 100;
    
    void Spawn(int i){
        Vector3 randomPosition = new Vector3
        (
            Random.Range(-spawnRange, spawnRange),
            0,
            Random.Range(-spawnRange, spawnRange)
        );

        RaycastHit hit;
        if(Physics.Raycast(randomPosition + Vector3.up * 50, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
            randomPosition.y = hit.point.y + 0.5f;

            GameObject obj = Instantiate(checkpointPrefab, randomPosition, Quaternion.identity);

            QuestionData data = obj.AddComponent<QuestionData>();
            data.id = ids[i];
            data.question = questions[i];
        }
    }


    void Start()
    {
        TextAsset tectFile = Resources.Load<TextAsset>("Questions");
        if(tectFile != null){
            string[] lines = tectFile.text.Split('\n');
            for (int i = 0; i < lines.Length; i++){

                if(string.IsNullOrWhiteSpace(lines[i])){continue;}

                string[] values = lines[i].Split(',');
                
                if(values.Length < 2){continue;}

                ids.Add(values[0].Trim());
                questions.Add(values[1].Trim());

                Spawn(i);
            }
        }
        else{
            Debug.LogError("CSV file not found!");
        }
    }

    
}
