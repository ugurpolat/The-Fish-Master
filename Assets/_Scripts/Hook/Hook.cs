﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    #region
    public Transform hookedTransform;
    
    private Camera mainCamera;
    private Collider2D coll;
    
    private int length;
    private int strength;
    private int fishCount;

    private bool canMove = false;

    private List<Fish> hookedFishes;

    private Tweener cameraTween;
    #endregion

    void Awake()
    {
        mainCamera = Camera.main;
        coll = GetComponent<Collider2D>();
        //List<fish>
        hookedFishes = new List<Fish>();
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        if (canMove || Input.GetMouseButton(0))
        {
            // function maybe?

            Vector3 vector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = vector.x;

            transform.position = position;
        }
    }

    public void StartFishing()
    {
        length = IdleManager.instance.length - 20;
        strength = IdleManager.instance.strength;
        fishCount = 0;

        float time = (-length) * 0.1f;

        cameraTween = mainCamera.transform.DOMoveY(length, 1 + time * 0.25f, false).OnUpdate(delegate
        {
             if (mainCamera.transform.position.y <= -11)
             {
                 transform.SetParent(mainCamera.transform);
             }


        }).OnComplete(delegate
        {
             coll.enabled = true;
             cameraTween = mainCamera.transform.DOMoveY(0, time * 5, false).OnUpdate(delegate
             {
                 if (mainCamera.transform.position.y >= -25f)
                     StopFishing();

                 
                
             });
        });


        
        ScreensManager.instance.ChangeScreen(Screens.GAME);
        coll.enabled = false;
        canMove = true;
        hookedFishes.Clear();


    }

    void StopFishing()
    {
        canMove = false;
        cameraTween.Kill(false);
        cameraTween = mainCamera.transform.DOMoveY(0, 2, false).OnUpdate(delegate 
        {
            if (mainCamera.transform.position.y >= -11)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -6);
            }
        
        }).OnComplete(delegate 
        {
            if (true)
            {
                transform.position = Vector2.down * 6;
                coll.enabled = true;
                int num = 0;
                for (int i = 0; i < hookedFishes.Count; i++)
                {
                    hookedFishes[i].transform.SetParent(null);
                    hookedFishes[i].ResetFish();
                    num += hookedFishes[i].Type.price;
                }

                IdleManager.instance.totalGain = num;
                ScreensManager.instance.ChangeScreen(Screens.END);
                //SceenManager End Screen

            }
        });

    }

    private void OnTriggerEnter2D(Collider2D target)
    {

        if (target.CompareTag("Fish") && fishCount != strength)
        {
            fishCount++;
            Fish component = target.GetComponent<Fish>();
            component.Hooked();
            hookedFishes.Add(component);
            target.transform.SetParent(transform);
            target.transform.position = hookedTransform.position;
            target.transform.rotation = hookedTransform.rotation;
            target.transform.localScale = Vector3.one;
            target.transform.DOShakeRotation(5, Vector3.forward * 45, 10, 90).SetLoops(1, LoopType.Yoyo).OnComplete(delegate
            {
                target.transform.rotation = Quaternion.identity;
            });
            if (fishCount == strength)
            {
                StopFishing();
            }
        }
        
    }
}
