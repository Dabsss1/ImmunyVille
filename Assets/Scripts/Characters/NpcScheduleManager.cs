using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScheduleManager : MonoBehaviour
{
    [Header("Settings")]
    public bool outdoor;
    public Transform doorPortal;

    [Header("CustomPortals")]
    public List<Transform> entrances;

    [Header("Npcs")]
    public List<NpcSchedule> schedules;

    public List<SpawnedNpc> spawnedNpcs;


    public NpcScheduleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
            Instance = this;
    }

    private void OnEnable()
    {
        TimeManager.OnHourChanged += EnterNpc;
    }

    private void OnDestroy()
    {
        TimeManager.OnHourChanged -= EnterNpc;
    }

    void EnterNpc()
    {
        if (outdoor)
            StartCoroutine(EnterNpcsOutdoor());
        else
            StartCoroutine(EnterNpcs());
    }

    IEnumerator EnterNpcs()
    {
        foreach (NpcSchedule schedule in schedules)
        {
            if (TimeManager.Instance.hour == schedule.timeIn)
            {
                SpawnedNpc spawnedNpc = new SpawnedNpc(schedule);

                spawnedNpc.npc = Instantiate(spawnedNpc.npc, doorPortal);

                spawnedNpcs.Add(spawnedNpc);

                StartCoroutine(spawnedNpc.npc.MoveNpc(schedule.movement, schedule.faceDir));
                yield return null;
            }
        }

        foreach (SpawnedNpc npc in spawnedNpcs)
        {
            if (npc.timeOut == TimeManager.Instance.hour)
            {
                StartCoroutine(npc.npc.MoveExit(npc.movement));
                yield return null;
            }
        }
    }

    IEnumerator EnterNpcsOutdoor()
    {
        foreach (NpcSchedule schedule in schedules)
        {
            if (TimeManager.Instance.hour == schedule.timeIn)
            {
                foreach(Transform entrance in entrances)
                {
                    if(entrance.gameObject.name == schedule.customEntrance)
                    {
                        SpawnedNpc spawnedNpc = new SpawnedNpc(schedule,schedule.exitMovement);

                        spawnedNpc.npc = Instantiate(spawnedNpc.npc, entrance);

                        spawnedNpcs.Add(spawnedNpc);

                        StartCoroutine(spawnedNpc.npc.MoveNpc(schedule.movement, schedule.faceDir));
                        yield return null;
                    }
                }
            }
        }

        foreach (SpawnedNpc npc in spawnedNpcs)
        {
            if (npc.timeOut == TimeManager.Instance.hour)
            {
                StartCoroutine(npc.npc.MoveExit(npc.movement));
                yield return null;
            }
        }
    }

    private void Start()
    {
        foreach(NpcSchedule schedule in schedules)
        {
            if (TimeManager.Instance.hour >= schedule.timeIn && TimeManager.Instance.hour < schedule.timeOut)
            {
                SpawnedNpc spawnedNpc = new SpawnedNpc(schedule);

                spawnedNpc.npc = Instantiate(spawnedNpc.npc, doorPortal);

                spawnedNpc.npc.transform.localPosition = schedule.TargetPosition();
                spawnedNpc.npc.setFaceDir(schedule.faceDir);

                spawnedNpcs.Add(spawnedNpc);
            }
        }
    }
}


[System.Serializable]
public class NpcSchedule
{
    public CharacterController npc;
    public string customEntrance;
    public int timeIn;
    public int timeOut;
    public List<Vector2> movement;
    public List<Vector2> exitMovement;
    public Vector2 faceDir;

    public Vector3 TargetPosition()
    {
        float x=0;
        float y=0;

        foreach(Vector2 move in movement)
        {
            x += move.x;
            y += move.y;
        }

        return new Vector3(x, y, 0);
    }
}

[System.Serializable]
public class SpawnedNpc
{
    public CharacterController npc;
    public int timeOut;

    public List<Vector2> movement;

    public SpawnedNpc(NpcSchedule schedule)
    {
        npc = schedule.npc;
        timeOut = schedule.timeOut;

        movement = ReverseMove(schedule.movement);
    }

    public List<Vector2> ReverseMove(List<Vector2> movement)
    {
        List<Vector2> reversedMovement = new List<Vector2>();

        foreach (Vector2 move in movement)
        {
            reversedMovement.Insert(0, new Vector2(move.x * -1, move.y * -1));
        }

        return reversedMovement;
    }

    public SpawnedNpc(NpcSchedule schedule, List<Vector2> exitMovement)
    {
        npc = schedule.npc;
        timeOut = schedule.timeOut;

        movement = exitMovement;
    }
}
