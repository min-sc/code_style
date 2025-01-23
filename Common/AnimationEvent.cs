using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
