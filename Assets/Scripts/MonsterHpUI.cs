using UnityEngine;
using UnityEngine.UI;

public class MonsterHpUI : MonoBehaviour
{
    public static MonsterHpUI Instance { get; private set; }

    public Slider hpSlider;
    public Monster currentMonster;

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

    public void SetMonster(Monster monster)
    {
        if (monster == null)
        {
            gameObject.SetActive(false);
            return;
        }

        currentMonster = monster;
        gameObject.SetActive(true);
        UpdateHP();
    }

    public void UpdateHP()
    {
        if (currentMonster != null)
        {
            hpSlider.value = currentMonster.hp / 100.0f;
        }
    }
}
