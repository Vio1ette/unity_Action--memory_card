using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController controller;

    private int _id;

    public int id  //id的getter函数
    {
        get { return _id;}
    }

    public void SetCard(int id,Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf&&controller.canReveal)  //只有当第二张卡片还没有被翻开时，才允许翻开卡片
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);  //this指的是当前脚本，当翻开卡片时，通知场景控制器
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);  //一个public方法，SceneController可以调用这个方法，SetActive控制可见性
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
