using UnityEngine;
using System.Collections;

using UnityEngine.Audio;
using UnityEngine.UI;

public class GiveItem : MonoBehaviour
{
    
    public Item itemInfo;
    public TextBalloonManager tbMng;

    int itemCode;
    int value;
    Color colorInfo;
    Vector3 origin;
    Vector3 screenPoint;
    Vector3 offset;

    void Awake()
    {
        itemCode = itemInfo.itemCode;
        colorInfo = GetComponent<Image>().color; //R,G,B는 유지가능

        value = itemInfo.itemValue;
    }

    void Update()
    {
        string currentItemString = "Item" + itemCode.ToString();
        if (PlayerPrefs.GetInt(currentItemString) == 0)
        {
            itemInfo.isBought = false;
        }
        else
        {
            itemInfo.isBought = true;
        }

        if (itemInfo.isBought)
        {
            interactChange(1f, true);
        }
        else
        {
            interactChange(0.2f,false);
        }
    }

    void interactChange(float amount, bool status)
    {
        GetComponent<Image>().color = 
            new Color(colorInfo.r, colorInfo.g, colorInfo.b, amount);
        GetComponent<CircleCollider2D>().enabled = status;
    }


    // OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider
    public void OnMouseDown()
    {
        origin = GetComponent<Transform>().position;
        offset = GetComponent<Transform>().position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    // OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse
    public void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        GetComponent<Transform>().position = curPosition;
    }

    // OnMouseUp is called when the user has released the mouse button
    public void OnMouseUp()
    {
        GetComponent<Transform>().position = origin;
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Cat")
        {
            collision.GetComponent<AudioSource>().Play();

            Transform gauge;
            gauge = collision.transform.FindChild("UI_Gauge");
            switch ((int)itemCode / 3)
            {
                case 0:
                    {
                        gauge = gauge.transform.FindChild("GaugeHunger");
                        break;
                    }
                case 1:
                    {
                        gauge = gauge.transform.FindChild("GaugeAffine");
                        break;
                    }
                case 2:
                    {
                        gauge = gauge.transform.FindChild("GaugeHealth");
                        break;
                    }
                default:
                    {
                        gauge = gauge.transform.FindChild("GaugeHealth");
                        break;
                    }
            }

            if(GaugeValueChange(gauge, value))
            {

                //먹일수없다!();
                if (gauge.name == "GaugeHunger")
                {
                    if (itemCode%3 == 0)
                    {
                        tbMng.Play("비리지만 맛있군");
                    }
                    else if(itemCode%3 == 1)
                    {
                        tbMng.Play("냠냠 치킨이당");
                    }
                    else if (itemCode % 3 == 2)
                    {
                        tbMng.Play("이 맛은.. 신세계다!");
                    }
                    else
                    {
                        tbMng.Play("잘 먹었어!");
                    }

                    PlayerPrefs.SetInt("Hunger", PlayerPrefs.GetInt("Hunger")+value);

                }
                else if (gauge.name == "GaugeAffine")
                {
                    if (itemCode % 3 == 0)
                    {
                        tbMng.Play("쥐돌이 잡을거야 얍얍");
                    }
                    else if (itemCode % 3 == 1)
                    {
                        tbMng.Play("폭신폭신하고 좋군");
                    }
                    else if (itemCode % 3 == 2)
                    {
                        tbMng.Play("멋있긴 한데 너무 빨라 ㅜㅜ");
                    }
                    else
                    {
                        tbMng.Play("재밌게 놀았어!");
                    }

                    PlayerPrefs.SetInt("Affine", PlayerPrefs.GetInt("Affine") + value);
                }
                else if (gauge.name == "GaugeHealth")
                {
                    if (itemCode % 3 == 0)
                    {
                        tbMng.Play("몸이 좀 좋아지는 느낌이야");
                    }
                    else if (itemCode % 3 == 1)
                    {
                        tbMng.Play("캬 역시 피로할 땐 냥카스지");
                    }
                    else if (itemCode % 3 == 2)
                    {
                        tbMng.Play("내 안의 숨어있던 힘이 깨어나는것같군!");
                    }
                    else
                    {
                        tbMng.Play("최고야!");
                    }

                    PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + value);
                }
                else
                {
                    tbMng.Play("고마워!");
                }
            }
            else
            {
                //먹일수없다!();
                if(gauge.name == "GaugeHunger")
                {
                    tbMng.Play("배부르다 한동안 소화시킬거얌");
                }
                else if (gauge.name == "GaugeAffine")
                {
                    tbMng.Play("다 놀았당 이제 쉬어도 될것같아요");
                }
                else if (gauge.name == "GaugeHealth")
                {
                    tbMng.Play("난 건강해! 약이 필요없지");
                }
                else
                {
                    tbMng.Play("안 줘도 괜찮아");
                }

            }
        }

        itemInfo.isBought = false;

        string currentItemString = "Item" + itemCode.ToString();
        PlayerPrefs.SetInt(currentItemString, 0);
        PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + 10);
    }

    bool GaugeValueChange(Transform gauge, int amount)
    {
        Slider slider = gauge.GetComponent<Slider>();

        if(slider.value == 100) //이미 100일때 돌려보냄
        {
            return false;
        }

        if (slider.value + amount > 100) // 100 넘는 양수일때
        {
            gauge.GetComponent<Slider>().value = 100;
        }
        else if (slider.value + amount < 0) //음수일때
        {
            gauge.GetComponent<Slider>().value = 0;
        }
        else //그 외의 경우
        {
            gauge.GetComponent<Slider>().value += amount;
        }
        return true;
    }
}
