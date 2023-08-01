using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class ArrowTime : MonoBehaviour
{
    private AlembicStreamPlayer[] m_StreamPlayer;
    private float m_Time;

    private void Start()
    {
        m_StreamPlayer = GetComponentsInChildren<AlembicStreamPlayer>();
    }

    private void OnEnable()
    {
        m_Time = 0;
        
    }

    private void Update()
    {
        switch (m_Time)
        {
            case > 1:
                Destroy(gameObject);
                break;
            case < 1:
                m_Time += (Time.deltaTime * 1.2f);
                break;
        }

        foreach (var player in m_StreamPlayer)
        {
            player.CurrentTime = m_Time;
        }
    }
}
