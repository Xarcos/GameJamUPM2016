using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OwnerGestures
{
    public Sprite hands;
    public Sprite face;
    public AudioClip voice;
}

public class Owner : MonoBehaviour {

    [SerializeField] OwnerGestures _BarkGestures;
    [SerializeField] OwnerGestures _MakeDeathGestures;
    [SerializeField] OwnerGestures _PawGestures;
    [SerializeField] OwnerGestures _SitGestures;

    [SerializeField] OwnerGestures _SadReaction;
    [SerializeField] OwnerGestures _HappyReaction;


    [SerializeField] bool RandomGestures;

	[SerializeField] UnityEngine.UI.Image m_faceGesture;
    [SerializeField] UnityEngine.UI.Image m_handsGesture;

    AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();

        if (RandomGestures)
        {
            var choosenGesturesInt = GenerateRandom(4, 0, MgrOwnerGestures.facesCount());
            AsignGestures(choosenGesturesInt[0], _BarkGestures);
            AsignGestures(choosenGesturesInt[1], _MakeDeathGestures);
            AsignGestures(choosenGesturesInt[2], _PawGestures);
            AsignGestures(choosenGesturesInt[3], _SitGestures);
        }
    }

    void AsignGestures(int i, OwnerGestures owg)
    {
        owg.face = MgrOwnerGestures.getFaceGesture(i);
        owg.hands = MgrOwnerGestures.getHandGesture(i);
        owg.voice = MgrOwnerGestures.getVoiceGesture(i);
    }

    public void MakeGesture(Actions action)
    {
        var gestureSprites = getGesture(action);
        m_handsGesture.sprite = gestureSprites.hands;
        m_faceGesture.sprite = gestureSprites.face;
        _audio.clip = gestureSprites.voice;
        _audio.Play();
    }

    public void HappyReaction()
    {
        m_handsGesture.sprite = _HappyReaction.hands;
        m_faceGesture.sprite = _HappyReaction.face;
        _audio.clip = _HappyReaction.voice;
        _audio.Play();
    }

    public void AngryReaction()
    {
        m_handsGesture.sprite = _SadReaction.hands;
        m_faceGesture.sprite = _SadReaction.face;
        _audio.clip = _SadReaction.voice;
        _audio.Play();
    }

    static int[] GenerateRandom(int count, int min, int max)
    {
        // generate count random values.
        HashSet<int> candidates = new HashSet<int>();
        while (candidates.Count < count)
        {
            // May strike a duplicate.
            candidates.Add(Random.Range(min,max));
        }

        // load them in to a list.
        List<int> result = new List<int>();
        result.AddRange(candidates);

        // shuffle the results:
        int i = result.Count;
        while (i > 1)
        {
            --i;
            int k = Random.Range(0, i + 1);
            int value = result[k];
            result[k] = result[i];
            result[i] = value;
        }
        return result.ToArray();
    }

    public OwnerGestures getGesture(Actions action)
    {
        switch (action)
        {
            case Actions.Bark:
                return _BarkGestures;
            case Actions.MakeDeath:
                return _MakeDeathGestures;
            case Actions.Paw:
                return _PawGestures;
            case Actions.Sit:
                return _SitGestures;
            default:
                throw new System.Exception(action.ToString() + " do not exist");
        }
    }
}
