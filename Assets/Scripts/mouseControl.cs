using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseControl : MonoBehaviour {

    AudioSource source;
    public AudioClip bii;
    public int type;
    SpriteRenderer pic;
    public Sprite black;
    public Sprite defaltPic;
    public Material defalt;
    public Material mosaic;

    // frame
    public GameObject[] trigger;
    bool[] isUsed;
    public float time_count;
    Vector3 mousePos;
    public float radius;

    // sound
     

    public float score;

    //angel add
    public enum GameState { play, buffer, replay}
    public GameState _gameState;
    public recordMouseState recordMouse;
    public GameObject video;
    [System.Serializable]
    public struct soundCheckPoint
    {
        public float _startTime;
        public float _endTime;
    }
    public soundCheckPoint[] _soundCheckPoints;
    [System.Serializable]
    public struct frameCheckPoint
    {
        public float _startTime;
        public float _endTime;
    }
    public frameCheckPoint[] _frameCheckPoints;
    // Use this for initialization
    void Start () {
        pic = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        source.clip = bii;
        type = 49; // mosaic
        time_count = 0;
        score = 0;
        isUsed = new bool[7];
        //angel
        _gameState = GameState.play;
        recordMouse.recordSpriteState(type, Time.timeSinceLevelLoad);
        InvokeRepeating("callRecordPos", 0.0f, 0.05f);
        //
    }
    void callRecordPos()
    {
        recordMouse.recordPosition(transform.position, time_count);
    }
    // Update is called once per frame
    void Update () {
        time_count += Time.deltaTime;
       
        if (_gameState == GameState.play)//can control
        {
            frameScoring();
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
           
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                type++;
                //
                recordMouse.recordSpriteState(type, Time.timeSinceLevelLoad);
                //
                changeType(type);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (type > 0) type--;
                else type += 250;
                //
                recordMouse.recordSpriteState(type, Time.timeSinceLevelLoad);

                changeType(type);
            }
        }
        else if (_gameState == GameState.replay)// replay time
        {
            //replay pos
            Vector3 pos;
            pos = recordMouse.replayPos();
            if (pos != new Vector3(-999,-999,-999))
            {
                transform.position = pos;
            }
            //replay sprite state
            int _type;
            _type = recordMouse.replaySpriteState();
            if (_type != 999)
            {
                changeType(_type);
            }

            //replay sound state
            float downTime;//down
            downTime = recordMouse.replayBiiDownState();
            if (downTime != 999)
            {
                source.Play();
                video.GetComponent<AudioSource>().volume = 0;
            }
            float upTime;//up
            upTime = recordMouse.replayBiiUpState();
            if (upTime != 999)
            {
                video.GetComponent<AudioSource>().volume = 0.8f;
                source.Stop();
            }
        }

    }
    public void SetBuffer()
    {

        source.Stop();
        video.GetComponent<AudioSource>().volume = 0.8f;
        recordMouse.recordBiiUpState(time_count);
        CancelInvoke("callRecordPos");
        _gameState = GameState.buffer;
    }
    public void SetReplay()
    {     
        _gameState = GameState.replay;
    }
    void SoundScoring()
    {
        bool isAddScore = false;
        foreach (soundCheckPoint item in _soundCheckPoints)
        {
            if(time_count > item._startTime && time_count < item._endTime)
            {
                score += 0.8f * Time.deltaTime;
                isAddScore = true;
                break;
            }
        }
        if (!isAddScore) score -= 0.09f * Time.deltaTime;

        #region useless script
        //if (time_count > 5.6f && time_count < 6.04f)
        //{
        //    score += 0.8f*Time.deltaTime;
        //}
        //else if (time_count > 17.72f && time_count < 17.95f)
        //{
        //    score += 0.8f * Time.deltaTime;
        //}
        //else if (time_count > 32.02f && time_count < 32.37f)
        //{
        //    score += 0.8f * Time.deltaTime;
        //}
        //else if (time_count > 38.13 && time_count < 38.55f)
        //{
        //    score += 0.8f * Time.deltaTime;
        //}
        //else
        //{
        //    score -= 0.09f * Time.deltaTime;
        //}
#endregion
    }
    void frameScoring()
    {
        bool isAddScore = false;
        for(int i = 0;i < _frameCheckPoints.Length; i++)
        {
            int r;
            if ((i == 0 || i == 4) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Spanda") r = 5;
            else r = 1;
            if(time_count > _frameCheckPoints[i]._startTime && !isUsed[i])
            {
                if (isCovering(i, r))
                {
                    //panda
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Spanda")
                    {
                        if (i == 0 || i == 4)
                        {
                            if (type % 3 == 2) score += 0.5f * Time.deltaTime;
                            else if (type % 3 == 1) score += 0.1f * Time.deltaTime;
                        }
                        else
                        {
                            if (type % 3 == 1) score += 0.1f * Time.deltaTime;
                        }
                    }
                    else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Sprince")//prince
                    {
                        if (type % 3 == 1 || type %3 == 2) score += 0.1f * Time.deltaTime;
                    }
                }
                else score -= 0.2f * Time.deltaTime;
                if (time_count > _frameCheckPoints[i]._endTime) isUsed[i] = true;
                isAddScore = true;
                break;
            }
        }
        if (!isAddScore)
        {
            if (type % 3 != 0) score = score - 0.001f * Time.deltaTime;
        }
        #region useless script
        //if (time_count > 7.0f && !isUsed[0])
        //{
        //    if (isCovering(0,5))
        //    {
        //        if (type % 3 == 2) score += 0.5f*Time.deltaTime;
        //        else if (type % 3 == 1) score += 0.1f * Time.deltaTime;
        //    }
        //    else score -= 0.2f * Time.deltaTime;
        //    if (time_count > 12.0f) isUsed[0] = true;
        //}
        //else if (time_count > 15.0f && !isUsed[1])
        //{
        //    if (isCovering(1))
        //    {
        //        if (type % 3 == 1) score += 0.1f * Time.deltaTime;
        //    }
        //    else score -= 0.2f * Time.deltaTime;
        //    if (time_count > 16.0f) isUsed[1] = true;
        //}
        //else if (time_count > 17.0f && !isUsed[2])
        //{
        //    if (isCovering(2))
        //    {
        //        if (type % 3 == 1) score += 0.1f * Time.deltaTime;
        //    }
        //    else score -= 0.2f * Time.deltaTime;
        //    if (time_count > 20.0f) isUsed[2] = true;
        //}
        //else if (time_count > 28.0f && !isUsed[3])
        //{
        //    if (isCovering(3))
        //    {
        //        if (type % 3 == 1) score += 0.1f * Time.deltaTime;
        //    }
        //    else score -= 0.2f * Time.deltaTime;
        //    if (time_count > 31.0f) isUsed[3] = true;
        //}
        //else if (time_count > 32.0f && !isUsed[4])
        //{
        //    if (isCovering(4,5))
        //    {
        //        if (type % 3 == 2) score += 0.5f * Time.deltaTime;
        //        else if (type % 3 == 1) score += 0.1f * Time.deltaTime;
        //    }
        //    else score -= 0.2f * Time.deltaTime;
        //    if (time_count > 33.0f) isUsed[4] = true;
        //}
        //else if (time_count > 33.0f && !isUsed[5])
        //{
        //    if (isCovering(5))
        //    {
        //        if (type % 3 == 1) score += 0.1f * Time.deltaTime;
        //    }
        //    else score -= 0.2f * Time.deltaTime;
        //    if (time_count > 35.0f) isUsed[5] = true;
        //}
        //else if (time_count > 36.0f && !isUsed[6])
        //{
        //    if (isCovering(6))
        //    {
        //        if (type % 3 == 1) score += 0.1f * Time.deltaTime;
        //    }
        //    else score -= 0.2f * Time.deltaTime;
        //    if (time_count > 40.0f) isUsed[6] = true;
        //}
        //else
        //{
        //    if (type % 3 != 0) score = score - 0.001f * Time.deltaTime;
        //}
#endregion //useless script

    }
    private void OnMouseDown()
    {
        if (_gameState == GameState.play)
        {
            source.Play();
            recordMouse.recordBiiDownState(time_count);
            Debug.Log(time_count);
        }
    }

    private void OnMouseDrag()
    {
        if (_gameState == GameState.play)
        {

            video.GetComponent<AudioSource>().volume = 0;
            SoundScoring();
        }
    }

    private void OnMouseUp()
    {
        if (_gameState == GameState.play)
        {
            source.Stop();

            video.GetComponent<AudioSource>().volume = 0.8f;
            recordMouse.recordBiiUpState(time_count);
        }
    }

    void changeType(int type)
    {
        switch (type % 3)
        {
            case 1: //mosaic
                pic.sprite = defaltPic;
                pic.material = mosaic;
                transform.localScale = new Vector3(15, 15, 15);
                break;
            case 2: //black
                pic.sprite = black;
                pic.material = defalt;
                pic.color = new Vector4(0, 0, 0, 1);
                transform.localScale = new Vector3(1,1,1);
                break;
            case 0: //nothing
                pic.color = new Vector4(0, 0, 0, 0);
                pic.material = defalt;
                break;
            default:
                break;
        }
    }
    
    bool isCovering(int n)
    {
        float dx = trigger[n].transform.position.x - mousePos.x;
        float dy = trigger[n].transform.position.y - mousePos.y;
        if (dx * dx + dy * dy < radius) { return true; }
        else return false;
    }
    bool isCovering(int n,int r)
    {
        float dx = trigger[n].transform.position.x - mousePos.x;
        float dy = trigger[n].transform.position.y - mousePos.y;
        if (dx * dx + dy * dy < r) { return true; }
        else return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(trigger[0].transform.position, radius);
        Gizmos.DrawWireSphere(trigger[1].transform.position, radius);
        Gizmos.DrawWireSphere(trigger[2].transform.position, radius);
        Gizmos.DrawWireSphere(trigger[3].transform.position, radius);
        Gizmos.DrawWireSphere(trigger[4].transform.position, radius);
        Gizmos.DrawWireSphere(trigger[5].transform.position, radius);
        Gizmos.DrawWireSphere(trigger[6].transform.position, radius);
    }
}
