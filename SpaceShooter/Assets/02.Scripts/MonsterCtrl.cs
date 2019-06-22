using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//
public class MonsterCtrl : MonoBehaviour
{
    public enum State
    {
         IDLE
        ,TRACE
        ,ATTACK
        ,DIE
    }

    public State state = State.IDLE;
    public float attackDist = 2.0f;
    public float traceDist  = 10.0f;

    [HideInInspector]
    public Transform playerTr;
    private Transform monsterTr;
    private NavMeshAgent nv;
    private Animator anim;

    private bool isDie = false;
    //애니메이터 컨트롤러에 정의된 파라미터의 해쉬값을 미리 추출해 저장
    private int hashIsAttack = Animator.StringToHash("IsAttack");
    private int hashHit      = Animator.StringToHash("Hit");
    private int hashDie      = Animator.StringToHash("Die");

    //몬스터의 생명 게이지 수치
    private float hp = 100.0f;

    void Start()
    {
        playerTr    = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();        
        monsterTr   = GetComponent<Transform>();
        nv          = GetComponent<NavMeshAgent>();
        anim        = GetComponent<Animator>();

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    //몬스터의 상태값을 체크하는 코루틴
    IEnumerator CheckMonsterState()
    {
        while(!isDie)
        {
            //0.3초 간격 로직을 수행
            yield return new WaitForSeconds(0.3f);

            //주인공과 몬스터간의 거리를 계산
            float dist = Vector3.Distance(monsterTr.position, playerTr.position);

            //공격 사정거리 이내의 경우
            if (dist <= attackDist)
            {
                state = State.ATTACK;
            }
            //추적 사정거리 이내의 경우
            else if (dist <= traceDist)
            {
                state = State.TRACE;
            }
            //사정거리 밖에 위치한 경우
            else
            {
                state = State.IDLE;
            }
        }
    }

    //몬스터의 상태에 따라서 행동을 처리하는 루틴
    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            //몬스터의 상태값에 따라서 분기 처리
            switch (state)
            {
                case State.IDLE:                    //휴면 상태
                    nv.isStopped = true;            //추적불가
                    anim.SetBool("IsTrace", false); //휴면 애니메이션 실행
                    break;

                case State.TRACE:                           //추적 상태
                    nv.SetDestination(playerTr.position);   //
                    nv.isStopped = false;                   //추적가능

                    anim.SetBool("IsTrace", true);          //걷기 애니메이션 실행
                    anim.SetBool(hashIsAttack, false);
                    break;

                case State.ATTACK:  //공격 모드
                    nv.isStopped = true;
                    
                    //주인공을 향하도록 회전
                    monsterTr.LookAt(playerTr.position);

                    anim.SetBool(hashIsAttack, true); //Hash 값을 전달하는 방식
                    break;

                case State.DIE:     //사망
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //총알의 데미지 추출
            float _damage = coll.gameObject.GetComponent<BulletCtrl>().damage;
            //생명 수치를 차감
            hp -= _damage;
            if (hp <= 0.0f)
            {
                MonsterDie();
            }

            anim.SetTrigger(hashHit);
            Destroy(coll.gameObject);
        }
    }

    void MonsterDie()
    {
        //네비게이션 정지
        nv.isStopped = true;
        //Capsule Collier 비활성화
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;

        //현재 스크립트에서 실행중인 모든 코루틴 함수를 정지
        StopAllCoroutines();

        //몬스터의 Die 애니메이션 실행
        anim.SetTrigger(hashDie);
    }
}
