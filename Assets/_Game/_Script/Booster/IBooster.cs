using System;
public interface IBooster {
    public EBoosterType boosterType { get; }
    public void OnInit();
    public void OnUpdate(Action callBack);
    public void OnExit();
    public void OnClick();
}