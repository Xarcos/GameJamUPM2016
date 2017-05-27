using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Paw : ActionsMono
{
    protected override void MakeAnimation()
    {
        player.SetTrigger("paw");

    }
}
