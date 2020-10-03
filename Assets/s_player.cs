using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class s_player : MonoBehaviour
{
    public float Accel = 10.0f;
    public float MaxVel = 20.0f;
    public s_weaponManager WeaponManager;
    public Material GhostMaterial;
    private s_weaponManager m_weaponManager = null;
    private GameObject m_playbackStorage = null;
    private PlaybackInfo m_playbackInfo = null;
    private bool m_bIsRecording = false;
    private int m_playerIndex = -1;
    private float m_spawnTime = 0.0f;
    private int m_currentPlaybackIndex = 0;

    // Start is called before the first frame update
    void Start()
    {        
        m_weaponManager = Instantiate(WeaponManager, transform);
    }

    public void SetPlayerIndex(int index)
    {
        m_spawnTime = Time.fixedTime;
        m_playerIndex = index;
        m_playbackStorage = GameObject.FindGameObjectWithTag("PlaybackStorage");
        if (m_playbackStorage != null )
        {
            s_playbackStorage PlaybackStorageScript = m_playbackStorage.GetComponent<s_playbackStorage>();
            if (!PlaybackStorageScript.HasPlaybackData(m_playerIndex))
            {
                m_bIsRecording = true;
                m_playbackInfo = PlaybackStorageScript.CreatePlayback();
            }
            else
            {
                GetComponent<SpriteRenderer>().material = GhostMaterial;
                m_playbackInfo = PlaybackStorageScript.GetPlayback(m_playerIndex);
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_bIsRecording)
        {
            UpdatePlayerMovement();
        }
        else
        {
            UpdateRecordedMovement();
        }
    }

    private void UpdatePlayerMovement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float rightInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(rightInput, forwardInput, 0);
        direction.Normalize();
        transform.position += direction * 5 * Time.fixedDeltaTime;

        Plane plane = new Plane(Vector3.forward, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseWorldPos = new Vector3(0, 0, 0);
        if (plane.Raycast(ray, out distance))
        {
            mouseWorldPos = ray.GetPoint(distance);
        }

        Vector3 vTowards = mouseWorldPos - transform.position;
        vTowards.Normalize();
        transform.up = vTowards;

        if (Input.GetAxis("Fire1") != 0)
        {
            m_weaponManager.TryFiring();
        }
        RecordTransform();
    }

    private void UpdateRecordedMovement()
    {
        float relativeTime = Time.fixedTime - m_spawnTime;
        while (m_currentPlaybackIndex < m_playbackInfo.PlaybackKeys.Count && m_playbackInfo.PlaybackKeys[m_currentPlaybackIndex].Timestamp < relativeTime)
        {
            PlaybackKeyframe currentKeyframe = m_playbackInfo.PlaybackKeys[m_currentPlaybackIndex];
            m_currentPlaybackIndex++;
            transform.position = currentKeyframe.PositionKey;
            transform.rotation = currentKeyframe.RotationKey;
            if (currentKeyframe.FireKey)
            {
                m_weaponManager.TryFiring();
            }
        }
    }

    private void RecordTransform()
    {
        PlaybackKeyframe newKeyframe = new PlaybackKeyframe();
        newKeyframe.PositionKey = transform.position;
        newKeyframe.RotationKey = transform.rotation;
        newKeyframe.FireKey = Input.GetAxis("Fire1") != 0;
        newKeyframe.Timestamp = Time.fixedTime - m_spawnTime;

        if (m_playbackInfo.PlaybackKeys.Count == 0 || !m_playbackInfo.PlaybackKeys[m_playbackInfo.PlaybackKeys.Count - 1].IsEqual(newKeyframe))
        {
            m_playbackInfo.PlaybackKeys.Add(newKeyframe);
        }
    }
}
