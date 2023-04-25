using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrehub;
    public GameObject Box;

    int[,] map;
    GameObject[,] field;//�Q�[���Ǘ��p�̔z�� 
    // Start is called before the first frame update
    void Start()
    {
        //GameObject instance = Instantiate(playerPrehub, new Vector3(0, 0, 0), Quaternion.identity);


        map = new int[,] { 
            { 0, 0, 0, 0, 0 },
            { 0, 0, 1, 2, 0 },
            { 0, 0, 0, 0, 0 },
        };
        field = new GameObject
            [
            map.GetLength(0),
            map.GetLength(1)
            ];
        string debugText = "";
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    //GameObject instance = Instantiate(
                    field[y, x] = Instantiate(
                        playerPrehub,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
                if (map[y, x] == 2)
                {
                    //GameObject instance = Instantiate(
                    field[y, x] = Instantiate(
                        Box,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
                //Debug.Log(map[i] + ",");
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";//�J��
        }

        Debug.Log(debugText);
    }

  //  Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex(); 

            MoveNummber("Player", playerIndex, playerIndex +new Vector2Int(1,0) );
          
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex(); 

            MoveNummber("Player", playerIndex, playerIndex+new Vector2Int(-1,0));    

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNummber("Player", playerIndex, playerIndex + new Vector2Int(0, -1));

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNummber("Player", playerIndex, playerIndex + new Vector2Int(0, 1));

        }
    }

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y,x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new(-1,-1);
    }
        bool MoveNummber(string tag,Vector2Int moveFrom, Vector2Int moveTo)
        {
            if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
            {
              return false;
            }
             if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
             {
                 return false;
             }
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            //�v���C���[�̈ړ��悩�炳��ɐ�ւQ���ړ�������B
            bool success = MoveNummber(tag, moveTo, moveTo + velocity);
            //���������Փ˂�����
            if (!success)
            {
                return false;
            }
        }

        field[moveFrom.y,moveFrom.x].transform.position =
            new Vector3(moveTo.x,field.GetLength(0)-moveTo.y,0);
            field[moveTo.y,moveTo.x] =field[moveFrom.y,moveFrom.x];
            field[moveFrom.y,moveFrom.x] = null;
            return true;
        }

    }
