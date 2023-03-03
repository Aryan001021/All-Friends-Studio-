using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class StoreGamma : MonoBehaviour
{
    [Header("json")]
    UnityEngine.TextAsset jsonFile;
    TopMenuContainerClass word = new TopMenuContainerClass();
    [Header("GameObjects")]
    [SerializeField] GameObject Slots;
    [SerializeField] GameObject textFieldForMenus;
    [SerializeField] GameObject prefabContainer;
    [SerializeField] GameObject characterPannel;
    [SerializeField] GameObject topMenuPannel;
    [SerializeField] GameObject sideMenuPannel;
    [SerializeField] GameObject moneyPannel;
    [SerializeField] GameObject pricePannel;
    [SerializeField] Button purchaseButton;
    List<string> topMenu = new List<string>();
    List<int> topMenuIndex = new List<int>();
    List<int> importantNumber=new List<int>();
    List<string> sideMenu = new List<string>();
    List<string> slotEntryName = new List<string>();
    List<string> slotEntryImageURL = new List<string>();
    List<List<Sprite>> allImageContainer = new List<List<Sprite>>();
    List<Sprite> showCaseImage = new List<Sprite>();
    List<string> slotEntryDescription = new List<string>();
    List<int> price = new List<int>();
    List<int> slotDamage = new List<int>();
    int currentSelect = 0;
    [Header("Scripts")]
    LoadingStoreData imageDataloadingScript;
    [Serializable]
    class itemData
    {
        public string eName;
        public string eImg;
        public int ePrice;
        public int edamage;
        public string edesc;

    }
    [Serializable]
    class SideMenu
    {
        public string itemName;
        public string itemImage;
        public int itemIndex;
        public int namingNumber;
        public itemData[] itemElements;
    }
    [Serializable]
    class TopMenu
    {
        public string pName;
        public string pImg;
        public int pOrder;
        public SideMenu[] pItems;
    }
    [Serializable]
    class TopMenuContainerClass
    {
        public TopMenu[] element1;
    }
    private void Awake()
    {
        imageDataloadingScript = FindObjectOfType<LoadingStoreData>();
        jsonFile=imageDataloadingScript.GetJson();
    }
    void Start()
    {
        allImageContainer=imageDataloadingScript.GetSpritesForShop();
        TopMenuFiller(0);
        UpperMenuInitialization();

    }
    private void Update()
    {
        
    }
    void TopMenuFiller(int sideMenuIndex)
    {
        word = JsonUtility.FromJson<TopMenuContainerClass>(jsonFile.text);
        foreach (var a in word.element1)
        {
            topMenu.Add(a.pName);
            topMenuIndex.Add(a.pOrder);

        }
        SideMenuFiller(sideMenuIndex);
    }
    void SideMenuFiller(int SideMenuIndex)
    {
        sideMenu.Clear();
        importantNumber.Clear();
        foreach (var b in word.element1[SideMenuIndex].pItems)
        {
            importantNumber.Add(b.namingNumber);
            sideMenu.Add(b.itemName);
        }
        MainMenuFiller(SideMenuIndex,0);
    }
    void MainMenuFiller(int topMenuIndex, int sideMenuIndex)
    {
        var b = word.element1[topMenuIndex].pItems[sideMenuIndex];
        {
            slotEntryName.Clear();
            slotEntryImageURL.Clear();
            showCaseImage.Clear();
            slotDamage.Clear();
            slotEntryDescription.Clear();
            price.Clear();
            foreach (var c in b.itemElements)
            {
                slotEntryName.Add(c.eName);
                slotEntryImageURL.Add(c.eImg);
                showCaseImage.AddRange(allImageContainer[importantNumber[sideMenuIndex]]);
                slotDamage.Add(c.edamage);
                slotEntryDescription.Add(c.edesc);
                price.Add((c.ePrice));
            }
        }

    }
    private void UpperMenuInitialization()
    {
        for (int i = 0; i < topMenu.Count; i++)
        { 
            UpperFunction1(i); 
        }
        UpperMenuButtonFunction(0);
    }

    private void UpperFunction1(int i)
    {
        GameObject upperTextButton = Instantiate(textFieldForMenus, topMenuPannel.transform);
        upperTextButton.GetComponent<TextMeshProUGUI>().text = topMenu[i];
        upperTextButton.GetComponent<Button>().onClick.AddListener(delegate { UpperMenuButtonFunction(i);  });
    }

    private void UpperMenuButtonFunction(int i)
    {
        DestroyAllChildren(prefabContainer);
        DestroyAllChildren(sideMenuPannel);

        SideMenuFiller(i);
        for (int j=0;j < sideMenu.Count;j++)
        {
            SideMenu1(i, j);
        }
        SideMenuButtonFunction(i, 0);
    }

    private void SideMenu1(int i, int j)
    {
        GameObject upperTextButton = Instantiate(textFieldForMenus, sideMenuPannel.transform);
        upperTextButton.GetComponent<TextMeshProUGUI>().text = sideMenu[j];
        upperTextButton.GetComponent<Button>().onClick.AddListener(delegate { SideMenuButtonFunction(i, j); });
    }

    private void SideMenuButtonFunction(int i,int j)
    {
        MainMenuFiller(i,j);
        DestroyAllChildren(prefabContainer);
        for (int k = 0; k < slotEntryName.Count(); k++)
        {
            MainFunctionPart1(k);
        }
        MainButtonFunction(0);
    }
    private void MainFunctionPart1(int i)
    {
        GameObject tempGameObject = Instantiate(Slots, prefabContainer.transform);
        tempGameObject.GetComponentsInChildren<Image>()[1].sprite = showCaseImage[i];
        tempGameObject.GetComponentInChildren<TextMeshProUGUI>().text = slotEntryName[i];
        Button aButton = tempGameObject.GetComponentInChildren<Button>();
        aButton.onClick.AddListener(delegate { MainButtonFunction(i);}); 

    }
    void MainButtonFunction(int i)
    {
        characterPannel.GetComponentsInChildren<Image>()[1].sprite = showCaseImage[i];
        characterPannel.GetComponentInChildren<TextMeshProUGUI>().text = slotEntryDescription[i];
        pricePannel.GetComponentInChildren<TextMeshProUGUI>().text = price[i].ToString();
        currentSelect += 1;
        int tempNo = currentSelect;
        purchaseButton.onClick.AddListener(delegate { PurchaseFunction(price[i], tempNo); });
    }
    void PurchaseFunction(int no, int no2)
    {
        int currentMoney = PlayerPrefs.GetInt("money");
        if (currentMoney > no && no2 == currentSelect)
        {
            int num = currentMoney - no;
            PlayerPrefs.SetInt("money", num);
            MoneyGetter();
        }
    }
    private void MoneyGetter()
    {
        TextMeshProUGUI moneyText = moneyPannel.GetComponentInChildren<TextMeshProUGUI>();

        int currentMonney = PlayerPrefs.GetInt("money");
        moneyText.text = currentMonney.ToString();
    }
    private void DestroyAllChildren(GameObject objecto)
    {
        foreach (Transform obj in objecto.transform)
        {
            Destroy(obj.gameObject);

        }
    }
}

