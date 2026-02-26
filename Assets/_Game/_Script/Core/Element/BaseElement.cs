using System.Collections.Generic;
using UnityEngine;

public abstract class BaseElement<T, R> : BLBMono where T : BaseElementVisual<R>
{
    [SerializeField] protected T visual;
    protected bool isBusy;
    public T Visual => visual;

    protected R data;
    public R Data => data;
    protected List<IProperties> allPros;
    public bool IsBusy => isBusy;

    public void Initilize(R data) // call first, set up data
    {
        this.data = data;
        isBusy = false;
    }
    public virtual void OnInit() {
        visual.OnInit(data);
        visual.SetCenterPos(Tf.position);
        //for (int i = 0; i < allPros.Count; i++)
        //{
        //    allPros[i].Initilize(this);
        //}
    } // call after Initilize

    public virtual void SetBusy(bool isBusy)
    {
        this.isBusy = isBusy;
    }
}
