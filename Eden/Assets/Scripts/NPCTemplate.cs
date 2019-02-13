using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPCTemplate : ScriptableObject
{

    public string realName;
    public string npcName;
    public GameObject npcSprite = null;

    public string message;

}
