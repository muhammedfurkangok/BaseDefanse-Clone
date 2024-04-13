using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TextAnimationController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private GameObject _textAnimation;

    #endregion

    #endregion

    private void Start()
    {
        _textAnimation.transform.DOLocalMove(new Vector3( 0f,5f,0f),0.5f).SetLoops(-1,LoopType.Yoyo); 
    }
}
