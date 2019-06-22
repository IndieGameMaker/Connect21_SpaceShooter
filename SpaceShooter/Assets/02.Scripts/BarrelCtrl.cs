using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    private int hitCount;
    public GameObject expEffect;
    public Texture[] textures;
    public MeshRenderer _renderer;

    void Start()
    {
        _renderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        int idx = Random.Range(0, textures.Length); //0,1,2
        _renderer.material.mainTexture = textures[idx];

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1200.0f);

        Destroy(this.gameObject, 2.0f); //Destroy(gameObject, 2.0f);
        GameObject effect = Instantiate(expEffect
                                        , transform.position
                                        , Quaternion.identity);
        
        Destroy(effect, 3.0f);
        //Destroy(this); 스크립트를 삭제
    }
}
