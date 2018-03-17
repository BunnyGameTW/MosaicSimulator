using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class preScene : MonoBehaviour {
    VideoPlayer videoPlayer;
    public string sceneName;
    // Use this for initialization
    void Start () {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void EndReached(VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
