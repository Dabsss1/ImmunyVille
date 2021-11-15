using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public List<InventoryItem> saleItems = new List<InventoryItem>();
    public List<PlantItem> saleSeeds = new List<PlantItem>();

    public TextMeshProUGUI goldText;

    public GameObject itemContentGO;
    public GameObject seedContentGO;

    public ShopItemBlueprint itemBlueprint;
    public ShopSeedBlueprint seedBlueprint;

    public GameObject NotEnoughMoneyText;
    public bool errorDisplayed;

    public TextMeshProUGUI BoughtPriceText;
    public bool deductTextDisplayed;
    public IEnumerator showDeduction;
    public static ShopUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        goldText.text = $"{Inventory.Instance.gold}";   

        foreach (Transform child in itemContentGO.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in saleItems)
        {
            ShopItemBlueprint shopItemBP = Instantiate(itemBlueprint, itemContentGO.transform);
            shopItemBP.SetData(item);
        }

        foreach (Transform child in seedContentGO.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (PlantItem seed in saleSeeds)
        {
            ShopSeedBlueprint shopSeedBP = Instantiate(seedBlueprint, seedContentGO.transform);
            shopSeedBP.SetData(seed);
        }
    }

    public void ExitSound()
    {
        AudioManager.Instance.PlaySfx("Exit");
    }

    public void ShowErrorMoney()
    {
        if (!errorDisplayed)
            StartCoroutine(ShowError());
    }

    IEnumerator ShowError()
    {
        errorDisplayed = true;
        NotEnoughMoneyText.SetActive(true);
        yield return new WaitForSeconds(1f);
        NotEnoughMoneyText.SetActive(false);
        errorDisplayed = false;
    }

    public void ShowDeductMoney(string deductPrice)
    {
        if (!deductTextDisplayed)
        {
            showDeduction = ShowDeduction(deductPrice);
            StartCoroutine(showDeduction);
        }
        else
        {
            StopCoroutine(showDeduction);
            BoughtPriceText.gameObject.SetActive(false);
            showDeduction = ShowDeduction(deductPrice);
            StartCoroutine(showDeduction);
        }
    }

    IEnumerator ShowDeduction(string deductPrice)
    {
        deductTextDisplayed = true;
        BoughtPriceText.gameObject.SetActive(true);
        BoughtPriceText.text = deductPrice;
        yield return new WaitForSeconds(1f);
        BoughtPriceText.gameObject.SetActive(false);
        deductTextDisplayed = false;
    }
}
