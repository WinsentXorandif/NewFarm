using UnityEngine;
using UnityEngine.UI;


public class PlantButtonsControl : MonoBehaviour
{
    [SerializeField]
    private Button buttonTree;
    [SerializeField]
    private Button buttonCarrot;
    [SerializeField]
    private Button buttonGrass;
    [SerializeField]
    private Button buttonCansel;

    private FieldControl fControl;
    private FieldCell fieldCell;

    private Transform mainCamTransform;


    private void Start()
    {

        mainCamTransform = Camera.main.transform;

        buttonTree.onClick.RemoveAllListeners();
        buttonTree.onClick.AddListener(() => OnButtonTreeNew());

        buttonCarrot.onClick.RemoveAllListeners();
        buttonCarrot.onClick.AddListener(() => OnButtonCarrotNew());

        buttonGrass.onClick.RemoveAllListeners();
        buttonGrass.onClick.AddListener(() => OnButtonGrassNew());

        buttonCansel.onClick.RemoveAllListeners();
        buttonCansel.onClick.AddListener(() => OnButtonCanselNew());

    }

    public void InitField(FieldControl field, FieldCell cell)
    {
        fControl = field;
        fieldCell = cell;
    }

    #region ButtonOperation
    private void OnButtonTreeNew()
    {
        fControl.HeroChoosePlant(EPlants.Tree, fieldCell);
    }
    private void OnButtonCarrotNew()
    {
        fControl.HeroChoosePlant(EPlants.Carrot, fieldCell);
    }
    private void OnButtonGrassNew()
    {
        fControl.HeroChoosePlant(EPlants.Grass, fieldCell);
    }
    private void OnButtonCanselNew()
    {
        fControl.DelCellMenu(fieldCell);
    }
    #endregion

    private void OnDisable()
    {
        buttonTree.onClick.RemoveListener(OnButtonTreeNew);
        buttonCarrot.onClick.RemoveListener(OnButtonCarrotNew);
        buttonGrass.onClick.RemoveListener(OnButtonGrassNew);
        buttonCansel.onClick.RemoveListener(OnButtonCanselNew);
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamTransform.forward);
    }
}
