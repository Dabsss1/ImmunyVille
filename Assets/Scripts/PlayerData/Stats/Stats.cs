using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float confidence;
    public float strength;
    public float body;
    public bool goodCondition;

    public float maxStats;

    public static Stats Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public bool MaxedStats()
    {
        if (health < maxStats)
            return false;
        else if (confidence < maxStats)
            return false;
        else if (strength < maxStats)
            return false;
        else if (body < maxStats)
            return false;
        else
            return true;
    }
    public void ResetData()
    {
        health = 0;
        confidence = 0;
        strength = 0;
        body = 0;
        goodCondition = true;
    }

    public bool ZeroStats()
    {
        if (health > 0)
            return false;
        else if (strength > 0)
            return false;
        else if (body > 0)
            return false;
        else
            return true;
    }

    public void DrainStats()
    {
        if (health > 0)
            health -= 0.083f;
        else
            health = 0;

        if (strength > 0)
            strength -= 0.083f;
        else
            strength = 0;

        if (confidence > 0)
            confidence -= 0.083f;
        else
            confidence = 0;

        if (body > 0)
            body -= 0.083f;
        else
            body = 0;
    }

    public void DeductStats(float h, float c, float s, float b)
    {
        if (health > h)
            health -= h;
        else
            health = 0;

        if (strength > s)
            strength -= s;
        else
            strength = 0;

        if (confidence > c)
            confidence -= c;
        else
            confidence = 0;

        if (body > b)
            body -= b;
        else
            body = 0;
    }
}
