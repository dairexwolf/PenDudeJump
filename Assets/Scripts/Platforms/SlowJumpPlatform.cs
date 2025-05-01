using UnityEngine;

public class SlowJumpPlatform : MonoBehaviour, IPlatform
{
    public void AtTouch(PlayerManager pm)
    {
       pm.curVel.y = pm.force/2;
    }
}
