using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public HeroType heroType;
    public int currentHealth;
    public int attack;
    public bool minionCollision = false;
    public bool bossCollision = false;




    public void MoveForward()
    {
        StartCoroutine(startMovement());
    }

    IEnumerator startMovement()
    {
        while (!minionCollision && !bossCollision)
        {
            transform.position = new Vector3(transform.position.x + (2 *Time.deltaTime), transform.position.y, transform.position.z);
            yield return 0;
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lich King")
        {
            bossCollision = true;

            GameManager.Instance.TotalHealth -= attack;
            Destroy(gameObject);
            if (GameManager.Instance.TotalHealth <= 0)
            {
                GameManager.Instance.ChangeState(GameState.EndGame);
            }
        }
        else if(collision.gameObject.tag == "Minion")
        {
            minionCollision = true;

           StartCoroutine(UnitManager.Instance.Battle(this, collision.gameObject.GetComponent<BaseMinion>()));
        }
    }


    public enum HeroType
    {
        Knight,
        Paladin,
        King
    }
}
