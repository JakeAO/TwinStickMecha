using System.Collections;
using Prime31.ZestKit;
using UnityEngine;

public abstract class ContextSceneRoot : MonoBehaviour
{
    private const float REGION_TWEEN_DURATION = 0.5f;

    [System.Serializable]
    public struct ContextRegionData
    {
        public RectTransform Root;
        public Vector3 TargetPosition;
        public Vector3 HiddenPosition;
    }

    public ContextRegionData[] Regions = new ContextRegionData[0];

    protected virtual void Awake()
    {
        foreach (var regionData in Regions)
        {
            regionData.Root.anchoredPosition3D = regionData.HiddenPosition;
        }
    }

    public Coroutine TriggerRegionEntry()
    {
        return StartCoroutine(coRegionEntry());
    }
    private IEnumerator coRegionEntry()
    {
        foreach (var regionData in Regions)
        {
            regionData.Root.anchoredPosition3D = regionData.HiddenPosition;
        }
        OnRegionEnter();
        foreach (var regionData in Regions)
        {
            regionData.Root.ZKanchoredPosition3DTo(regionData.TargetPosition, REGION_TWEEN_DURATION);
        }
        yield return new WaitForSeconds(REGION_TWEEN_DURATION);
    }

    public Coroutine TriggerRegionExit()
    {
        return StartCoroutine(coRegionExit());
    }
    private IEnumerator coRegionExit()
    {
        foreach (var regionData in Regions)
        {
            regionData.Root.anchoredPosition3D = regionData.TargetPosition;
        }
        foreach (var regionData in Regions)
        {
            regionData.Root.ZKanchoredPosition3DTo(regionData.HiddenPosition, REGION_TWEEN_DURATION);
        }
        yield return new WaitForSeconds(REGION_TWEEN_DURATION);

        OnRegionExit();
    }

    protected abstract void OnRegionEnter();
    protected abstract void OnRegionExit();
}
