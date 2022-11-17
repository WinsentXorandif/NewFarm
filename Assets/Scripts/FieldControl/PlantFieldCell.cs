using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlantFieldCell : MonoBehaviour, IFieldCell
{
    [SerializeField]
    private Transform plantPoint;

    [SerializeField]
    private PlantData plantData;

    [SerializeField]
    private GameObject progresUI;

    [SerializeField]
    private TextMeshProUGUI textMPROUI;
    [SerializeField]
    private Image imageUI;

    private float growthTime;
    private int experience;

    private FieldControl fieldControl;
    private EPlants thisPlant;

    private GameObject smollPlant;
    private GameObject bigPlant;

    private int IndexPlant;

    private void Awake()
    {
        IndexPlant = 0;
        growthTime = 0;
    }

    public EPlants getPlantType() { return thisPlant; }

    public void Init(FieldControl fControl, EPlants plant)
    {
        fieldControl = fControl;
        thisPlant = plant;
    }

    private bool CheckData()
    {
        for (int i = 0; i < plantData.allPlantData.plantList.Count; i++)
        {
            if (plantData.allPlantData.plantList[i].namePlant == thisPlant)
            {
                growthTime = plantData.allPlantData.plantList[i].growthTime;
                experience = plantData.allPlantData.plantList[i].experience;
                IndexPlant = i;
                return true;
            }
        }
        return false;
    }

    private void Start()
    {
        if (CheckData())
        {
            StartCoroutine(CStartPlanting());
        }
    }

    private void CreateSmollPlant()
    {
        smollPlant = Instantiate(plantData.allPlantData.plantList[IndexPlant].smollPlant, plantPoint.position, plantPoint.rotation);
        Vector3 anglesPlant = new Vector3(0f, Random.Range(0f, 180f), 0f);
        smollPlant.transform.localEulerAngles = anglesPlant;
        smollPlant.transform.parent = transform;
    }

    private void CreateBigPlant()
    {
        bigPlant = Instantiate(plantData.allPlantData.plantList[IndexPlant].bigPlant, plantPoint.position, plantPoint.rotation);
        Vector3 anglesPlant = new Vector3(0f, Random.Range(0f, 180f), 0f);
        bigPlant.transform.localEulerAngles = anglesPlant;
        bigPlant.transform.parent = transform;
    }


    IEnumerator CStartPlanting()
    {
        float tmpTime = 0.1f;
        textMPROUI.SetText("0%");

        do
        {
            tmpTime += 0.1f;
            float progress = tmpTime / growthTime;
            imageUI.fillAmount = progress;

            int textRez = Mathf.CeilToInt(progress * 100);

            if ((textRez >= 50) && (smollPlant == null))
            {
                CreateSmollPlant();
            }

            if (textRez > 100)
            {
                textRez = 100;
            }

            textMPROUI.SetText($"{textRez}%");

            yield return new WaitForSeconds(0.1f);

        } while (tmpTime < growthTime);

        Destroy(progresUI);
        Destroy(smollPlant);

        CreateBigPlant();
    }

    public HeroState fieldOperation()
    {
        if ((thisPlant == EPlants.Tree) || (bigPlant == null)) return HeroState.stay;
        fieldControl.HeroCollect(this, experience);
        return HeroState.collect;
    }
}
