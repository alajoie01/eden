using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoType : MonoBehaviour {

    IEnumerator AutoType(string message)
    {
        yield return new WaitForSeconds(0.3f);
        foreach (char letter in message.ToCharArray())
        {
            GetComponent<TextMesh>().text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator AutoDelete()
    {
        GetComponent<TextMesh>().text = "";
        yield return new WaitForSeconds(0);
    }
}
