using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runLeft;
    public AnimationClip runRight;
    public AnimationClip[] dies;
}

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    private Animation anim;

    public PlayerAnim playerAnim;
    public float moveSpeed = 10.0f;

    void Start()
    {
        tr = GetComponent<Transform>(); 
        anim = GetComponent<Animation>();

        anim.Play(playerAnim.idle.name);
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical"); // -1.0f ~ 0.0f ~ +1.0f
        float h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ +1.0f
        float r = Input.GetAxis("Mouse X");

        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(dir.normalized * Time.deltaTime * moveSpeed);

        //회전로직
        tr.Rotate(Vector3.up * Time.deltaTime * 80.0f * r);

        if (v >= 0.1f)  //전진
        {
            anim.CrossFade(playerAnim.runForward.name, 0.3f);
        }
        else if (v <= -0.1f) //후진
        {
            anim.CrossFade(playerAnim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)  //오른쪽
        {
            anim.CrossFade(playerAnim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f) //왼쪽
        {
            anim.CrossFade(playerAnim.runLeft.name, 0.3f);
        }
        else
        {
            anim.CrossFade(playerAnim.idle.name, 0.3f);
        }



        /*
        정규화 벡터 = 크기가 1인 벡터 = 방향만을 가리키는 벡터
        단위 벡터
        Normalized Vector

        Vector3.forward = Vector3(0, 0, 1)
        Vector3.up      = Vector3(0, 1, 0)
        Vector3.right   = Vector3(1, 0, 0)

        Vector3.one     = Vector3(1, 1, 1)
        Vector3.zero    = Vector3(0, 0, 0)
        */
    }


}
