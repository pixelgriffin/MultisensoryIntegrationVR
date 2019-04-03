using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour {

    public bool readOnAwake = true;
    public string fname = "Tests.csv";

    public GameObject rotationRig;

    private int testRepeats = 0;

	void Awake () {
        if(readOnAwake)
        {
            ReadCSV(Application.dataPath + "\\" + fname);
        }
	}

    public int GetDesiredTestRepeats()
    {
        return testRepeats;
    }

    public void ReadCSV(string fname)
    {
        string data = File.ReadAllText(fname);
        string[] lines = data.Split('\n');

        string[] repeatLine = lines[0].Trim().Split(',');
        int repeatCount = 0;
        int.TryParse(repeatLine[1], out repeatCount);

        string[] lineData = lines[1].Trim().Split(',');
        int testCount = 0;
        int.TryParse(lineData[1], out testCount);

        if(testCount > 0 && repeatCount > 0)
        {
            testRepeats = repeatCount;

            for(int i = 0; i < testCount; i++)
            {
                string[] xRotLine = lines[4 + (i * 9)].Trim().Split(',');
                string[] yRotLine = lines[6 + (i * 9)].Trim().Split(',');
                string[] zRotLine = lines[8 + (i * 9)].Trim().Split(',');
                string[] distLine = lines[10 + (i * 9)].Trim().Split(',');

                int xmin, xmax, ymin, ymax, zmin, zmax, dmin, dmax;

                int.TryParse(xRotLine[0], out xmin);
                int.TryParse(xRotLine[1], out xmax);

                int.TryParse(yRotLine[0], out ymin);
                int.TryParse(yRotLine[1], out ymax);

                int.TryParse(zRotLine[0], out zmin);
                int.TryParse(zRotLine[1], out zmax);

                int.TryParse(distLine[0], out dmin);
                int.TryParse(distLine[1], out dmax);

                rotationRig.transform.rotation = Quaternion.Euler(Random.Range(xmin, xmax), Random.Range(ymin, ymax), Random.Range(zmin, zmax));
                rotationRig.transform.GetChild(0).position = new Vector3(0f, 0f, -Random.Range(dmin, dmax));
            }
        }
    }

    public void WriteCSV(string fname)
    {
        StreamWriter w = new StreamWriter(fname, true);

        w.WriteLine("data,1");

        w.Close();
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
