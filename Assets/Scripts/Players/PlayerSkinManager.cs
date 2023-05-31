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
        packs = bases[2].GetComponentsInChildren<MeshRenderer>();

        SetModel(0);
        for (int j = 0; j < hairs.Length; j++)
        {
            hairs[j].gameObject.SetActive(false);
        }
        for (int j = 0; j < packs.Length; j++)
        {
            packs[j].gameObject.SetActive(false);
        }
    }

    public bool SetModel(int num)
    {
        for (int i = 0; i < models.Length; i++)
        {
            if (i == num)
            {
                models[i].gameObject.SetActive(true);

                for (int j = 0; j < models.Length; j++)
                {
                    if(i != j)
                        models[j].gameObject.SetActive(false);
                }
                return true;
            }
        }
        return false;
    }

    public bool SetModel(string name)
    {
        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].name == name)
            {
                models[i].gameObject.SetActive(true);

                for (int j = 0; j < models.Length; j++)
                {
                    if(i != j)
                        models[j].gameObject.SetActive(false);
                }
                return true;
            }
        }
        return false;
    }

    public bool SetHair(int num)
    {
        for (int i = 0; i < hairs.Length; i++)
        {
            if (i == num)
            {
                hairs[i].gameObject.SetActive(true);

                for (int j = 0; j < hairs.Length; j++)
                {
                    if (i != j)
                        hairs[j].gameObject.SetActive(false);
                }
                return true;
            }
        }
        return false;
    }

    public bool SetHair(string name)
    {
        for (int i = 0; i < hairs.Length; i++)
        {
            if (hairs[i].name == name)
            {
                hairs[i].gameObject.SetActive(true);

                for (int j = 0; j < hairs.Length; j++)
                {
                    if(i != j)
                        hairs[j].gameObject.SetActive(false);
                }
                return true;
            }
        }
        return false;
    }

    public bool SetPack(int num)
    {
        for (int i = 0; i < packs.Length; i++)
        {
            if (i == num)
            {
                packs[i].gameObject.SetActive(true);

                for (int j = 0; j < packs.Length; j++)
                {
                    if(i != j)
                        packs[j].gameObject.SetActive(false);
                }
                return true;
            }
        }
        return false;
    }

    public bool SetPack(string name)
    {
        for (int i = 0; i < packs.Length; i++)
        {
            if (packs[i].name == name)
            {
                packs[i].gameObject.SetActive(true);

                for (int j = 0; j < packs.Length; j++)
                {
                    if(i != j)
                        packs[j].gameObject.SetActive(false);
                }
                return true;
            }
        }
        return false;
    }
}
