using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public AudioClip fireSfx;
    public MeshRenderer muzzleFlash;

    private AudioSource _audio;
    //Ray에 닿은 객체의 여러가지 충돌 정보를 저장할 변수
    private RaycastHit hit;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            //레이를 투사하는 함수 (발사원점, 발사방향, out 결괏값, 거리)
            //1<<8 = 2^8 = 256
            //1<<8 | 1<<9 , ~(1<<8)
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f, 1<<8))
            {
                hit.collider.GetComponent<MonsterCtrl>().OnDamage(hit, 25.0f);
            }
        }
    }

    void Fire()
    {
        //총알을 생성
        //Instantiate(bullet, firePos.position, firePos.rotation);
        //총 소리 발생
        _audio.PlayOneShot(fireSfx, 0.8f); //오디오 파일, 볼륨
        //코루틴 호출 함수
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        //Offset 값 변경
        Vector2 offset = new Vector2(Random.Range(0,2) , Random.Range(0,2)) * 0.5f;
        muzzleFlash.material.SetTextureOffset("_MainTex", offset);

        //Scale 변경 (컴포넌트).trasform
        muzzleFlash.transform.localScale = Vector3.one * Random.Range(1.0f, 3.0f);

        //Quaternio.Euler : 오일러각을 쿼터니언 타입으로 변환
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;

        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.enabled = false;
    }

}
