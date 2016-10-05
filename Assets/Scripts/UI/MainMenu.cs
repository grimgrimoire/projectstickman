using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject weaponMenu;
    public GameObject mainMenu;
    public GameObject debugMenu;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Stage1Button()
    {
        SceneManager.LoadScene(ConstMask.SCENE_STAGE_1);
    }

    public void ShowLoadoutUI()
    {
        mainMenu.SetActive(false);
        weaponMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        weaponMenu.SetActive(false);
        SaveChange();
    }

    public void ShowDebugMenu()
    {
        mainMenu.SetActive(false);
        weaponMenu.SetActive(false);
        debugMenu.SetActive(true);
    }

    private void SaveChange()
    {

    }

}
