using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            ContactPoint[] cp = coll.contacts;

            Vector3 pos = cp[0].point;
            Quaternion rot = Quaternion.LookRotation(cp[0].normal);
            GameObject effect = Instantiate(sparkEffect, pos, rot); 
            Destroy(effect, 0.3f);
            Destroy(coll.gameObject);
        }
    }
}
