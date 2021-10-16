using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.2f;
    [SerializeField] private float alphaSet = 0.8f;
    [SerializeField] private float alphaMutiplier = 0.85f;

    private float timeActivated;
    private float alpha;
    private Transform playerTransform;
    private SpriteRenderer SR;
    private SpriteRenderer playerSR;
    private Player player;

    private Color color;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = playerTransform.GetComponent<SpriteRenderer>();
        player = playerTransform.GetComponent<Player>();
    }

    private void OnEnable()
    {
        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        if (player.FacingDirection == 1)
            SR.flipX = false;
        else
            SR.flipX = true;

        transform.position = playerTransform.position;
        transform.rotation = playerTransform.rotation;
        timeActivated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMutiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;

        if (Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
