using UnityEngine;

public class SlowJumpPlatform : MonoBehaviour, IPlatform
{
    [SerializeField] private float modifier = 0.8f;
    public void AtTouch(PlayerManager pm)
    {
       pm.curVel.y = pm.force* modifier;
    }
}
