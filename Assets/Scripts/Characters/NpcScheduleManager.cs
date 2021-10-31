using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScheduleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class NpcSchedule
{
    public CharacterController npc;
    public int timeIn;
    public int timeOut;
}
