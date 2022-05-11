using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2;
    public const float offsetY = 2.5f;

    private int _score = 0;


    [SerializeField] private MemoryCard originalCard;
    private MemoryCard _firstRevealed;   //���������Ѿ��򿪵Ŀ�Ƭ 
    private MemoryCard _secondRevealed;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;


    public bool canReveal
    {
        get { return _secondRevealed == null; } //�ڶ��ſ�Ƭ��û�б�����
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)  //�����һ�ſ�Ƭ��û�б�����
        {
            _firstRevealed = card;
        }
        else  //��һ�ſ�Ƭ�Ѿ��������ˣ����ǵڶ���һ����û�б����������� CardRevealed �������ᱻ�ɹ����õ�
        {
            _secondRevealed = card; //���ٵڶ��ſ�Ƭ���ж����ſ�Ƭ�Ƿ�ƥ��
            StartCoroutine(CheckMatch());   
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)       //��
        {
            for (int j = 0; j < gridRows; j++)   //��
            {
                MemoryCard card;
                if (i == 0 & j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int index = j * gridCols + i; //��˳�����numbers��Ԫ����Ϊ�±�
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);

            }
        }
        
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;

    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene");  //���س�����Դ
    }

}
