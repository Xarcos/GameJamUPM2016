using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class MgrOwnerGestures : ScriptableObject {

    public Sprite[] _handGestures;
    public Sprite[] _faceGestures;
    public AudioClip[] _voiceGestures;

    static MgrOwnerGestures instance;

    static MgrOwnerGestures Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<MgrOwnerGestures>("gestures");
            return instance;
        }
    }

    public static int voicesCount()
    {
        return Instance._voiceGestures.Length;
    }
    public static int handsCount()
    {
        return Instance._handGestures.Length;
    }
    public static int facesCount()
    {
        return Instance._faceGestures.Length;
    }
    public static Sprite getHandGesture(int i)
    {
        return Instance._handGestures[i];
    }
    public static Sprite getFaceGesture(int i)
    {
        return Instance._faceGestures[i];
    }
    public static AudioClip getVoiceGesture(int i)
    {
        return Instance._voiceGestures[i];
    }
}
