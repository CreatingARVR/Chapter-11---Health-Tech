using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothedHandScript : MonoBehaviour
{
    [SerializeField]
    private GameObject RightController;
    public int filterSampleSize = 70;
    [System.NonSerialized]
    public List<Vector3> positionList = new List<Vector3>();
    [System.NonSerialized]
    public List<Quaternion> rotationList = new List<Quaternion>();
    public Vector3 filteredPosition;
    private Quaternion meanQuaternion;
    private Transform currentTransform;
    private int lowerBound;
    public bool filterOn = false;
    // hand model: https://poly.google.com/view/btWmPNVSKUc
    // balloon: https://poly.google.com/view/d1gDDhM7pTf
    // used https://github.com/matzman666/OpenVR-AdvancedSettings/releases
    // used https://poly.google.com/view/0mIKrWNcEyd for pencil 

    //derived from https://forum.unity.com/threads/average-quaternions.86898/
    // assuming qArray.Length > 1
    // @param qList a list of quaternion objects, each a sample from the last N frames.
    // @return qAvg the average expected output of the rotation, in quaternion format.
    Quaternion AverageQuaternion(List<Quaternion> qList)
    {
        Quaternion qAvg = qList[0];
        float weight;
        for (int i = 1; i < qList.Count; i++)
        {
            weight = 1.0f / (float)(i + 1);
            qAvg = Quaternion.Slerp(qAvg, qList[i], weight);
        }
        return qAvg;
    }
    //@param tList list of position vectors in 3 dimensions, representing positions over time. Each element is a sample from one of N frames.
    //@return filteredPosition a single Vector3 where each dimension is averaged on the window of provided samples.
    Vector3 AveragePosition(List<Vector3> tList)
    {
        Vector3 positionSum = new Vector3(0, 0, 0);
        int n = tList.Count;
        for (int i = 0; i < n; i++)
        {
            positionSum += tList[i];
        }
        Vector3 filteredPosition = positionSum / n;
        return(filteredPosition);
    }
    void LateUpdate()
    {

       //On each evaluation, save the position and orientation of the files to a variable
       currentTransform = RightController.transform;
       positionList.Add(currentTransform.position);
       rotationList.Add(currentTransform.rotation);
       
       // State machine for tremor filtering

       //Case where filter is disengaged
       if (filterOn==false)
        {
            transform.SetPositionAndRotation(currentTransform.position, currentTransform.rotation);
        }
       // Case where filter is engaged, but insufficient data precludes filtering.
       else if (positionList.Count < filterSampleSize)
        {
            transform.SetPositionAndRotation(currentTransform.position, currentTransform.rotation);
            Debug.Log("Filling sample set " + Time.frameCount.ToString() + "/" + filterSampleSize.ToString());
        }
       // Case where there is sufficient data to filter and filtering is engaged
       else if(positionList.Count >= filterSampleSize)
        {
            lowerBound = (positionList.Count - filterSampleSize);

            List<Vector3> subsetOfPositions = positionList.GetRange(lowerBound, filterSampleSize);
            filteredPosition = AveragePosition(subsetOfPositions);
            meanQuaternion = AverageQuaternion(rotationList.GetRange(lowerBound, filterSampleSize));
            transform.SetPositionAndRotation(filteredPosition, meanQuaternion);
        }
       // No unidentified exceptions have been found as of 11:15pm, Mar 13.
        else{Debug.Log("Unidentified Error Re:Time indiex.");}} 
}