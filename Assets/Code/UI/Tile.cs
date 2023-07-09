using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public bool isSelectable = true;

    public BaseMinion OccupiedUnit;


    void OnMouseEnter()
    {
        if (GameManager.Instance.GameState != GameState.MinionPhase && isSelectable == false) return;
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        if (GameManager.Instance.GameState != GameState.MinionPhase && isSelectable == false) return;
        _highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.MinionPhase && isSelectable == false) return;


        if (OccupiedUnit != null)
        {
            UnitManager.Instance.SetSelectedMinion(OccupiedUnit);
            if (CardManager.instance.SelectedCard != null)
            {
                if (CardManager.instance.SelectedCard.type == CardType.Minion)
                {
                    //TODO Upgrade
                }
                else if (CardManager.instance.SelectedCard.type == CardType.Buff)
                {
                    if (CardManager.instance.SelectedCard.buffType == BuffType.AttackPlus2)
                    {
                        OccupiedUnit.attack += 2;

                    }
                    else if (CardManager.instance.SelectedCard.buffType == BuffType.AttackPlus4)
                    {
                        OccupiedUnit.attack += 4;
                    }
                    else if (CardManager.instance.SelectedCard.buffType == BuffType.HealthPlus2)
                    {
                        OccupiedUnit.maxHealth += 2;
                        OccupiedUnit.currentHealth += 2;
                    }
                    else
                    {
                        OccupiedUnit.maxHealth += 4;
                        OccupiedUnit.currentHealth += 4;
                    }
                    UnitManager.Instance.SetSelectedMinion(OccupiedUnit);
                    GameObject card = CardManager.instance.Hand.Where(c => c.GetComponent<CardViewer>().guid == CardManager.instance.SelectedCardViewer.guid).First();
                    if (card != null)
                    {
                        CardManager.instance.Hand.Remove(card);
                        Destroy(card);
                        CardManager.instance.SelectedCardViewer = null;
                        CardManager.instance.SelectedCard = null;
                        CardManager.instance.rearrangeCards();
                    }

                }
                else
                {
                    if (CardManager.instance.SelectedCard.healType == HealType.PartHeal)
                    {
                        OccupiedUnit.currentHealth = OccupiedUnit.currentHealth + (OccupiedUnit.maxHealth / 2) > OccupiedUnit.maxHealth ? OccupiedUnit.maxHealth : OccupiedUnit.currentHealth + (OccupiedUnit.maxHealth / 2);
                    }
                    else
                    {
                        OccupiedUnit.currentHealth = OccupiedUnit.maxHealth;
                    }

                    UnitManager.Instance.SetSelectedMinion(OccupiedUnit);
                    GameObject card = CardManager.instance.Hand.Where(c => c.GetComponent<CardViewer>().guid == CardManager.instance.SelectedCardViewer.guid).First();
                    if (card != null)
                    {
                        CardManager.instance.Hand.Remove(card);
                        Destroy(card);
                        CardManager.instance.SelectedCardViewer = null;
                        CardManager.instance.SelectedCard = null;
                        CardManager.instance.rearrangeCards();
                    }

                }
            }
        }
        else
        {
            if (CardManager.instance.SelectedCard != null)
            {
                if (CardManager.instance.SelectedCard.type == CardType.Minion)
                {
                    BaseMinion spawnedMinion = Instantiate(UnitManager.Instance.GetSpecifiedMinionUnit(CardManager.instance.SelectedCard.minionType));
                    spawnedMinion.attack = CardManager.instance.SelectedCard.Attack;
                    spawnedMinion.maxHealth = CardManager.instance.SelectedCard.Health;
                    spawnedMinion.currentHealth = CardManager.instance.SelectedCard.Health;
                    SetUnit(spawnedMinion);
                    UnitManager.Instance.SetSelectedMinion(null);
                    GameObject card = CardManager.instance.Hand.Where(c => c.GetComponent<CardViewer>().guid == CardManager.instance.SelectedCardViewer.guid).First();
                    if (card != null)
                    {
                        CardManager.instance.Hand.Remove(card);
                        Destroy(card);
                        CardManager.instance.SelectedCardViewer = null;
                        CardManager.instance.SelectedCard = null;
                        CardManager.instance.rearrangeCards();
                    }
                }
                else
                {
                    if (CardManager.instance.SelectedCard.type == CardType.Buff)
                    {
                        FadingText.text.text = "Cannot use buff on empty tile";
                        StartCoroutine(FadingText.FadeTextToZeroAlpha(5));
                    }
                    else
                    {
                        FadingText.text.text = "Cannot use heal on empty tile";
                        StartCoroutine(FadingText.FadeTextToZeroAlpha(5));
                    }
                }
            }
        }

    }

    public void SetUnit(BaseMinion unit)
    {
        if (this.isSelectable == false) return;
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void SetHero(BaseHero unit, int offset)
    {
        if (this.isSelectable) return;
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        unit.transform.position = new Vector3(unit.transform.position.x - offset, unit.transform.position.y, unit.transform.position.z);
        Text text = unit.GetComponentInChildren<Text>();
        text.text = "Health: " + unit.currentHealth.ToString();
        text.transform.position = new Vector3(unit.transform.position.x - offset, unit.transform.position.y, unit.transform.position.z);
        unit.OccupiedTile = this;
    }

}
