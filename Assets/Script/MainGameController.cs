using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;
public class MainGameController : MonoBehaviour
{

    public List<Transform> ListMap = new List<Transform>();
    public CharacterController ModelController;
    Vector3 EndVectorPosition;
    float endposition;
    float speed = 500f;
    void Start()
    {
        endposition = ListMap[1].GetComponent<RectTransform>().rect.width;
        EndVectorPosition = new Vector3(-endposition, ListMap[1].localPosition.y, 0);
    }
    void Update()
    {
        if (ModelController.grounded && Input.GetMouseButtonDown(0))
        {
            JumbModel();
        }
        Moveobject();
        if (ModelController.transform.localPosition.y - (ListMap[0].localPosition.y + ListMap[0].GetComponent<RectTransform>().rect.height) < 1)
        {

            ModelController.grounded = true;
        }
        else
        {
            ModelController.grounded = false;
        }
    }
    public void JumbModel()
    {
        Debug.Log((ListMap[0].localPosition.y + ListMap[0].GetComponent<RectTransform>().rect.height));
        Debug.Log("jumb");
        //ModelController.jumb();
        ModelController.transform.GetComponent<Rigidbody2D>().gravityScale = 50;
        ModelController.transform.GetComponent<Rigidbody2D>().isKinematic = false;
        ModelController.transform.DOJump(new Vector3(ModelController.transform.position.x,ModelController.transform.position.y,ModelController.transform.position.z),100f,1,2).OnComplete(()=> {
            ModelController.GetComponent<Rigidbody2D>().gravityScale = 0;
            ModelController.GetComponent<Rigidbody2D>().isKinematic = true;
        });
    }
    void Moveobject()
    {
        for (int i = 0; i < ListMap.Count; i++)
        {
            ListMap[i].localPosition = Vector3.MoveTowards(ListMap[i].localPosition, EndVectorPosition, Time.fixedDeltaTime * speed);
            if (ListMap[i].localPosition == EndVectorPosition)
            {
                if (i + ListMap.Count - 1 < ListMap.Count)
                {
                    ListMap[i].localPosition = new Vector3(ListMap[i + 2].localPosition.x + ListMap[i].GetComponent<RectTransform>().rect.width - Time.fixedDeltaTime * speed, ListMap[i].localPosition.y, 0);
                    Debug.Log(ListMap[i].localPosition.x + " ListMap[0].localPosition.x " + ListMap[i].localPosition.x);
                    // UnityEditor.EditorApplication.isPaused = true;
                }
                else
                    ListMap[i].localPosition = new Vector3(ListMap[i - 1].localPosition.x + ListMap[i].GetComponent<RectTransform>().rect.width, ListMap[i].localPosition.y, 0);
            }
        }
    }
}
