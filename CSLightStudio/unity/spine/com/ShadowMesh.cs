using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowMesh : MonoBehaviour
{

    // Use this for initialization

    void Start()
    {
        //StartShadow(9);
    }
    List<GameObject> shadows = new List<GameObject>();
    // Update is called once per frame
    public float shadowStep = 1 / 30.0f;
    public Color shadowColor = new Color(0.3f, 0.3f, 1.0f, 0.5f);

    public bool bShadowEnable
    {
        get;
        private set;
    }
    public void StartShadow(int shadowCount)
    {
        bShadowEnable = true;

        while (shadows.Count < shadowCount)
        //for (int i = 0; i < shadowCount; i++)
        {
            //Transform t = this.transform.FindChild("_shadow_" + i.ToString());
            //if (t == null)
            {
                GameObject s = new GameObject();
                s.AddComponent<MeshFilter>().mesh = this.GetComponent<MeshFilter>().mesh;
                s.AddComponent<MeshRenderer>();
                s.name = "_shadow_";// +i.ToString();
                s.hideFlags = HideFlags.HideInHierarchy;
                //s.transform.parent = this.transform;
                //var t = s.transform;
                s.SetActive(false);
                shadows.Add(s);
            }
        }

    }
    public void EndShadow()
    {
        bShadowEnable = false;
    }
    float stepTimer = 0;
    void Update()
    {
        stepTimer += Time.deltaTime;
        if (stepTimer > shadowStep&&shadows.Count>0)
        {
            GameObject s = shadows[0];
            Vector3 pos = transform.position;
            pos.z += 1;
            if (bShadowEnable)
            {
                s.transform.position = pos;
                UpdateMesh(this.gameObject, s);
                s.transform.localScale = transform.localScale;
                s.SetActive(true);
            }
            else
            {
                s.SetActive(false);
            }
            shadows.RemoveAt(0);
            shadows.Add(s);
            stepTimer = 0;
        }
        foreach (var _s in shadows)
        {
            if (_s.activeSelf == false) continue;
            Vector3 _pos = _s.transform.position;
            _pos.z -= Time.deltaTime * 0.01f;
            _s.transform.position = _pos;
            foreach (var m in _s.GetComponent<MeshRenderer>().materials)
            {
                Color c = m.color;
                c.a -= Time.deltaTime * 0.25f;
                if (c.a < 0)
                {
                    c.a = 0;
                    _s.SetActive(false);
                }
                m.color = c;
            }

        }


    }
    void UpdateMesh(GameObject last, GameObject self)
    {
        self.GetComponent<MeshFilter>().mesh = last.GetComponent<MeshFilter>().mesh;
        List<Material> mats = new List<Material>();
        foreach (var mat in last.GetComponent<MeshRenderer>().materials)
        {
            Material nmat = new Material(mat);
            nmat.shader = Shader.Find("Spine/SkeletonTrans");
            nmat.color = shadowColor;
            //nmat.SetFloat("White", 1.0f);
            mats.Add(nmat);
        }
        self.GetComponent<MeshRenderer>().materials = mats.ToArray();
    }
}
