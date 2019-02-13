using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCnote : MonoBehaviour {

    public string message;
    private Animator anim;
    public GameObject box;
    autoType childScript;
    Text myText;
    bool inZone;

    // Use this for initialization
    void Start () {
        box = GameObject.Find("speechBox");
        box.transform.localScale = new Vector3(0, 0, 0);
        anim = GetComponent<Animator>();
        childScript = GameObject.Find("speechText").GetComponent<autoType>();
        myText = GameObject.Find("speechText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        anim.SetBool("inZone", inZone);
	}

    float CalculateLengthOfMessage(string message)
    {
        float totalLength = 0;

        Font myFont = GameObject.Find("speechText").GetComponent<TextMesh>().font;
        CharacterInfo characterInfo = new CharacterInfo();

        char[] arr = message.ToCharArray();

        foreach (char c in arr)
        {
            myFont.RequestCharactersInTexture(c.ToString(), GameObject.Find("speechText").GetComponent<TextMesh>().font.fontSize, GameObject.Find("speechText").GetComponent<TextMesh>().fontStyle);
            myFont.GetCharacterInfo(c, out characterInfo, GameObject.Find("speechText").GetComponent<TextMesh>().font.fontSize);
            Debug.Log(characterInfo.advance);
            totalLength += (float) characterInfo.advance;
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

