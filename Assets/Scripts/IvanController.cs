using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BoxColliderManager))]
public class IvanController : MonoBehaviour {

    public enum State {
        RUN, JUMP, DOUBLE_JUMP, DUCK, DEAD
    };

    public GameController gameController;

    Rigidbody2D rigidbody2d;

    public float jumpForce;
    public float doubleJumpForce;

    BoxCollider2D _collider;
    BoxColliderManager colliderManager;

    public float duckingHeightRatio;
    Vector2 defaultColliderSize;
    Vector2 defaultColliderOffset;
    Vector2 duckingColliderSize;
    Vector2 duckingColliderOffset;

    Animator animator;

    AudioSource[] audioSources;
    AudioSource footstepAudioSource;
    AudioSource oneShotAudioSource;
    public AudioClip jumpAudio;
    public AudioClip footstepAudio;

    static int terrainLayer;
    static int obstacleLayer;

    State _state;
    State state {
        get { return _state; }
        set {
            switch (value) {
                case State.RUN:
                    colliderManager.SetColliderDimensionsTo(_collider, "default");
                    break;
                case State.JUMP:
                    animator.SetTrigger("jump");
                    break;
                case State.DOUBLE_JUMP:
                    animator.SetTrigger("doubleJump");
                    break;
                case State.DUCK:
                    colliderManager.SetColliderDimensionsTo(_collider, "duck");
                    break;
                case State.DEAD:
                    animator.SetTrigger("die");
                    gameController.Die();
                    break;
                default: break;
            }
            _state = value;
        }
    }


    void Awake() {
        terrainLayer = LayerMask.NameToLayer("Terrain");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }


    // Start is called before the first frame update
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        colliderManager = GetComponent<BoxColliderManager>();
        animator = GetComponent<Animator>();

        audioSources = GetComponents<AudioSource>();
        footstepAudioSource = audioSources[0];
        footstepAudioSource.loop = true;
        footstepAudioSource.clip = footstepAudio;
        footstepAudioSource.Play();
        oneShotAudioSource = audioSources[1];
        oneShotAudioSource.loop = false;

        state = State.RUN;
    }

    // Update is called once per frame
    void Update() {
        bool isJumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
        bool isDuckDown = Input.GetKey(KeyCode.DownArrow);

        animator.SetBool("isDuckDown", isDuckDown);

        switch (state) {
            case State.RUN:
                if (isJumpPressed) {
                    rigidbody2d.velocity = Vector2.up * jumpForce;
                    PlaySound(jumpAudio);
                    state = State.JUMP;
                }
                else if (isDuckDown) {
                    state = State.DUCK;
                }
                footstepAudioSource.enabled = true;
                break;
            case State.JUMP:
                if (isJumpPressed) {
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
                if (!isDuckDown) {
                    state = State.RUN;
                }
                break;
            case State.DEAD:
                footstepAudioSource.enabled = false;
                break;
            default: break;
        }
    }

    public void PlaySound(AudioClip clip) {
        oneShotAudioSource.PlayOneShot(clip);
    }

    void OnCollisionEnter2D(Collision2D col) {
        int layer = col.gameObject.layer;
        if (layer == terrainLayer) {
            OnTerrainCollision();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        int layer = col.gameObject.layer;
        if (layer == obstacleLayer) {
            OnObstacleCollision();
        }
    }

    void OnTerrainCollision() {
        switch (state) {
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

    void OnObstacleCollision() {
        state = State.DEAD;
    }
}
