using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Bark : ActionsMono
{
    [SerializeField] AudioSource audio;

    protected override void MakeAnimation()
    {
        player.SetTrigger("bark");
        audio.Play();
    }
}
