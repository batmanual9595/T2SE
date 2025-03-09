using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeerState
{
    public virtual void handleGravity(){}
    public virtual void handleForward(){}
    public virtual void handleLeft(){}
    public virtual void handleRight(){}
    public virtual void handleSpace(){}
    public virtual void handleShift(){}
    public virtual void advanceState(){}

    
}
