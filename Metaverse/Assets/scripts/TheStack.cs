using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TheStack : MonoBehaviour
{
    private const float boundSize = 3.5f;
    private const float movingBoundsSize = 1f;
    private const float stackMovingSpeed = 5.0f;
    private const float blockMovingSpeed = 3.5f;
    private const float errorMargin = 0.1f;

    public GameObject originBlock = null;

    private Vector3 prevBlockPosition;
    private Vector3 desiredPosition;
    private Vector3 stackBounds = new Vector2(boundSize, boundSize);

    Transform lastBlock = null;
    float blockTrangition = 0f;
    float secondaryPosition = 0f;



    int stackCount = -1;
    public int Score { get { return stackCount; } }
    int comboCount = 0;
    public int Combo { get { return comboCount; } }

    private int maxCombo = 0;
    public int MaxCombo { get => maxCombo; }

    public Color prevColor;
    public Color nextColor;

    private bool isGameOver = true;

    bool isMovingX = true;

    int bestScore = 0;
    public int BestScore { get => bestScore; }
    int bestCombo = 0;
    public int BestCombo { get => bestCombo; }
    private const string BestScoreKey = "BestScore";
    private const string BestComboKey = "BestCombo";
    void Start()
    {
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        bestCombo = PlayerPrefs.GetInt(BestComboKey, 0);

        prevBlockPosition = Vector3.down;
        prevColor = GetRandomColor();
        nextColor = GetRandomColor();
        Spawn_Block();
        Spawn_Block();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isGameOver) return;

            if (PlaceBlock())
            {
                Spawn_Block();
            }
            else
            {
                UpdateScore();
                isGameOver = true;
                GameOverEffect();
                UIManager.Instance.SetScoreUI();
            }


        }
        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, stackMovingSpeed * Time.deltaTime);
    }


    bool Spawn_Block()
    {
        if (lastBlock != null)
            prevBlockPosition = lastBlock.localPosition;

        GameObject newBlock = null;
        Transform newtrans = null;

        newBlock = Instantiate(originBlock);
        ColorChange(newBlock);
        newtrans = newBlock.transform;
        newtrans.parent = this.transform;
        newtrans.localPosition = prevBlockPosition + Vector3.up;
        newtrans.localRotation = Quaternion.identity;
        newtrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        stackCount++;

        desiredPosition = Vector3.down * stackCount;
        blockTrangition = 0f;

        lastBlock = newtrans;

        isMovingX = !isMovingX; //현제 상태를 반대로 바꾼다
        UIManager.Instance.UpdateScore();
        return true;

    }

    Color GetRandomColor() //색상 랜덤 바꾸기
    {
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }
    void ColorChange(GameObject go) //색상 체인지
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10);

        Renderer rn = go.GetComponent<Renderer>();
        rn.material.color = applyColor;
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        if (applyColor.Equals(nextColor) == true)
        {
            prevColor = nextColor;
            nextColor = GetRandomColor(); //질문
        }
    }

    void MoveBlock()
    {
        blockTrangition += Time.deltaTime * blockMovingSpeed; //점차 값이 증가하여 이동 계산

        float movePosition = Mathf.PingPong(blockTrangition, boundSize) - boundSize / 2;
        if (isMovingX)
        {
            lastBlock.localPosition = new Vector3(movePosition * movingBoundsSize, stackCount, secondaryPosition);
        }
        else
        {
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * movingBoundsSize);
        }
    }

    bool PlaceBlock()
    {
        Vector3 lastPosition = lastBlock.transform.localPosition;

        if (isMovingX)
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;
            bool isNegativeNum = (deltaX < 0) ? true : false;

            deltaX = Mathf.Abs(deltaX);
            if (deltaX > errorMargin)
            {
                stackBounds.x -= deltaX;
                if (stackBounds.x <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.x - lastPosition.x) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.x = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaX / 2f;
                CreateRubble(
                    new Vector3(isNegativeNum
                            ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                            : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale
                        , lastPosition.y
                        , lastPosition.z),
                    new Vector3(deltaX, 1, stackBounds.y)
                );

                comboCount = 0;

            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        else
        {
            float deltaZ = prevBlockPosition.z - lastPosition.z;
            bool isNegativeNum = (deltaZ < 0) ? true : false;

            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > errorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.z = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaZ / 2f;
                CreateRubble(
                    new Vector3(
                        lastPosition.x
                        , lastPosition.y
                        , isNegativeNum
                            ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                            : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                    new Vector3(stackBounds.x, 1, deltaZ)
                );

                comboCount = 0;
            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }

        secondaryPosition = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;

        return true;
    }


    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;

        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.localRotation = Quaternion.identity;

        go.AddComponent<Rigidbody>();
        go.name = "Rubble";
    }
    void ComboCheck()
    {
        comboCount++;
        if (comboCount > maxCombo)
        {
            maxCombo = comboCount;
        }
        if (comboCount % 5 == 0)
        {
            stackBounds += new Vector3(0.5f, 0.5f);
            stackBounds.x = (stackBounds.x > boundSize) ? boundSize : stackBounds.x;
            stackBounds.y = (stackBounds.x > boundSize) ? boundSize : stackBounds.x;
        }
    }
    void UpdateScore()
    {
        if (bestScore < stackCount)
        {
            Debug.Log("최고 점수 갱신");
            bestScore = stackCount;
            bestCombo = maxCombo;

            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.SetInt(BestComboKey, bestCombo);
        }


    }
    void GameOverEffect()
    {
        int childCount = this.transform.childCount;

        for (int i = 1; i < 20; i++)
        {
            if (childCount < i)
                break;

            GameObject go =
                this.transform.GetChild(childCount - i).gameObject;

            if (go.name.Equals("Rubble"))
                continue;

            Rigidbody rigid = go.AddComponent<Rigidbody>();

            rigid.AddForce(
                (Vector3.up * Random.Range(0, 10f)
                 + Vector3.right * (Random.Range(0, 10f) - 5f))
                * 100f

            );
           
        }


    }

    public void Restart()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        isGameOver = false;

        lastBlock = null;
        desiredPosition = Vector3.zero;
        stackBounds = new Vector3(boundSize, boundSize);

        stackCount = -1;
        isMovingX = true;
        blockTrangition = 0f;
        secondaryPosition = 0f;

        comboCount = 0;
        maxCombo = 0;

        prevBlockPosition = Vector3.down;

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        Spawn_Block();
        Spawn_Block();
    }
}