using System;

public class AreaBuff
{
    public AreaInfo AreaInfo { get; protected set; }
    public Action<AreaBuff> OnBuildingChanged { get; protected set; }
    public bool IsRemoved {  get; protected set; }

    public AreaBuff(AreaInfo areaInfo, Action<AreaBuff> onBuildingChanged = null)
    {
        AreaInfo = areaInfo;
        areaInfo.AddBuff(this);
        OnBuildingChanged += onBuildingChanged;
    }

    public void RemoveSelf()
    {
        IsRemoved = true;
        OnBuildingChanged = null;
    }
}