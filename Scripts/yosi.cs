using UnityEngine;
using System.Collections;

public class yosi : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                On();
            }
        }
    }

    public void On()
    {
        Application.LoadLevel("Demoscene");

    }
  
}
