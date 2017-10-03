using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(RGBDDataFetcher))]
public class RGBObservationProducer : ObservationProducer {

    private byte[] m_RGBBuffer;

    private RGBDDataFetcher m_RGBDataFetcher;

	private bool m_UseDepth = false;

    public override byte[] GetObservation()
    {
		if (m_UseDepth)
			m_RGBDataFetcher.GetDepthObservation(ref m_RGBBuffer);
		else
			m_RGBDataFetcher.GetRGBObservation(ref m_RGBBuffer);
		//m_RGBDataFetcher.GetRGBObservation(ref m_RGBBuffer);
        return m_RGBBuffer;
    }

    public override void GetObservation(out byte[] buffer)
    {
        buffer = m_RGBBuffer;
		if (m_UseDepth)
			m_RGBDataFetcher.GetDepthObservation(ref buffer);
		else
			m_RGBDataFetcher.GetRGBObservation(ref buffer);

    }

    public override int GetObservationSize()
    {
        return m_RGBDataFetcher.GetRGBTextureSize();
    }

    void Start ()
    {
        m_RGBDataFetcher = GetComponent<RGBDDataFetcher>();
        m_RGBBuffer = new byte[m_RGBDataFetcher.GetRGBTextureSize()];
		RGBDDataFetcher fetcher = GetComponent<RGBDDataFetcher> ();
		if (fetcher != null) {
			m_UseDepth = fetcher.IsUseDepth ();
		} else{
			Debug.LogError ("RGBDDataFetcher is not defined");
		}
	}
	
}
