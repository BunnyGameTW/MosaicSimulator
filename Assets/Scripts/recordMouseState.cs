using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordMouseState : MonoBehaviour {
    public struct SpriteState
    {
        public int _type;
        public float _time;
    }
   public struct PosState
    {
        public Vector3 _pos;
        public float _time;
    }
    List<SpriteState> _spriteList;
    List<float> _biiDownList, _biiUpList;
    List<PosState> _posList;
   float  nowTime;
    int spriteIndex, biiDownIndex, biiUpIndex, posIndex;
    public bool isReplay;
    bool spriteDoWork, biiDownDoWork, biiUpDoWork,posDoWork;

    // Use this for initialization
    void Start () {
        _spriteList = new List<SpriteState>();
        _biiDownList = new List<float>();
        _biiUpList = new List<float>();
        _posList = new List<PosState>();
         nowTime = 0;
        posIndex = spriteIndex = biiDownIndex = biiUpIndex = 0;
        isReplay = false;
        posDoWork = spriteDoWork = biiDownDoWork = biiUpDoWork = true;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (isReplay) replay();

    }
    
    //record pos
  public  void recordPosition(Vector3 pos, float time)
    {
        PosState posState = new PosState();
        posState._pos = pos;
        posState._time = time;
        _posList.Add(posState);
    }
    public Vector3 replayPos()
    {
        if (isReplay)
        {
            if (posIndex == _posList.Count) posDoWork = false;//如果串列到底 就不做事
            if (posDoWork && nowTime > _posList[posIndex]._time)
            {
                posIndex++;
                return _posList[posIndex - 1]._pos;
            }
        }
        return new Vector3(-999,-999,-999);

    }

    //切換圖片時間、圖片狀態
    public void recordSpriteState(int spriteType, float time)
    {
        SpriteState spState = new SpriteState();
        spState._type = spriteType;
        spState._time = time;
        _spriteList.Add(spState);
    }
    //按下逼逼的時間
    public void recordBiiDownState(float time)
    {
        _biiDownList.Add(time);
    }
    //放開逼逼的時間
    public void recordBiiUpState(float time)
    {
        _biiUpList.Add(time);
    }
    void replay()
    {
        nowTime += Time.deltaTime;

        
    }
    public int replaySpriteState()
    {
        if (isReplay)
        {
            if (spriteIndex == _spriteList.Count ) spriteDoWork = false;//如果串列到底 就不做事
            if (spriteDoWork && nowTime > _spriteList[spriteIndex]._time)
            {
                spriteIndex++;
                return _spriteList[spriteIndex - 1]._type;
            }   
        }
        return 999;
    }

    public void SetIsReplay()
    {
        isReplay = true;
    }
    public float replayBiiUpState()
    {
        if (isReplay)
        {
            if (biiUpIndex == _biiUpList.Count) biiUpDoWork = false;//如果串列到底 就不做事
            if (biiUpDoWork && nowTime > _biiUpList[biiUpIndex])
            {
                biiUpIndex++;
                return _biiUpList[biiUpIndex - 1];
            }
        }
        return 999;
    }
    public float replayBiiDownState()
    {
        if (isReplay)
        {
            if (biiDownIndex == _biiDownList.Count) biiDownDoWork = false;//如果串列到底 就不做事
            if (biiDownDoWork && nowTime > _biiDownList[biiDownIndex])
            {
                biiDownIndex++;
                return _biiDownList[biiDownIndex - 1];
            }
        }
        return 999;
    }
}
