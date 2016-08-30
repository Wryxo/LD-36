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
        float aspect = GetComponent<Camera>().aspect;
        if (aspect == 4f/3f) {
          pos.x = Mathf.Clamp(pos.x, -26.3f, 31.4f);
          pos.y = Mathf.Clamp(pos.y, 1.0f, 19.9f);
        } else if (aspect == 16f/10f) {
          pos.x = Mathf.Clamp(pos.x, -23.0f, 28.25f);
          pos.y = Mathf.Clamp(pos.y, 1.0f, 19.9f);
        }
        else {
          pos.x = Mathf.Clamp(pos.x, -21.0f, 26.25f);
          pos.y = Mathf.Clamp(pos.y, 1.0f, 19.9f);
        }
      }
      transform.position = pos;
    }
  }
}
