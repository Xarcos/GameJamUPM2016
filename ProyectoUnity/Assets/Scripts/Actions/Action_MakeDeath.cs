using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_MakeDeath : ActionsMono
{
    protected override void MakeAnimation()
    {
        player.SetTrigger("makeDeath");

    }
}
