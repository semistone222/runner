using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;


public class ResultInfo{
	public int Stage;
	public int BrozenMoney;
	public int SilverTime;
	public int SilverMoney;
	public int GoldTime;
	public int GoldMoney;
}
public class ResultManager : MonoBehaviour {
	public Text StageText;
	public Text FinishTimetext;
	public Text Goldtext;
	public Sprite Gold;
	public Sprite Silver;


	public GameObject Medal;


	public List<ResultInfo> itemList = new List<ResultInfo>(); 

	// Use this for initialization
	void Start () {
		itemList = Read("ResultData"); 
		StageText.text =  ("Stage"+ SendSence.StageNumber);
		FinishTimetext.text = Timer.timesec;

		if (Timer.CompareTime <= itemList [SendSence.StageNumber - 1].GoldTime) {
			Debug.Log ("Gold");
		
			Medal.GetComponent<Image> ().sprite = Gold;
			Goldtext.text = itemList [SendSence.StageNumber - 1].GoldMoney.ToString();
		} else if ( itemList [SendSence.StageNumber - 1].GoldTime <Timer.CompareTime &&  Timer.CompareTime <= itemList [SendSence.StageNumber - 1].SilverTime) {
			Debug.Log ("Silver");
			Medal.GetComponent<Image> ().sprite = Silver;
			Goldtext.text = itemList [SendSence.StageNumber - 1].SilverMoney.ToString();
		} else if(Timer.CompareTime > itemList [SendSence.StageNumber - 1].SilverTime){
			Debug.Log ("Brozen");
			Goldtext.text = itemList [SendSence.StageNumber - 1].BrozenMoney.ToString();
		}	
	}

	public static List<ResultInfo> Read(string filepath)
	{
		TextAsset textxml = (TextAsset)Resources.Load (filepath, typeof(TextAsset));
		XmlDocument Document = new XmlDocument ();
		Document.LoadXml (textxml.text);

		XmlElement ItemListElement = Document ["Result"];

		List<ResultInfo> ItemList = new List<ResultInfo> ();

		foreach (XmlElement ItemElement in ItemListElement.ChildNodes) {
			ResultInfo Item = new ResultInfo ();
			Item.Stage = System.Convert.ToInt32 (ItemElement.GetAttribute ("Stage"));
			Item.BrozenMoney = System.Convert.ToInt32 (ItemElement.GetAttribute ("BrozenMoney"));
			Item.SilverTime = System.Convert.ToInt32 (ItemElement.GetAttribute ("SilverTime"));
			Item.SilverMoney = System.Convert.ToInt32 (ItemElement.GetAttribute ("SilverMoney"));
			Item.GoldTime = System.Convert.ToInt32 (ItemElement.GetAttribute ("GoldTime"));
			Item.GoldMoney = System.Convert.ToInt32 (ItemElement.GetAttribute ("GoldMoney"));
			ItemList.Add (Item);
		}
		return ItemList;
	}
}