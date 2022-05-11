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
    private MemoryCard _firstRevealed;   //用来跟踪已经打开的卡片 
    private MemoryCard _secondRevealed;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;


    public bool canReveal
    {
        get { return _secondRevealed == null; } //第二张卡片还没有被翻开
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)  //如果第一张卡片还没有被翻开
        {
            _firstRevealed = card;
        }
        else  //第一张卡片已经被翻开了，但是第二张一定还没有被翻开，否则 CardRevealed 方法不会被成功调用的
        {
            _secondRevealed = card; //跟踪第二张卡片，判断两张卡片是否匹配
            StartCoroutine(CheckMatch());   
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)       //列
        {
            for (int j = 0; j < gridRows; j++)   //行
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
                int index = j * gridCols + i; //按顺序遍历numbers的元素作为下标
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
        SceneManager.LoadScene("Scene");  //加载场景资源
    }

}
