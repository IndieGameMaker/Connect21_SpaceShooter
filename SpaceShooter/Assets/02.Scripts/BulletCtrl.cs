using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    private Rigidbody rb;
    //총알의 데미지
    public float damage = 20.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 1500.0f);   
    }

}
