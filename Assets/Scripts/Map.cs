using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    [SerializeField] List<Monster> monsters = new List<Monster>();

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

        float distMin = float.MaxValue;
        // 처음 비교를 시작하기 전에는 아무 몬스터도 확인하지 않았으므로
        // 최소값 비교에 가장 큰 값을 기본값으로 설정
        // C# 에서는 지역 변수를 초기화 없이 사용하려고 하면 컴파일러가 오류를 발생시킴

        foreach (Monster monster in monsters)
        {
            var distSqr = GetDistanceSqr(monster.transform.position, playerPos);
            if (distSqr < distMin)
            {
                distMin = distSqr;
                nearestMonster = monster;
            }
        }

        return nearestMonster;
    }

    float GetDistanceSqr(Vector3 a, Vector3 b) // 플레이어와 가까운 몬스터 반환, 제곱을 비교해 가장 가까운 몬스터 찾기
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
    }
}
