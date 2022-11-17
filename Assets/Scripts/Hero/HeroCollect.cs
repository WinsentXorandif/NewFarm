using UnityEngine;
using UnityEngine.AI;

public class HeroCollect : IHeroPlay
{
    private const float COLLECT_TIME = 1.5f;

    private HeroCyborg heroCyb;
    private NavMeshAgent navMeshagent;
    private Animator animator;
    private bool IsStart;

    private float timer;

    public HeroCollect(HeroCyborg hero)
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
        animator.Play("collect1");

        timer += Time.deltaTime;
        if (timer >= COLLECT_TIME)
        {
            heroCyb.HeroCollectAction?.Invoke();
            return HeroState.stay;
        }

        return HeroState.collect;
    }
}
