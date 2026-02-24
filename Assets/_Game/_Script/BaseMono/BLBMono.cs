using UnityEngine;

public class BLBMono : MonoBehaviour
{
    private Transform tf;
    public Transform Tf
    {
        get
        {
            if(tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
}
