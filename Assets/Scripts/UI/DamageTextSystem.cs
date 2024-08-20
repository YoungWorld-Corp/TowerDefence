using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct DamageTextInfo
{
    public int damage;
    public GameObject mob;
    public float show_duration;
    public float remain_duration;
    public Vector3 last_position; // Use when mob is dead 
    public DamageTextInfo(int damage, GameObject mob, Vector3 init_position, float show_duration = 0.5f)
    {
        this.damage = damage;
        this.mob = mob;
        this.show_duration = show_duration;
        this.remain_duration = show_duration;
        this.last_position = init_position;
    }
}

public class DamageTextSystem : MonoBehaviour
{
    public GameObject TextPrefab;
    public Camera mainCamera;

    private List<DamageTextInfo> _damageInfos;
    private List<DamageTextInfo> _damageInfosBackup;

    private List<GameObject> _textPool;
    private int TEXT_POOL_SIZE = 500;
    private int TEXT_PADDING_Y = 80;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntializeTextPool();

        _damageInfos = new List<DamageTextInfo>();
        _damageInfosBackup = new List<DamageTextInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDamageTexts();
    }

    void IntializeTextPool()
    {
        _textPool = new List<GameObject>();

        for (int i=0; i<TEXT_POOL_SIZE; i++)
        {
            GameObject TextObject = Instantiate(TextPrefab, new UnityEngine.Vector3(0,0,0), Quaternion.identity);
            TextObject.name = "PooledDamageText_" + i;
            TextObject.transform.SetParent(gameObject.transform);
            TextObject.SetActive(false);
            _textPool.Add(TextObject);
        }
    }

    // this function is called from Mob
    public void AddDamageInfo(DamageTextInfo damageTextInfo)
    {
        _damageInfos.Add(damageTextInfo);
    }

    private void UpdateDamageTexts()
    {
        int NumDamageInfo = _damageInfos.Count;
        int NumTextPool = _textPool.Count;

        for (int i=0; i<NumTextPool; i++)
        {
            if (i < NumDamageInfo)
            {
                GameObject TextObject = _textPool[i];
                DamageTextInfo DamageInfo = _damageInfos[i];
                Vector3 TextPosition = DamageInfo.last_position;
                float yPadding = GetTextYPadding(DamageInfo.remain_duration, DamageInfo.show_duration);

                if (!DamageInfo.mob || DamageInfo.mob.IsDestroyed())
                {
                    // use last_position
                }
                else
                {
                    Vector3 screenPosition = mainCamera.WorldToScreenPoint(DamageInfo.mob.transform.position);
                    TextPosition = new Vector3(screenPosition.x, screenPosition.y, 0);
                    DamageInfo.last_position = TextPosition;
                }

                TextObject.transform.position = new Vector3(TextPosition.x , TextPosition.y + yPadding, 0);

                TextObject.GetComponent<TMPro.TextMeshProUGUI>().text = DamageInfo.damage.ToString();
                TextObject.GetComponent<TMPro.TextMeshProUGUI>().alpha = GetTextAlpha(DamageInfo.remain_duration, DamageInfo.show_duration);

                TextObject.SetActive(true);
                DamageInfo.remain_duration -= Time.deltaTime;
                if (DamageInfo.remain_duration > 0)
                {
                    // remain next frame
                    _damageInfosBackup.Add(DamageInfo);
                }
            }
            else // do not use current frame
            {
                _textPool[i].SetActive(false);
            }
        }

        Swap(ref _damageInfos, ref _damageInfosBackup);
        _damageInfosBackup.Clear();
    }

    public float GetTextYPadding(float remain_duration, float show_duration)
    {
        float duration_ratio = remain_duration / show_duration;
        float yPadding = TEXT_PADDING_Y + (1-duration_ratio) * 40;
        return yPadding;
    }
    public float GetTextAlpha(float remain_duration, float show_duration)
    {
        float alpha = remain_duration / show_duration;
        return alpha;
    }

    private void Swap<T>(ref List<T> first, ref List<T> second)
    {
        List<T> temp = first;
        first = second;
        second = temp;
    }
}
