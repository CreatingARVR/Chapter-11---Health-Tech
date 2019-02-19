using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class RawTrialDataSaver : MonoBehaviour
{
    private string status = "dataUnsaved";

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
    private SmoothedHandScript s;
    private string filepath;
    public GameObject rightHandObject;
    private string dataFilePath = @"C:\Users\bcohn\Desktop\Insight_Patient_Data\patientData.csv";
    // Use this for initialization
    void Start () {
        s = rightHandObject.GetComponent<SmoothedHandScript>();
	}
    private bool PDFIsAvailable() {return(File.Exists(@"C:\Users\bcohn\Desktop\Insight_Patient_Data\report.pdf"));}
    private void Update()
    {
        if (status=="dataUnsaved" && Input.GetKeyDown(KeyCode.Space))
        {
            try
            {
                SavePositionsAndRotationsToDiskAndAnalyze();
                status = "waitingOnPDF";
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
        else if (status=="waitingOnPDF" && PDFIsAvailable() == false)
        {
            Debug.Log("STILL WAITING ON PDF");
        } else if (status=="waitingOnPDF" && PDFIsAvailable())
        {
            Debug.Log("<TODO SERHAN LOAD THE PDF AND SHOW IT>");
            status = "pdfReadyAndLoaded";
        }
    }

    void SavePositionsAndRotationsToDiskAndAnalyze()
    {
        using (FileStream fs = File.Create(dataFilePath))
        {
            AddText(fs, "x,y,z\n");
            foreach (Vector3 position in s.positionList)
            {
                var observationString = position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString();
                AddText(fs, observationString);
                AddText(fs, "\n");
            }
        }
        Debug.Log("Saved Data To Disk Successfully. Now Waiting on PDF");
    }
}
