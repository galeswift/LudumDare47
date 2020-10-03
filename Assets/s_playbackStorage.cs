using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaybackKeyframe
{
    public float Timestamp;
    public Vector3 PositionKey;
    public Quaternion RotationKey;
    public bool FireKey = false;

    public bool IsEqual(PlaybackKeyframe other)
    {
        return PositionKey == other.PositionKey && RotationKey == other.RotationKey && FireKey == other.FireKey;
    }
}

public class PlaybackInfo
{
    public List<PlaybackKeyframe> PlaybackKeys = new List<PlaybackKeyframe>();
}

public class s_playbackStorage : MonoBehaviour
{
    private List<PlaybackInfo> m_playerbackInfo = new List<PlaybackInfo>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool HasPlaybackData(int index)
    {
        return index < m_playerbackInfo.Count;
    }

    public PlaybackInfo CreatePlayback()
    {
        PlaybackInfo result = new PlaybackInfo();
        m_playerbackInfo.Add(result);
        return result;
    }

    public PlaybackInfo GetPlayback(int index)
    {
        if (index < m_playerbackInfo.Count)
        {
            return m_playerbackInfo[index];
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
