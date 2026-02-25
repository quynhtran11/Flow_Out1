using UnityEngine;

public interface IProperties
{
    public void Initilize<T, R>(BaseElement<T, R> data) where T : BaseElementVisual<R>;
}