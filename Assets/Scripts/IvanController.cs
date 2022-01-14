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
        bool isDuckPressed = Input.GetKeyDown(KeyCode.DownArrow);
        Debug.Log($"State: {state} | isJumpPressed: {isJumpPressed} | isDuckPressed: {isDuckPressed}");

        switch (state)
        {
            case State.RUN:
                if (isJumpPressed)
                {
                    rigidbody2d.velocity = Vector2.up * jumpForce;
                    PlaySound(jumpAudio);
                    state = State.JUMP;
                }
                else if (isDuckPressed)
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
                }
                footstepAudioSource.enabled = false;
                break;
            case State.DOUBLE_JUMP:
                footstepAudioSource.enabled = false;
                break;
            case State.DUCK:
                footstepAudioSource.enabled = true;
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
        Debug.Log("OnCollisionEnter2D");
        switch (state)
        {
            case State.JUMP:
                state = State.RUN;
                break;
            case State.DOUBLE_JUMP:
                state = State.RUN;
                break;
            default: break;
        }
    }
}
