using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
//new things will be added in itemdata/sidemenu/topMenu and filler methods in bottom of this script 
public class LoadingStoreData : MonoBehaviour
{
    [Header("JSON")]
    [SerializeField] TextAsset localData;
    [SerializeField] TextAsset jsonDataOnline;
    TextAsset jsonData;
    TopMenuContainerClass word = new TopMenuContainerClass();
    [Serializable]class itemData
    {
        public string eName;
        public string eImg;
        public int ePrice;
        public int edamage;
        public string edesc;

    }
    [Serializable]class SideMenu
    {
        public string itemName;
        public string itemImage;
        public int itemIndex;
        public itemData[] itemElements;
    }
    [Serializable]class TopMenu
    {
        public string pName;
        public string pImg;
        public int pOrder;
        public SideMenu[] pItems;
    }
    [Serializable]class TopMenuContainerClass
    {
        public TopMenu[] element1;
    }
    [Header("ImageDownload")]
    Texture2D textureOut;
    List<List<int>> spriteIndex = new List<List<int>>();
    List<List<string>> imageURlHolder= new List<List<string>>();
    List<List<Texture2D>> textureMainContainer= new List<List<Texture2D>>();
    List<List<Sprite>> spriteMainContainer= new List<List<Sprite>>();
    [Header("Image Local storage")]
    string imagePath;
    SceneManagerScript sceneManagerScript;
    private void Awake()
    {
        sceneManagerScript=FindObjectOfType<SceneManagerScript>();
        DontDestroyOnLoad(this.gameObject);
    }
    public void StartTheLoading()
    {
        if (PlayerPrefs.GetInt("fromLocal") == 2)
        {
            //local datahere
            jsonData = localData;
            Debug.Log("LocalImagesLoad");
            imagePath = "/store Image/";
            
            SpriteIndexAndImageHolderFiller();
            LocalImageLoader(Application.dataPath+imagePath);
            ImageListFiller();
            sceneManagerScript.StoreLoadScene();

        }
        else if (PlayerPrefs.GetInt("fromLocal") == 1)
        {
            string[] jsonList = Directory.GetFiles(Path.Combine(Application.persistentDataPath + "/jsonFolder"), "*.json");
            if (jsonList.Length == 1)
            {
                string path = (Application.persistentDataPath + "/jsonFolder/" + "/FirstJson.json");
                jsonData = new TextAsset(File.ReadAllText(path));
            }
            else if (jsonList.Length == 2)
            {
                string path = (Application.persistentDataPath + "/jsonFolder/" + "/SecondJson.json");
                jsonData = new TextAsset(File.ReadAllText(path));
            }
            imagePath = "/store Image/";
            SpriteIndexAndImageHolderFiller();
            if (FileChecker())
            {
                LocalImageLoader(Application.persistentDataPath + imagePath);
                ImageListFiller();
                sceneManagerScript.StoreLoadScene();
            }

            else
            {
               // ImageDeletingFunction();
                ImageDownloader();
            }
        }
    }
    public TextAsset GetJson()
    {
        return jsonData;
    }
    void ImageDeletingFunction()
    {
        string path = Path.Combine(Application.persistentDataPath + imagePath);
        string[] files = Directory.GetFiles(path, "*.png");
        foreach (var images in files)
        {
            File.Delete(images);
        }
    }
    public List<List<Sprite>> GetSpritesForShop()
    {
        return spriteMainContainer;
    }

    bool FileChecker()
    {
        try
        {
            string path =Path.Combine( Application.persistentDataPath + imagePath);
            string[] files = Directory.GetFiles(path, "*.png");

            for (int i = 0; i < imageURlHolder.Count; i++)
            {
                for (int j = 0; j < imageURlHolder[i].Count; j++)
                {
                    string filePath = Path.Combine(Application.persistentDataPath + imagePath + i.ToString() +"##"+ j.ToString() + ".png");
                    if (File.Exists(filePath))
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    void LocalImageLoader(string applicationPath) 
    {
        string path = Path.Combine(applicationPath);
        string[] files = Directory.GetFiles(path, "*.png");

        for (int i = 0; i < imageURlHolder.Count; i++)
        {
            List<Texture2D> texxture = new List<Texture2D>();
            for (int j = 0; j < imageURlHolder[i].Count; j++)
            {
                string filePath =Path.Combine(applicationPath+ i.ToString() + "##"+ j.ToString() + ".png");
                if (File.Exists(filePath))
                {
                    byte[] imageData = File.ReadAllBytes(filePath);

                    // Create a new Texture2D object
                    Texture2D texture = new Texture2D(2, 2);

                    // Load the image data into the texture
                    texture.LoadImage(imageData);
                    texxture.Add(texture);                               
                }
                
            }
            textureMainContainer.Add(texxture);
        }

    }
    void ImageLocallySaver()
    {
        string path = Path.Combine(Application.persistentDataPath + imagePath);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        for (int i=0;i<textureMainContainer.Count;i++)
        {
            for (int j = 0; j < textureMainContainer[i].Count; j++)
            {
                byte[] bytes = textureMainContainer[i][j].EncodeToPNG();
                File.WriteAllBytes(Path.Combine(Application.persistentDataPath+imagePath+i.ToString() +"##" + j.ToString()+".png"), bytes);
            }
        }
    }


    void ImageListFiller()
    {
        foreach (List<Texture2D> texturelist in textureMainContainer)
        {
            List<Sprite> tempSpriteList = new List<Sprite>();
            foreach (Texture2D texture1 in texturelist)
            {
                Sprite sprite = Sprite.Create(texture1, new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f));
                tempSpriteList.Add(sprite);
            }
            spriteMainContainer.Add(tempSpriteList);
        }

    }

    private void ImageDownloader()
    {
         OnlineImagerGetter();      
    }
    private void OnlineImagerGetter()
    {
        Get(
        (Texture2D success) =>
        {
            Texture2D temptex = success;
        },
        (string failure) => { Debug.Log("Error" + failure); }
        );
    }
    private void Get( Action<Texture2D> onSuccess, Action<string> onFailure)
    {
        StartCoroutine(GetCourotine( onSuccess, onFailure));
    }

    IEnumerator GetCourotine(Action<Texture2D> onSuccess, Action<string> onFailure)
    {
        foreach (var urlcontainer in imageURlHolder)
        {
            List<Texture2D> texxture = new List<Texture2D>();
            foreach (var url in urlcontainer)
            {
                using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url))
                {
                    yield return unityWebRequest.SendWebRequest();
                    if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
                    {
                        onFailure(unityWebRequest.error);
                    }
                    else
                    {
                        DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
                        Texture2D temptex = downloadHandlerTexture.texture;
                        texxture.Add(temptex);
                        onSuccess(textureOut = downloadHandlerTexture.texture);

                    }
                }
            }
            textureMainContainer.Add(texxture);
        }
        ImageLocallySaver();
        ImageListFiller();
        sceneManagerScript.StoreLoadScene();
    }
    void SpriteIndexAndImageHolderFiller()
    {

        word = JsonUtility.FromJson<TopMenuContainerClass>(jsonData.text);
        foreach (var topMenu in word.element1)
        {
            SideMenuFiller(topMenu.pOrder);
        }

    }
    void SideMenuFiller(int topMenu)
    {

        foreach (var sideMenu in word.element1[topMenu].pItems)
        {
            List<int> tempList = new List<int>();
            tempList.Add(topMenu);
            tempList.Add(sideMenu.itemIndex);
            spriteIndex.Add(tempList);
            MainMenuFiller(topMenu, sideMenu.itemIndex);
        }

    }
    void MainMenuFiller(int topMenu, int sideIndex)
    {
        var b = word.element1[topMenu].pItems[sideIndex];
        List<string> tempString = new List<string>();
        foreach (var c in b.itemElements)
        {
            tempString.Add(c.eImg);
        }
        imageURlHolder.Add(tempString);
    }
}

