using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceHolder : MonoBehaviour
{

    [SerializeField] List<Slice> liveSlices = new List<Slice>();
    [SerializeField] List<Slice> deadSlices = new List<Slice>();

    int lastDeadSplice;
    int lastLiveSplice;
    
    //REMAKE TO HOLD SPLICE SO WHEN CAN CHECK WHAT KIND OF SPLICE WAS LAST SENT
    public GameObject GetSlice(int lifeStatus)
    {
        
        if(lifeStatus == 0)
        {
            int rnd = UnityEngine.Random.Range(0, deadSlices.Count);
            return deadSlices[rnd].GetSlice();
        }
        else
        {
            int rnd = UnityEngine.Random.Range(0, liveSlices.Count);
            return liveSlices[rnd].GetSlice();
        }
            
    }

}
