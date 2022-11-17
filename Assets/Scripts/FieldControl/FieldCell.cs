using UnityEngine;

public class FieldCell : MonoBehaviour, IFieldCell
{

    [SerializeField]
    private GameObject plantMenu;

    private Vector3 posMenu;
    private GameObject pMenu;
    private FieldControl fieldControl;

    private void Awake()
    {
        posMenu = transform.position;
        posMenu.y += 2.5f;
    }

    public void Init(FieldControl fControl)
    {
        fieldControl = fControl;
    }

    private void CreatePlantMenu()
    {
        pMenu = Instantiate(plantMenu, posMenu, Quaternion.identity);
        pMenu.GetComponentInChildren<PlantButtonsControl>().InitField(fieldControl, this);
        pMenu.transform.parent = transform;

    }

    public void DeletePlantMenu()
    {
        Destroy(pMenu);
    }

    public HeroState fieldOperation()
    {
        CreatePlantMenu();
        return HeroState.none;
    }
}
