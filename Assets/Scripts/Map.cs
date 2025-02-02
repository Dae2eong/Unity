using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    public static Map Instance {  get; private set; }

    [SerializeField] List<Monster> monsters = new List<Monster>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Monster GetMonster(int handle)
    {
        foreach(Monster monster in monsters)
        {
            if (monster.Handle == handle)
            {
                return monster;
            }
        }
        return null;
    }
    public Monster GetNearestMonster(Vector3 playerPos)
    {
        Monster nearestMonster = null; // 가장 가까운 몬스터를 저장

        float distMin = 50f;
        // C# 에서는 지역 변수를 초기화 없이 사용하려고 하면 컴파일러가 오류를 발생시킴

        monsters.RemoveAll(monster => monster == null);

        foreach (Monster monster in monsters)
        {
            if (monster == null)
            {
                continue;
            }

            var distSqr = GetDistanceSqr(monster.transform.position, playerPos);
            if (distSqr < distMin)
            {
                distMin = distSqr;
                nearestMonster = monster;
            }
        }

        return nearestMonster;
    }

    public void RemoveMonster(Monster monster)
    {
        if (monsters.Contains(monster))
        {
            monsters.Remove(monster);
        }
    }

    float GetDistanceSqr(Vector3 a, Vector3 b) // 플레이어와 가까운 몬스터 반환, 제곱을 비교해 가장 가까운 몬스터 찾기
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
    }
}
