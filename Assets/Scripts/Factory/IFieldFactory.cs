using UnityEngine;

public interface IFieldFactory
{
    GameObject CreateFieldCell(Vector3 pos);

    GameObject CreateFieldCellPlant(Vector3 pos);
}
