using Assets;
using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour, ISusceptibleToBullets
{
    // if you want animation crate full object no sprite
    public List<Sprite> Peaces;
    public float timerBeforeDestroy = 1f;
    public float radiusOfColider = 1f; 
    public float timerLivePeaces = 10f;
    public float DistantionX = 0.2f;
    public float DistantionY = 1f;
    private float TimerBefore;
    private bool isBurst = false;
    private bool isFirst = true;

    public DamageModel DamageModel { get; set; }

    public void Hit()
    {
        isBurst = true;
    }
    private void Spawn(Sprite sprite)
    {
        var gObject = new GameObject("Peace");

        var random = Random.Range(0, DistantionX);

        var pos = new Vector2(this.transform.position.x + random, (float)(this.transform.position.y - DistantionY));

        gObject.transform.position = pos;

        var renderer = gObject.AddComponent<SpriteRenderer>();

        var circColider = gObject.AddComponent<BoxCollider2D>();

        circColider.size = new Vector2(radiusOfColider, radiusOfColider);

        gObject.AddComponent<Rigidbody2D>();

        var clearer = gObject.AddComponent<ClearerGameObject>();

        clearer.Clear(timerLivePeaces);
        
        renderer.sprite = sprite;


    }
    private void CreatePeaces()
    {
        if (!isBurst) return;

        if (TimerBefore >= timerBeforeDestroy) Destroy(this.gameObject);

        TimerBefore += Time.fixedDeltaTime;

        if (!isFirst) return;
        foreach (var part in Peaces)
        {
            Spawn(part);
        }
        isFirst = false;
    }
 
    void FixedUpdate()
    {
        CreatePeaces();
    }
}
