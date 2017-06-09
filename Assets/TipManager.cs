using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class TipInfo{
	public int id;
	public string Tip;
}
	
public class TipManager : MonoBehaviour {
	private int RandomNumber;
	public Text TipText;
	public List<TipInfo> itemList = new List<TipInfo>(); 
	// Use this for initialization
	void Start () {
		itemList = Read("TipData"); 

		RandomNumber= Random.Range(0, itemList.Count);
		Debug.Log ("RandomNumber"  + RandomNumber);
		Debug.Log ("Tip"  + itemList[0].id);
		Debug.Log ("Tip"  + itemList.Count);
		TipText.text = itemList [RandomNumber].Tip;
	}
	
	public static List<TipInfo> Read(string filepath)
	{
		TextAsset textxml = (TextAsset)Resources.Load (filepath, typeof(TextAsset));
		XmlDocument Document = new XmlDocument ();
		Document.LoadXml (textxml.text);

		XmlElement ItemListElement = Document ["Loading"];

		List<TipInfo> ItemList = new List<TipInfo> ();

		foreach (XmlElement ItemElement in ItemListElement.ChildNodes) {
			TipInfo Item = new TipInfo ();
			Item.id = System.Convert.ToInt32 (ItemElement.GetAttribute ("id"));
			Item.Tip =ItemElement.GetAttribute ("Tip");
			ItemList.Add (Item);
		}
		return ItemList;
	}
}
