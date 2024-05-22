using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void OnDead(Transform obj)
    {
        Destroy(obj.gameObject);
    }
}
