using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMain : MonoBehaviour
{

    private Animator anim;
    public GameObject box;
    autoType childScript;
    Text myText;

    public string message;

    bool inZone;

    // Use this for initialization
    void Start()
    {
        box = transform.GetChild(1).GetChild(1).gameObject;
        box.transform.localScale = new Vector3(0, 0, 0);
        anim = transform.GetChild(1).GetComponent<Animator>();
        childScript = transform.GetChild(1).GetChild(0).GetComponent<autoType>();
        myText = transform.GetChild(1).GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("inZone", inZone);
    }

    float CalculateLengthOfMessage(string message)
    {
        float totalLength = 0;

        Font myFont = transform.GetChild(1).GetChild(0).GetComponent<TextMesh>().font;
        CharacterInfo characterInfo = new CharacterInfo();

        char[] arr = message.ToCharArray();

        foreach (char c in arr)
        {
            myFont.RequestCharactersInTexture(c.ToString(), transform.GetChild(1).GetChild(0).GetComponent<TextMesh>().font.fontSize,
                transform.GetChild(1).GetChild(0).GetComponent<TextMesh>().fontStyle);
            myFont.GetCharacterInfo(c, out characterInfo, transform.GetChild(1).GetChild(0).GetComponent<TextMesh>().font.fontSize);
            totalLength += (float)characterInfo.advance;
        }

        return totalLength;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            inZone = true;
            childScript.StartCoroutine("AutoType", message);
            StartCoroutine("Wait", 0.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            inZone = false;
            childScript.StopCoroutine("AutoType");
            childScript.StartCoroutine("AutoDelete");
            box.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public IEnumerator Wait(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        float length = CalculateLengthOfMessage(message);
        box.transform.localScale = new Vector3((length * 0.033f), 1, 5);
    }
}
