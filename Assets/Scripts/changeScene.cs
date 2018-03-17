using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class changeScene : MonoBehaviour {
	// Use this for initialization
	
    public void change(string name)
    {
        SceneManager.LoadScene(name);
    }
}
