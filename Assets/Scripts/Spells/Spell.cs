using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    //Debug. This is a button until we expand it.
    private void OnMouseDown ()
    {
        PrepareToCast();
    }

    //When the spell is selected. Don't immediately cast by default, as additional inputs may be required.
    public virtual void PrepareToCast() {

    }

    //Cast the spell with the given inputs
    public virtual void Cast()
    {

    }

}
