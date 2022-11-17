using UnityEngine;
using UnityEngine.AI;


public class HeroPlant : IHeroPlay
{
    private const float PLANT_TIME = 1.5f;

    private HeroCyborg heroCyb;
    private NavMeshAgent navMeshagent;
    private Animator animator;
    private bool IsStart;

    private float timer;

    public HeroPlant(HeroCyborg hero)
    {
        heroCyb = hero;
        navMeshagent = hero.GetNavMeshAgent();
        animator = hero.GetAnimator();
        IsStart = false;
    }

    public void BeginPlay()
    {
        timer = 0.0f;
        IsStart = true;
    }

    public void EndPlay()
    {
        IsStart = false;
    }

    public HeroState UpdatePlay()
    {
        if (!IsStart) return HeroState.none;

        navMeshagent.enabled = false;
        animator.Play("plant1");

        timer += Time.deltaTime;
        if (timer >= PLANT_TIME)
        {
            heroCyb.HeroPlantAction?.Invoke();
            return HeroState.stay;
        }

        return HeroState.plant;
    }

}
