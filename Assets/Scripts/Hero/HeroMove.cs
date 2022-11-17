using UnityEngine;
using UnityEngine.AI;

public class HeroMove : IHeroPlay
{
    private bool IsStart;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private HeroCyborg heroCyborg;
    private RaycastHit hitInfo;

    public HeroMove(HeroCyborg hero)
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

        if (Input.GetMouseButtonDown(0))
        {
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

                heroCyborg.targetPos = hitInfo.point;
            }
        }

        float distanc = Vector3.Distance(heroCyborg.transform.position, heroCyborg.targetPos);

        if (distanc <= 1f)
        {
            if (heroCyborg.fieldCell != null)
            {
                navMeshAgent.enabled = false;
                animator.Play("idle1");
                return heroCyborg.fieldCell.fieldOperation();
            }
            return HeroState.stay;
        }

        navMeshAgent.enabled = true;
        navMeshAgent.destination = heroCyborg.targetPos;
        animator.Play("run1");
        return HeroState.move;

    }
}
