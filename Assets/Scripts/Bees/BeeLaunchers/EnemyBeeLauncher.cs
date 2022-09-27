using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeLauncher : BeeLauncher
{
    
    protected override void reset()
    {
        this.isLoaded = false;
    }

    protected override Vector3 calculateDirection()
    {
        return (this.endDragPosition - this.launchPosition).normalized;
    }
}
