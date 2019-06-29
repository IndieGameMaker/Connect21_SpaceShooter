using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform[] points;
    public GameObject monsterPrefab;
    //몬스터의 생성 주기
    public float createTime = 3.0f;
    //게임 종료 여부
    public bool isGameOver = false;

    void Start()
    {
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        //메소드를 반복해서 호출(실행) 시킴
        //InvokeRepeating("CreateMonster", 2.0f, createTime);
        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(2.0f); //Delay time

        while(isGameOver == false)
        {
            //난수 발생
            int idx = Random.Range(1, points.Length); //(1, 22) 1...21
            //몬스터 생성
            Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);

            yield return new WaitForSeconds(createTime);
        }
    }

}
