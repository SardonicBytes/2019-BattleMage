using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    int ownership;
    public int Ownership
    {
        get { return ownership; }
        private set { ownership = value; }
    }
    bool isCarried;

    /// <summary>
    /// Does this peice have ownership? If so, Is it mine?
    /// </summary>
    /// <param name="myTeam"></param>
    /// <returns></returns>
    public bool isMine(int myTeam)
    {
        if (myTeam == Ownership || Ownership == 0){
            return true;
        }
        return false;
    }

    /// <summary>
    /// Whenever we pick up Pickupable Object, we want to call this incase there are overrides
    /// </summary>
    public virtual void Pickup(){}

    /// <summary>
    /// Whenever we drop Pickupable Object, we want to call this incase there are overrides
    /// </summary>
    public virtual void Dropped(){}

}
