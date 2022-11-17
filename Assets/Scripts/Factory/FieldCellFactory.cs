using UnityEngine;

namespace FieldFactory
{
    public class FieldCellFactory : IFieldFactory
    {
        private GameObject fiedCellGO;
        private GameObject fiedCellPlantGO;

        public FieldCellFactory(GameObject emptyCell, GameObject plantCell)
        {
            fiedCellGO = emptyCell;
            fiedCellPlantGO = plantCell;
        }

        public GameObject CreateFieldCell(Vector3 pos)
        {
            GameObject go = Object.Instantiate(fiedCellGO, pos, Quaternion.identity);
            return go;
        }

        public GameObject CreateFieldCellPlant(Vector3 pos)
        {
            GameObject go = Object.Instantiate(fiedCellPlantGO, pos, Quaternion.identity);
            return go;
        }
    }
}