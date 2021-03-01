using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : SpellEffect
{
    public virtual bool ActAsObstacle()
    {
        return true;
    }
}
