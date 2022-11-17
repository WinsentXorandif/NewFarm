using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroCyborg : MonoBehaviour
{

    public Action HeroPlantAction;
    public Action HeroCollectAction;

    private Dictionary<HeroState, IHeroPlay> heroPlayDict;
    private IHeroPlay iHeroPlayCurrent;
    private HeroState heroStateCurrent;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private NavMeshAgent navMeshAgent;


    public Vector3 targetPos { get; set; }
    public IFieldCell fieldCell { get; set; }

    public Animator GetAnimator() { return animator; }
    public NavMeshAgent GetNavMeshAgent() { return navMeshAgent; }

    private void Start()
    {
        InitDictionary();

        iHeroPlayCurrent = heroPlayDict[HeroState.stay];
        iHeroPlayCurrent.BeginPlay();
    }

    private void InitDictionary()
    {
        heroPlayDict = new Dictionary<HeroState, IHeroPlay>
        {
            { HeroState.none, new HeroNone() },
            { HeroState.stay, new HeroStay(this) },
            { HeroState.move, new HeroMove(this) },
            { HeroState.plant, new HeroPlant(this) },
            { HeroState.collect, new HeroCollect(this) }
        };
    }


    public HeroState SetNewHeroPlay(HeroState newHeroPlay)
    {
        if (iHeroPlayCurrent != null) iHeroPlayCurrent.EndPlay();

        heroStateCurrent = newHeroPlay;
        iHeroPlayCurrent = heroPlayDict[newHeroPlay];
        iHeroPlayCurrent.BeginPlay();
        return heroStateCurrent;
    }

    private void Update()
    {
        HeroState newHeroPlay = iHeroPlayCurrent.UpdatePlay();

        if (newHeroPlay != heroStateCurrent)
        {
            SetNewHeroPlay(newHeroPlay);
        }
    }

}

