using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject weaponMenu;
    public GameObject mainMenu;



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
        SceneManager.LoadScene("testscript");
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

    private void SaveChange()
    {

    }

}
