using UnityEngine;
using System.Collections;

public class CreateEnemy : MonoBehaviour {

    public GameObject EnemyPrefab;
    public GameObject GodPrefab;
    public GameObject Parent;
    public float OpponentProbability;
    public float EnemyProbability;


	// Update is called once per frame
	void Update () {
        Random.seed = (int)System.DateTime.Now.Ticks;
        if (Random.Range(0.0f, 1.0f) < OpponentProbability) OpponentGenerate();

	}

    void OpponentGenerate()
    {
        float rnd = Random.Range(0.0f, 1.0f);

        Vector3 diff = new Vector3(5, 0, 0);

        if (rnd < EnemyProbability)
        {
            
            GameObject enemy = (GameObject)Instantiate(
           EnemyPrefab,
           transform.position + new Vector3(diff.x * Mathf.Cos(transform.rotation.y)
               - diff.y * Mathf.Sin(transform.rotation.y),
               0,
               diff.x * Mathf.Sin(transform.rotation.y)
               + diff.y * Mathf.Cos(transform.rotation.y)),
           Quaternion.identity);
           
        }

        else
        {
            GameObject god = (GameObject)Instantiate(
            GodPrefab,
            transform.position + new Vector3(diff.x * Mathf.Cos(transform.rotation.y)
                - diff.y * Mathf.Sin(transform.rotation.y),
                0,
                diff.x * Mathf.Sin(transform.rotation.y)
                + diff.y * Mathf.Cos(transform.rotation.y)),
            Quaternion.identity);
        }
        

    }
}
