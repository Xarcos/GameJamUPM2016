using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Sit : ActionsMono
{
    protected override void MakeAnimation()
    {
        player.SetTrigger("sit");
    }
}
