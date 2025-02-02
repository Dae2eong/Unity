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
        Monster nearestMonster = null; // ���� ����� ���͸� ����

        float distMin = 50f;
        // C# ������ ���� ������ �ʱ�ȭ ���� ����Ϸ��� �ϸ� �����Ϸ��� ������ �߻���Ŵ

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

    float GetDistanceSqr(Vector3 a, Vector3 b) // �÷��̾�� ����� ���� ��ȯ, ������ ���� ���� ����� ���� ã��
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
    }
}
