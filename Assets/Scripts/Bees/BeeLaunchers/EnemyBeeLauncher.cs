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
      Vector3 direction = this.endPosition - this.launchPosition;
      return direction;
    }
}
