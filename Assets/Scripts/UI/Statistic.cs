using TMPro;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI carrotText;

    [SerializeField]
    private TextMeshProUGUI expirienceText;

    public void SetCarrotText(int num)
    {
        carrotText.SetText($"{num}");
    }
    public void SetExpirienceText(int num)
    {
        expirienceText.SetText($"{num}");
    }

}
