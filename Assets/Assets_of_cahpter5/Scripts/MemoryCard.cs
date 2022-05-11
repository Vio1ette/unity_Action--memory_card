using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController controller;

    private int _id;

    public int id  //id��getter����
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
        if (cardBack.activeSelf&&controller.canReveal)  //ֻ�е��ڶ��ſ�Ƭ��û�б�����ʱ������������Ƭ
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);  //thisָ���ǵ�ǰ�ű�����������Ƭʱ��֪ͨ����������
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);  //һ��public������SceneController���Ե������������SetActive���ƿɼ���
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
