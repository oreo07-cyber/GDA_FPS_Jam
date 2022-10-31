using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class BaseForm : ScriptableObject
{

    public enum EnergyUsage { Unlimited, CooldownMag, SingleMag };
    public enum EnergyRegenerationType { ConstantFullFill, ConstantIncremental, EmptyFullFill };
    public enum FireType { Auto, Semi, Hold };

    [Header("Base Form Data")]
    public string id;
    public string description;
    public Sprite icon;
    public Transform pivot;
    public FireType firingType;
    public float actionCooldown;
    public float maxHoldDuration;

    [Header("Energy Data")]
    public EnergyUsage energyType;
    public EnergyRegenerationType energyRegenType;
    public float energyRegenCooldown;
    public float energyRegenRate;
    public float energyMax;
    public float energyCost;




    public virtual void FormAction(float context)
    {

    }

    public virtual void InitializeForm(Transform input)
    {
        pivot = input;
    }



}

