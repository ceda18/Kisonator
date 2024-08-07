using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWritting : MonoBehaviour
{
    public GameObject block;
    TextMeshProUGUI textGO;
    float delayBeforeStart = 3f;
    float delayBtwChar = 0.05f;
    float delayBtwString = 2f;

    string writter = "";

    string[] a = {
        "Ko bi rekao da je prošlo već 10 godina od Velike suše...",
        "Jebem ti dan kada je ona kompanija otela i poslednju kap vode od nas, \"običnih\".",
        "Ja se razbijam od posla za gutljaj dok bogataši i cveće mogu da zalivaju.",
    "Tek su sad pronašli moju tajnu laboratoriju, ali poslednji deo Kišonatora je gotov.",
"Vreme je da vratim kišu, pravac Avala!"
    };

    void Start()
    {
        textGO = GetComponent<TextMeshProUGUI>();
        StartCoroutine(kucanje());
    }

    IEnumerator kucanje()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < a[i].Length; j++)
            {
                writter = writter + a[i][j].ToString();
                textGO.text = writter;
                yield return new WaitForSeconds(delayBtwChar);
            }
            yield return new WaitForSeconds(delayBtwString);
            writter = "";
        }
        block.SetActive(false);
    }
}