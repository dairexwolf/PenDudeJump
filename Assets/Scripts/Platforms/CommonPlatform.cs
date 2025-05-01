using UnityEngine;

public class CommonPlatform : MonoBehaviour, IPlatform
{
    public void AtTouch(PlayerManager pm)
    {
       pm.curVel.y = pm.force;
    }
}
