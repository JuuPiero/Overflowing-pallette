using System.Collections;
using UnityEngine;

public class ScreenBase : MonoBehaviour, INavigationScreen
{

    [Header("Transition")]
    [SerializeField] protected bool _useTransition = false;
    [SerializeField] protected float _transitionDuration = 0.3f;

    protected virtual void Awake()
    {

    }

    public virtual void OnEnter(object param = null)
    {
        gameObject.SetActive(true);
        if (_useTransition)
            TransactionIn();

    }
    public virtual void OnExit()
    {
        if (_useTransition)
            TransactionOut();
        else
            gameObject.SetActive(false);
    }
    protected virtual void TransactionIn()
    {

    }
    protected virtual void TransactionOut()
    {
        
    }
}