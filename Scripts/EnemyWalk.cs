using UnityEngine;
using System.Collections;

public class EnemyWalk : MonoBehaviour {

    public float speed = 3.0f;
    public int disappear = 1000;
    private int count = 0;
    Vector3 RandomDirection;


    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        RandomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
    }

	// Update is called once per frame
	void Update () {
        Vector3 direction = RandomDirection;

        
        direction = direction.normalized;
        transform.position = transform.position + (direction * speed * Time.deltaTime);
        //count++;
        //if (count > disappear)
        //{
        //    Destroy(gameObject);
        //}
	}
    
    void OnCollisionEnter(Collision collision)
    {
        RandomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));

     
    }

}
