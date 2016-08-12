using UnityEngine;
using System.Collections;
using UI;

public class DirLightManager : MonoBehaviour {
	private GameObject obj;

	// Use this for initialization
	void Start () {
		obj = gameObject;
	}
	
	
	void Update () {
		if(	(UIData.Instance.atomtype == UIData.AtomType.particleball) || ((UIData.Instance.atomtype == UIData.AtomType.hyperball) || (UIData.Instance.atomtype == UIData.AtomType.noatom) 
			&& (UIData.Instance.bondtype == UIData.BondType.nobond) || (UIData.Instance.bondtype == UIData.BondType.line) || (UIData.Instance.bondtype == UIData.BondType.hyperstick)) )
		{
			obj.GetComponent<Light>().shadows = LightShadows.None;
		}
		else
		{
			obj.GetComponent<Light>().shadows = LightShadows.Soft;			
		}
	}
}
