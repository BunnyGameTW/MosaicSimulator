using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour {
    float finalScore;
    string playScene;
    public GameObject[] pic;
	// Use this for initialization
	void Start () {
        finalScore = VideoEvent.finalScore;
        playScene = VideoEvent.playSceneName;
        if(playScene == "Sprince")
        {
            if (finalScore < -2)
            {
                pic[3].SetActive(true);
            }
            else 
            {
                pic[4].SetActive(true);
            }
        }
        else if(playScene == "Spanda")
        {
           if (finalScore > 5)
            {
                pic[1].SetActive(true);
            }
            else if (finalScore > 0)
            {
                pic[0].SetActive(true);
            }           
            else
            {
                pic[2].SetActive(true);
            }
        }
       
	}
	
}
