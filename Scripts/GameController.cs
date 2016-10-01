using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public NejikoController nejiko;
    public Text scoreLabel;
    public LifePanel lifePanel;

    public void Update()
    {
        int score = CalcScore();
        scoreLabel.text = "Score : " + score + "";

        lifePanel.UpdateLife(nejiko.Life());
    }

    int CalcScore()
    {
        return (int)nejiko.transform.position.z;
    }
}
