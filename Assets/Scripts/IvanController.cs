using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvanController : MonoBehaviour
{
    public enum State
    {
        RUN, JUMP, DOUBLE_JUMP, DUCK, DEAD
    };

    State state;

    Rigidbody2D rigidbody2d;

    public float jumpForce;
    public float doubleJumpForce;

    Animator animator;

    AudioSource[] audioSources;
    AudioSource footstepAudioSource;
    AudioSource oneShotAudioSource;
    public AudioClip jumpAudio;
    public AudioClip footstepAudio;

    static int terrainLayer;

    void Awake()
    {
        terrainLayer = LayerMask.NameToLayer("Terrain");
    }


    // Start is called before the first frame update
    void Start()
    {
        state = State.RUN;

        rigidbody2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        audioSources = GetComponents<AudioSource>();
        footstepAudioSource = audioSources[0];
        footstepAudioSource.loop = true;
        footstepAudioSource.clip = footstepAudio;
        footstepAudioSource.Play();
        oneShotAudioSource = audioSources[1];
        oneShotAudioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
        bool isDuckDown = Input.GetKey(KeyCode.DownArrow);
        Debug.Log($"State: {state} | isJumpPressed: {isJumpPressed} | isDuckPressed: {isDuckDown}");

        animator.SetBool("isDuckDown", isDuckDown);

        switch (state)
        {
            case State.RUN:
                if (isJumpPressed)
                {
                    rigidbody2d.velocity = Vector2.up * jumpForce;
                    PlaySound(jumpAudio);
                    state = State.JUMP;
                    animator.SetTrigger("jump");
                }
                else if (isDuckDown)
                {
                    state = State.DUCK;
                }
                footstepAudioSource.enabled = true;
                break;
            case State.JUMP:
                if (isJumpPressed)
                {
                    rigidbody2d.velocity = Vector2.up * doubleJumpForce;
                    PlaySound(jumpAudio);
                    state = State.DOUBLE_JUMP;
                    animator.SetTrigger("doubleJump");
                }
                footstepAudioSource.enabled = false;
                break;
            case State.DOUBLE_JUMP:
                footstepAudioSource.enabled = false;
                break;
            case State.DUCK:
                footstepAudioSource.enabled = true;
                if (!isDuckDown)
                {
                    state = State.RUN;
                }
                break;
            case State.DEAD:
                footstepAudioSource.enabled = false;
                break;
            default: break;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        oneShotAudioSource.PlayOneShot(clip);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        int layer = col.gameObject.layer;
        if (layer == terrainLayer)
        {
            OnTerrainCollision();
        }
    }

    void OnTerrainCollision()
    {
        switch (state)
        {
            case State.JUMP:
                state = State.RUN;
                animator.SetTrigger("land");
                break;
            case State.DOUBLE_JUMP:
                state = State.RUN;
                animator.SetTrigger("land");
                break;
            default: break;
        }
    }
}
