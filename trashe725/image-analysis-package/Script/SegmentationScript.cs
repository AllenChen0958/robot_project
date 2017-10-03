using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentationScript : MonoBehaviour {

	[HideInInspector]
	public Shader shader=null;

	private Camera maincam;
	private Camera dummycam;
	public RenderTexture target=null;
	public bool enable = true;




	void CreateDummyCamera(){
		GameObject dummy = new GameObject ();
		dummy.name = "HiddenSegmentationCamera";
		dummy.transform.SetParent (this.transform);
		dummy.transform.localPosition = Vector3.zero;
		dummy.transform.localRotation = Quaternion.identity;
		dummy.transform.localScale = Vector3.one;

		//dummy.hideFlags = HideFlags.HideInHierarchy;

		dummycam = dummy.AddComponent<Camera> ();

		dummycam.cullingMask = maincam.cullingMask;
		dummycam.aspect = maincam.aspect;
		dummycam.nearClipPlane = maincam.nearClipPlane;
		dummycam.farClipPlane = maincam.farClipPlane;
		dummycam.fieldOfView = maincam.fieldOfView;
		dummycam.targetTexture = target;
		dummycam.rect = maincam.rect;
		dummycam.depth = maincam.depth+1;
		dummycam.clearFlags = CameraClearFlags.Color;
		dummycam.backgroundColor = Color.black;
	}

	// Use this for initialization
	void Start () {

		maincam = this.GetComponent<Camera> ();

		if(shader == null)
			shader = Shader.Find ("Hidden/SegmentationShader");

		CreateDummyCamera ();

		dummycam.depthTextureMode = DepthTextureMode.DepthNormals;
		dummycam.SetReplacementShader (shader, "");

		UpdateMaterialPropertyBlock ();
	}

	void UpdateMaterialPropertyBlock(){
		var renderers = GameObject.FindObjectsOfType<Renderer> ();
		var prob = new MaterialPropertyBlock ();

		foreach (var r in renderers) {
			var tag = r.gameObject.tag;
			prob.SetColor ("_ObjectColor", TagsManager.GetColor (tag));
			r.SetPropertyBlock (prob);
		}
	}
	
	// Update is called once per frame
	void Update () {

		dummycam.enabled = enable;
		if(enable)
			UpdateMaterialPropertyBlock ();
	}

	public Camera GetDummyCamera(){
		return dummycam;
	}

	public RenderTexture TargetTexture{
		set{ 
			target = value;
			dummycam.targetTexture = target;
		}
		get{ return target; }
	}

	public void Render(){
		dummycam.Render ();
	}
}
