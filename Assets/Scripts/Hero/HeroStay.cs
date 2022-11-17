using UnityEngine;
using UnityEngine.AI;


public class HeroStay : IHeroPlay
{
    private bool IsStart;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private RaycastHit hitInfo;

    private HeroCyborg heroCyborg;

    public HeroStay(HeroCyborg hero)
    {
        hitInfo = new RaycastHit();

        heroCyborg = hero;
        navMeshAgent = hero.GetNavMeshAgent();
        animator = hero.GetAnimator();
        IsStart = false;
    }

    public void BeginPlay()
    {
        IsStart = true;
    }

    public void EndPlay()
    {
        IsStart = false;
    }

    public HeroState UpdatePlay()
    {
        if (!IsStart) return HeroState.none;

        if (!Input.GetMouseButtonDown(0))
        {
            navMeshAgent.enabled = false;
            animator.Play("idle1");
            return HeroState.stay;
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100))
        {
            IFieldCell fieldCell = hitInfo.transform.GetComponent<IFieldCell>();

            if (fieldCell != null)
            {
                heroCyborg.fieldCell = fieldCell;
            }
            else
            {
                heroCyborg.fieldCell = null;
            }

            float distanc = Vector3.Distance(heroCyborg.transform.position, hitInfo.point);

            if (distanc > 1f)
            {
                heroCyborg.targetPos = hitInfo.point;
                return HeroState.move;
            }

            if (distanc <= 1f)
            {
                if (fieldCell != null)
                {
                    return fieldCell.fieldOperation();
                }
                return HeroState.stay;
            }
        }

        return HeroState.stay;
    }

}
