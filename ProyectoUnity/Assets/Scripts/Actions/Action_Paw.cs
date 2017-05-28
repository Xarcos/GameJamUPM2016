using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Paw : ActionsMono
{
    [SerializeField]
    AudioSource audio;

    protected override void MakeAnimation()
    {
        player.SetTrigger("paw");
        audio.Play();
    }
}
