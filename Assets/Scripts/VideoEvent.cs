using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoEvent : MonoBehaviour {
    VideoPlayer videoPlayer;
    bool isFirst,isSecond;
    public recordMouseState record;
    AudioSource auSource;
    public static float finalScore;
    public mouseControl mouse;
    public static string playSceneName;
    public GameObject replayAni;
    // Use this for initialization
    void Start () {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
        isFirst = true;
        isSecond = false;
        auSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void EndReached(VideoPlayer vp)
    {
        Debug.Log("video end");
        //replay video
        //set replay state
        if (isFirst)
        {
            StartCoroutine(ani());
        }
        else if (isSecond) {
            playSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            finalScore = mouse.score;
            UnityEngine.SceneManagement.SceneManager.LoadScene("score");
        }
    }
    IEnumerator ani()
    {
        replayAni.SetActive(true);//play animation
        mouse.SetBuffer();//set player cant control
        yield return new WaitForSeconds(2.0f);
        afterAni();
    }
    public void afterAni()
    {
        
        isFirst = false;
        isSecond = true;
        //set audio stop      
        mouse.SetReplay();
        record.SetIsReplay();
        videoPlayer.Play();
        auSource.Play();
        replayAni.SetActive(false);
    }
}
