using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogAvatar : MonoBehaviour
{
    [System.Serializable]
    public class status
    {
        public Sprite Avatar;
        public int minHp;
    }

    public bool loseHp(int loss)
    {
        currentHP = Mathf.Max(0, currentHP - loss);
        if (currentHP == 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().EndGame();
            return false;
        }

        updateStateSprite();
        return true;
    }

    public void gainHp(int gain)
    {
        currentHP = Mathf.Min(currentHP + gain, MAX_LIFE);
        updateStateSprite();
    }
    public void setBoost(bool newBoost)
    {
        isBoosted = newBoost;
        if (isBoosted)
        {
            dogAvatar.GetComponent<Animation>().Play();
        }
        else {
            dogAvatar.GetComponent<Animation>().Stop();
        }
        
        updateStateSprite();
    }
    void updateStateSprite()
    {
        if (isBoosted)
        {
            dogAvatar.sprite = boostAvatar;
        }
        else
        {
            for (int i = 0; i < dogStates.Length; ++i)
            {
                if (currentHP < dogStates[i].minHp)
                {
                    dogAvatar.sprite = dogStates[i-1].Avatar;
                    return;
                }
            }

            dogAvatar.sprite = dogStates[dogStates.Length - 1].Avatar;
        }
    }

    [SerializeField] status[] dogStates;

    [SerializeField] Image dogAvatar;
    [SerializeField] Sprite boostAvatar;

    float currentHP;

    bool isBoosted;

    public float MAX_LIFE;
    // Use this for initialization
    void Start()
    {
        currentHP = MAX_LIFE;

        updateStateSprite();
    }
}
