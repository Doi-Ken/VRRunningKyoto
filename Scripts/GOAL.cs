using UnityEngine;
using System.Collections;

public class GOAL : MonoBehaviour {
	GUIStyle style;
	public GameObject Finish;

		int width = Screen.width;
		int height = Screen.height;
	// Use this for initialization
	void Start () {
		Finish.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	/*void OnGUI(){

		int width1=50;
		int height1=50;

		style = new GUIStyle();
		style.normal.textColor = Color.red;
		style.fontSize = 30; 

		GUI.Label(new Rect((width-width1)/2, 50, 100, 30), "Clear!", style);

	}*/

   void OnCollisionEnter(Collision collision)
    {

	if(collision.gameObject.tag == "Player")
		//OnGUI();
		//GameObject textmessage = Instantiate<GameObject>(Finish);
		
		Finish.SetActive(true);

    }

}
