using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour {

    private Animator animator
    {
        get
        {
            return GetComponent<Animator>();
        }
    }
    private UISprite sprite
    {
        get
        {
            return transform.GetChild(0).GetComponent<UISprite>();
        }
    }
    public void PlayAnimationByName(string spriteName)
    {
        this.gameObject.SetActive(true);
        sprite.spriteName = spriteName;
        this.animator.Play(0);
        StartCoroutine(WaiteTime());
    }
    private IEnumerator WaiteTime()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }


}
