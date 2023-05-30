using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    [SerializeField] GameObject[] bases;

    [SerializeField] SkinnedMeshRenderer[] models;
    [SerializeField] MeshRenderer[] hairs, packs;

    public SkinnedMeshRenderer[] Models {  get { return models; } }
    public MeshRenderer[] Hairs { get { return hairs; } }
    public MeshRenderer[] Packs { get { return packs; } }

    void Awake()
    {
        models = bases[0].GetComponentsInChildren<SkinnedMeshRenderer>();
        hairs = bases[1].GetComponentsInChildren<MeshRenderer>();
        packs = bases[3].GetComponentsInChildren<MeshRenderer>();

        SetModel(0);
        SetHair(0);
        SetPack(0);
    }

    public bool SetModel(int num)
    {
        for (int j = 0; j < models.Length; j++)
        {
            if (j != num)
                models[j].gameObject.SetActive(false);
        }
        if (num < 0 || num >= models.Length)
            return false;
        for (int i = 0; i < models.Length; i++)
        {
            if (i == num)
            {
                models[i].gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public bool SetModel(string name)
    {
        for (int j = 0; j < models.Length; j++)
        {
            if (models[j].name != name)
                models[j].gameObject.SetActive(false);
        }
        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].name == name)
            {
                models[i].gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public bool SetHair(int num)
    {
        for (int j = 0; j < hairs.Length; j++)
        {
            if (j != num)
                hairs[j].gameObject.SetActive(false);
        }
        if (num < 0 || num >= hairs.Length)
            return false;
        for (int i = 0; i < hairs.Length; i++)
        {
            if (i == num)
            {
                hairs[i].gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public bool SetHair(string name)
    {
        for (int j = 0; j < hairs.Length; j++)
        {
            if (hairs[j].name != name)
                hairs[j].gameObject.SetActive(false);
        }
        for (int i = 0; i < hairs.Length; i++)
        {
            if (hairs[i].name == name)
            {
                hairs[i].gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public bool SetPack(int num)
    {
        for (int j = 0; j < packs.Length; j++)
        {
            if (j != num)
                packs[j].gameObject.SetActive(false);
        }
        if (num < 0 || num >= packs.Length)
            return false;
        for (int i = 0; i < packs.Length; i++)
        {
            if (i == num)
            {
                packs[i].gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public bool SetPack(string name)
    {
        for (int j = 0; j < packs.Length; j++)
        {
            if (packs[j].name != name)
                packs[j].gameObject.SetActive(false);
        }
        for (int i = 0; i < packs.Length; i++)
        {
            if (packs[i].name == name)
            {
                packs[i].gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
}
