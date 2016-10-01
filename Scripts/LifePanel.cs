using UnityEngine;
using System.Collections;

public class LifePanel : MonoBehaviour {

    public GameObject[] icons;
	public GameObject GameOver;

	void Start(){
		GameOver.SetActive(false);
	}


    public void UpdateLife(int life)
    {
        for(int i = 0;i < icons.Length; i++)
        {
            if (i < life) icons[i].SetActive(true);
            else icons[i].SetActive(false);
        }
		if(life == 0){
            
            GameOver.SetActive(true); 
            StartCoroutine("sleep");
			
		}
    }
	public void SceneLoad(){ 
 		Application.LoadLevel ("DemoScene"); 
 
 	} 
    
	IEnumerator sleep(){
        yield return new WaitForSeconds(6);  //10秒待つ
		SceneLoad();
	}


}
