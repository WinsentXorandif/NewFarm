using UnityEngine;



[CreateAssetMenu(fileName = "NewFieldData", menuName = "Field Data")]
public class FieldData : ScriptableObject
{
    private const int MIN = 1;
    private const int MAX = 9;


    [SerializeField]
    [Range(MIN, MAX)]
    private int lengthField;

    [SerializeField]
    [Range(MIN, MAX)]
    private int widthField;

    [SerializeField]
    private GameObject field;

    [SerializeField]
    private GameObject fieldPlant;


    public int getLengthField()
    {
        return lengthField;
    }

    public int getWidthField()
    {
        return widthField;
    }

    public GameObject getFieldOne()
    {
        return field;
    }

    public GameObject getFieldPlant()
    {
        return fieldPlant;
    }


}
