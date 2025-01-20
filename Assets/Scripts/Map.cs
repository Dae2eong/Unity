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
        Monster nearestMonster = null; // ���� ����� ���͸� ����

        float distMin = float.MaxValue;
        // ó�� �񱳸� �����ϱ� ������ �ƹ� ���͵� Ȯ������ �ʾ����Ƿ�
        // �ּҰ� �񱳿� ���� ū ���� �⺻������ ����
        // C# ������ ���� ������ �ʱ�ȭ ���� ����Ϸ��� �ϸ� �����Ϸ��� ������ �߻���Ŵ

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

    float GetDistanceSqr(Vector3 a, Vector3 b) // �÷��̾�� ����� ���� ��ȯ, ������ ���� ���� ����� ���� ã��
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
    }
}
