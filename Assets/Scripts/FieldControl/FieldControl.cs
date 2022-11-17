using FieldFactory;
using UnityEngine;

public class FieldControl : MonoBehaviour
{
    [SerializeField]
    private HeroCyborg hero;

    [SerializeField]
    private Statistic stat;

    [SerializeField]
    private FieldData fieldData;

    [SerializeField]
    private Transform startPointField;


    private FieldCellFactory fieldCellFactory;
    private EPlants tmpPlant;
    private PlantFieldCell tmpComplitPlant;


    private int lengthField;
    private int widthField;
    private GameObject fieldGO;
    private GameObject fieldPlantGO;

    private FieldCell fieldCellCurrent;

    private int carriotCount;
    private int expirienceVolume;

    private bool CheckPlantData()
    {
        if (fieldData == null)
        {
            Debug.Log("Error: FieldData is missing!");
            return false;
        }

        if (fieldData.getFieldOne() == null)
        {
            Debug.Log("Error: field game object is missing!");
            return false;
        }

        if (fieldData.getFieldPlant() == null)
        {
            Debug.Log("Error: FieldPlant game object is missing!");
            return false;
        }

        if (startPointField == null)
        {
            Debug.Log("Error: FieldStartPoint is missing!");
            return false;
        }

        fieldGO = fieldData.getFieldOne();
        fieldPlantGO = fieldData.getFieldPlant();
        lengthField = fieldData.getLengthField();
        widthField = fieldData.getWidthField();

        fieldCellFactory = new FieldCellFactory(fieldGO, fieldPlantGO);

        return true;
    }


    private void CreateField()
    {
        float boundsX = fieldGO.GetComponent<Renderer>().bounds.size.x;
        float boundsZ = fieldGO.GetComponent<Renderer>().bounds.size.z;

        Vector3 newCellPosZ = startPointField.position;

        for (int l = 0; l < widthField; l++)
        {
            newCellPosZ = startPointField.position;
            newCellPosZ.z += l * boundsZ;

            for (int i = 0; i < lengthField; i++)
            {
                Vector3 newCellPosX = newCellPosZ;
                newCellPosX.x -= i * boundsX;
                GameObject fieldCellTmp = fieldCellFactory.CreateFieldCell(newCellPosX);

                FieldCell cell = fieldCellTmp.GetComponent<FieldCell>();

                cell.Init(this);
            }
        }
    }

    public void DelCellMenu(FieldCell cell)
    {
        hero.SetNewHeroPlay(HeroState.stay);
        cell.DeletePlantMenu();
    }

    #region heroWork
    public void HeroChoosePlant(EPlants plant, FieldCell cell)
    {
        tmpPlant = plant;
        fieldCellCurrent = cell;

        hero.SetNewHeroPlay(HeroState.plant);
        cell.DeletePlantMenu();
    }

    public void HeroCollect(PlantFieldCell cell, int expir)
    {
        tmpComplitPlant = cell;
        expirienceVolume = expir;
    }
    #endregion

    private void Start()
    {
        if (CheckPlantData())
        {
            Debug.Log("Start FieldControl!!!");
            CreateField();
        }
    }


    private void OnHeroPlant()
    {
        Vector3 posPlant = fieldCellCurrent.transform.position;
        Destroy(fieldCellCurrent.gameObject);
        GameObject fieldPlantCellTmp = fieldCellFactory.CreateFieldCellPlant(posPlant);
        fieldPlantCellTmp.GetComponent<PlantFieldCell>().Init(this, tmpPlant);
    }

    private void OnHeroCollectComplit()
    {

        if (tmpComplitPlant.getPlantType() == EPlants.Carrot)
        {
            carriotCount++;
            int expirienceCount = carriotCount * expirienceVolume;

            stat.SetCarrotText(carriotCount);
            stat.SetExpirienceText(expirienceCount);

        }

        Vector3 posCell = tmpComplitPlant.transform.position;
        Destroy(tmpComplitPlant.gameObject);
        GameObject fieldCellTmp = fieldCellFactory.CreateFieldCell(posCell);
        FieldCell cell = fieldCellTmp.GetComponent<FieldCell>();
        cell.Init(this);
    }

    private void OnEnable()
    {
        hero.HeroPlantAction += OnHeroPlant;
        hero.HeroCollectAction += OnHeroCollectComplit;
    }

    private void OnDisable()
    {
        hero.HeroPlantAction -= OnHeroPlant;
        hero.HeroCollectAction -= OnHeroCollectComplit;
    }



}


