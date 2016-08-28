using UnityEngine;
using System.Collections;

namespace PC2D
{
  public class CameraFollow : MonoBehaviour
  {
    public Transform Target;
    public float OffsetX;
    public float OffsetY;
    public bool FollowingPlayer = true;

    // Update is called once per frame
    void Update()
    {
      if (!Target) {
        Debug.Log("NIKOHO NEMAM!");
        return;
      }
      Vector3 pos = transform.position;
      if (Mathf.Abs(Target.position.x - transform.position.x) > OffsetX)
      {
        pos.x = Target.position.x - ((Target.position.x > transform.position.x) ? OffsetX : -OffsetX);
      }
      if (Mathf.Abs(Target.position.y - transform.position.y) > OffsetY)
      {
        pos.y = Target.position.y - ((Target.position.y > transform.position.y) ? OffsetY : -OffsetY);
      }
      if (FollowingPlayer) { 
        pos.x = Mathf.Clamp(pos.x, -21.0f, 26.2f);
        pos.y = Mathf.Clamp(pos.y, -6.0f, 12.9f);
      }
      transform.position = pos;
    }
  }
}
