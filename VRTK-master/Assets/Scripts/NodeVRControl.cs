namespace VRTK{


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVRControl : VRTK_InteractableObject
{
    float spinSpeed = 0f;


    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        print("USING");
    }

    public override void StopUsing(VRTK_InteractUse usingObject)
    {

    }

    protected void Start()
    {

    }

    protected override void Update()
    {

    }
}
}