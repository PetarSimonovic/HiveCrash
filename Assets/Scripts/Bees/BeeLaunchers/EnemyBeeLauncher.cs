using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeLauncher : BeeLauncher
{
    

    protected override void Start() {
      SetIsPlayer(false);
    }
    protected override void reset()
    {
        this.isLoaded = false;
    }
    public override void SetEndPosition(Vector3 worldPosition)
    {
      this.endPosition = fixYPosition(worldPosition);
    }
    // protected override Vector3 calculateDirection()
    // {
    //   Vector3 direction = this.endPosition - this.launchPosition;
    //   return direction;
    // }
}
