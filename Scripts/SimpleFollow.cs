using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {

    Vector3 diff;
    Vector3 diffRotation;

    public GameObject target;
    public float followSpeed;


	// Use this for initialization
	void Start () {
        diff = target.transform.position - transform.position;
        diffRotation = target.transform.eulerAngles - transform.eulerAngles;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followSpeed);
        //transform.eulerAngles = Vector3.Lerp(
        //    transform.eulerAngles,
        //    target.transform.eulerAngles - diffRotation,
        //    Time.deltaTime * followSpeed);

        //best
        //if (Mathf.DeltaAngle(transform.eulerAngles.y, target.transform.eulerAngles.y) < -0.1f)
        //{
        //    transform.Rotate(new Vector3(0f, -5f, 0f));
        //}

        //transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, Time.deltaTime * followSpeed);


        if (Application.isEditor)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, Time.deltaTime * followSpeed);
        }
    }
}
