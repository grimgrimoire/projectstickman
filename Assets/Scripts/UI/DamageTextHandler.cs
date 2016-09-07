using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageTextHandler : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public float duration = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowDamage(int damage, Vector2 position)
    {
        GameObject instance = Instantiate(damageTextPrefab);
        instance.transform.SetParent(GameObject.Find("UI").transform);
        instance.gameObject.transform.position = Camera.main.WorldToScreenPoint(position);
        instance.GetComponentInChildren<Text>().text = damage + "";
        StartCoroutine(DestroyText(instance));
    }

    IEnumerator DestroyText(GameObject text)
    {
        yield return new WaitForSeconds(duration);
        Destroy(text);
    }
}
