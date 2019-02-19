using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

//Loads texture from image file on disk
public class GetImage : MonoBehaviour
{
    //public string filePath = "/ Users / serhan / Desktop / Insight_Patient_Data";
    string filePath = @"C:\Users\bcohn\Desktop\Insight_Patient_Data";

    void Update()
	{
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Refreshed framed plot");
            Texture2D tex = LoadPNG(filePath);

            Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            Image img = this.GetComponent<Image>();
            img.sprite = sp;
        }
       
	}

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath+ @"\plotT.png"))
        {
            fileData = File.ReadAllBytes(filePath + @"\plotT.png");
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}