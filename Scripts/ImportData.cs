/*
   Jet Ppropulsion Laboratory
   Virtual Reality for Mars Rovers | Summer 2016
   Davy Ragland | dragland@stanford.edu
   Oleg Pariser | Oleg.Pariser@jpl.nasa.gov
*/

/*********************************************************************
SETUP
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
/*********************************************************************
                           CLASS
*********************************************************************/
public class ImportData : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public GameObject prefab;
	public TextAsset xmlFile;
	/* ****************  GLOBAL OBJECTS  ****************  */
	bool visable = false;
	XmlDocument xmlDoc;
	public Vector3[] dataPoints;
	GameObject[] pins;
	/*********************************************************************
	                             BOOT
	*********************************************************************/
	void Start () {
		loadData ();
		dataPointsInit ();
	}

	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update () {
		resetCheck ();
		if (Input.GetButtonDown ("Y_Button")) visable = !visable;
		displayPoints(visable);
	}

	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: loadData
	---------------------------------
	This function loads the data from an external file.
	*/
	void loadData(){
		xmlDoc  = new XmlDocument();
		xmlDoc.LoadXml (xmlFile.text);
		XmlNodeList vertex = xmlDoc.GetElementsByTagName("position");
		int dataPointIndex = 0;
		foreach( XmlNode node in vertex){
			XmlNodeList chilren = node.ChildNodes;
			float x = 0, y = 0, z = 0;
			foreach (XmlNode child in chilren){
				if(child.Name == "x") x = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "y") y = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "z") z  = XmlConvert.ToSingle(child.InnerText);
			}
			dataPoints[dataPointIndex] = new Vector3(x, y, z);
			dataPointIndex++;
		}
	}
	/**********************************************************************
	                           HELPERS 
	*********************************************************************/
	/*
	function: resetCheck
	---------------------------------
	This function returns the user to the start position.
	*/
	void resetCheck(){
		if (Input.GetButtonDown ("Start_Button")) {
			visable = false;
		}
	}

	/*
	function: dataPointsInit
	---------------------------------
	This function initializes the dataPoints from the file.
	*/
	void dataPointsInit(){
		pins = new GameObject[dataPoints.Length];
		for (int i = 0; i < dataPoints.Length; i++) {
			GameObject Pin = Instantiate (prefab);
			Pin.transform.position = dataPoints [i];
			pins [i] = Pin;
		}
	}

	/*
	function: displayPoints
	---------------------------------
	This function displays or removes the loaded datapoints.
	*/
	void displayPoints(bool display){
		for (int i = 0; i < pins.Length; i++) {
			pins [i].SetActive(display);
		}
	}
}