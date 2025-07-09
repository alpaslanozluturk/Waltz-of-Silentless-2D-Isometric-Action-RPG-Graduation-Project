using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftSlot : MonoBehaviour
{
    private ItemDataSO itemToCraft;
    [SerializeField] private UI_CraftPreviw craftPreviw;


    [SerializeField] private Image craftItemIcon;
    [SerializeField] private TextMeshProUGUI craftItemName;


    public void SetupButton(ItemDataSO craftData)
    {
        this.itemToCraft = craftData;
        craftItemIcon.sprite = craftData.itemIcon;
        craftItemName.text = craftData.itemName;
    }

    public void UpdateCraftPreviw() => craftPreviw.UpdateCraftPreviw(itemToCraft);

}
