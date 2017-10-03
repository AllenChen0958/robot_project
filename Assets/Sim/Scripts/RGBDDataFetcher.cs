using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class RGBDDataFetcher : PostEffectsBase {

    [SerializeField]
    private Material m_DepthMaterial;

    [SerializeField]
    private RenderTexture m_ScreenTexture;
    [SerializeField]
    private RenderTexture m_DepthTexture;

    private Texture2D m_BufferTexture2D;
    private Rect m_RenderTextureRect;

    [SerializeField]
    private bool m_IsRenderToScreen = true;
    [SerializeField]
    private bool m_UseDepth = false;

	private bool m_UseSegmentaion;

	public bool IsUseDepth() {
		return m_UseDepth;
	}

    public int GetDepthTextureSize()
    {
        return m_DepthTexture.width * m_DepthTexture.height * Marshal.SizeOf(typeof(Color));
    }

    public int GetRGBTextureSize()
    {
        return m_ScreenTexture.width * m_ScreenTexture.height * Marshal.SizeOf(typeof(Color));
    }

    public void GetRGBObservation(ref byte[] bytes)
    {
        getRenderTextureByteData(m_ScreenTexture, ref bytes);
    }

    public void GetDepthObservation(ref byte[] bytes)
    {
        getRenderTextureByteData(m_DepthTexture, ref bytes);
    }

    new void Start()
    {
        Debug.Assert(m_ScreenTexture.width == m_DepthTexture.width && m_ScreenTexture.height == m_DepthTexture.height);

        base.Start();
        m_BufferTexture2D = new Texture2D(m_ScreenTexture.width, m_ScreenTexture.height);
        m_RenderTextureRect = new Rect(0, 0, m_BufferTexture2D.width, m_BufferTexture2D.height);
		m_UseSegmentaion = this.GetComponent<SegmentationScript>().enable;
		//m_UseSegmentaion = false;
	}

    void OnEnable()
    {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
    }

    void OnDisable()
    {
        GetComponent<Camera>().depthTextureMode &= ~DepthTextureMode.Depth;
    }

    void OnDestroy()
    {
        m_BufferTexture2D = null;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (m_DepthMaterial && m_UseDepth)
        {
			if (m_UseDepth) {
				Graphics.Blit (source, m_DepthTexture, m_DepthMaterial);
				//Graphics.Blit(source, destination, m_DepthMaterial);

			}
        }
		if(m_UseSegmentaion == false) //if not using segmentaion then blit the original image to m_screenTexture
        	Graphics.Blit(source, m_ScreenTexture);

        if (m_IsRenderToScreen)
        {
			if(m_UseDepth)
				Graphics.Blit(m_DepthTexture, destination);
			else
				Graphics.Blit(m_ScreenTexture, destination);
        }
    }

    override public bool CheckResources()
    {
        return isSupported;
    }

    private void getRenderTextureByteData(RenderTexture renderTexture, ref byte[] bytes)
    {
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        m_BufferTexture2D.ReadPixels(m_RenderTextureRect, 0, 0);
        RenderTexture.active = currentActiveRT;

        Color[] colors = m_BufferTexture2D.GetPixels(0, 0, renderTexture.width, renderTexture.height);

        ColorArrayToByteArray(ref colors, ref bytes);
    }

    static public byte[] ColorArrayToByteArray(ref Color[] colors, ref byte[] bytes)
    {
        if (colors == null || colors.Length == 0)
            return null;

        int length = bytes.Length;

        GCHandle handle = default(GCHandle);
        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            Marshal.Copy(ptr, bytes, 0, length);
        }
        finally
        {
            if (handle != default(GCHandle))
                handle.Free();
        }

        return bytes;
    }
}
