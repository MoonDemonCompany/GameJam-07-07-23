using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;
    private List<ScriptableMinion> _minions;
    private List<ScriptableHero> _heroes;
    public BaseMinion selectedMinion;
    public ObservableCollection<BaseHero> spawnedHeroes = new ObservableCollection<BaseHero>();
    public BaseHero prefab;

    void Awake() {
        Instance = this;

        _minions = Resources.LoadAll<ScriptableMinion>("Minions").ToList();
        _heroes = Resources.LoadAll<ScriptableHero>("Heroes").ToList();
        spawnedHeroes.CollectionChanged += spawnedHeroes_CollectionChanged;

    }

  
    public void MinionPhase() 
    {

        int numberOfLanes = Random.Range(1, GameManager.Instance.CurrentWave > 5 ? 5 : GameManager.Instance.CurrentWave);
        int numberOfHeroes = Random.Range(1, GameManager.Instance.CurrentWave);
        if(GameManager.Instance.CurrentWave % 5 == 0)
        {
            Tile randomTile = GridManager.Instance.GetHeroSpawnTile();
            BaseHero spawnedHero = Instantiate(prefab);
            spawnedHeroes.Add(spawnedHero);
            randomTile.SetHero(spawnedHero,0);
        }
        else
        {
            SpawnHeroes(numberOfHeroes, numberOfLanes);
        }

    }

    public void SpawnHeroes(int numberOfHeroes, int numberOfLanes)
    {
        SpawnUnitsSlowly(numberOfHeroes, numberOfLanes);
        
    }

      void SpawnUnitsSlowly(int numberOfHeroes, int numberOfLanes)
    {
        List<Tile> RandomTiles = new List<Tile>();
        for (int i = 0; i < numberOfLanes; i++)
        {
            Tile randomTile = GridManager.Instance.GetHeroSpawnTile();
            if (!RandomTiles.Contains(randomTile))
            {
                RandomTiles.Add(randomTile);
            }
            else
            {
                i--;
            }
        }
        foreach (Tile tile in RandomTiles)
        {
            SpawnMultipleLanes(numberOfHeroes, tile);
        }
    }

    void SpawnMultipleLanes(int numberOfHeroes, Tile tile)
    {
        for (int i = 0; i < numberOfHeroes; i++)
        {
            BaseHero randomPrefab = GetRandomUnit();
            BaseHero spawnedHero = Instantiate(randomPrefab);

            spawnedHeroes.Add(spawnedHero);
            tile.SetHero(spawnedHero, i * 5);
        }
       
    }

    private void spawnedHeroes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if(spawnedHeroes.Count == 0)
        {
            GameManager.Instance.ChangeState(GameState.DrawPhase);
        }
    }

    public void AttackPhase()
    {
        StartCoroutine(slowlyAttack());
    }

    IEnumerator slowlyAttack()
    {
        for (int i = 0; i < spawnedHeroes.Count; i++)
        {
            spawnedHeroes[i].MoveForward();
        }
        yield return null;
    }

    public IEnumerator Battle(BaseHero hero, BaseMinion minion)
    {
        while (hero.currentHealth > 0 && minion.currentHealth > 0)
        {
            hero.currentHealth -= minion.attack;
            minion.currentHealth -= hero.attack;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        if(hero.currentHealth <= 0)
        {
            Destroy(hero.gameObject);
            spawnedHeroes.Remove(hero);
        }
        if(minion.currentHealth <= 0)
        {
            Destroy(minion.gameObject);
            hero.minionCollision = false;
            hero.MoveForward();
        }
        yield return null;
    }

    private BaseHero GetRandomUnit()
    {
        return _heroes.Where(u => u.unitPrefab.heroType != BaseHero.HeroType.King).OrderBy(o => Random.value).First().unitPrefab;
    }

    public BaseMinion GetSpecifiedMinionUnit(BaseMinion.MinionType minionType)
    {
        return _minions.Where(u => (u.unitPrefab).minionType == minionType).First().unitPrefab;
    }

    public void SetSelectedMinion(BaseMinion minion) {
        selectedMinion = minion;
        MenuManager.Instance.ShowSelectedMinion(minion);
    }

    
}
