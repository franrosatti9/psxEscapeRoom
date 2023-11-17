using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] float focusTime = 1f;
    public GameState gameState;
    private Player _player;
    private IFocusable currentFocusedObject;
    private IFocusable currentSubFocusedObject;
    private Camera _mainCam;
    private Vector3 playerLastCameraPos;
    private Quaternion playerLastCameraRot;

    private bool onTransition;

    public event Action<int> OnPlayerInteracted;
    
    public Player GetPlayer => _player;

    public static GameManager instance;
    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
        
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Cursor.visible = false;
    }

    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            if (gameState == GameState.Moving)
            {
                // Open menu
            }
            
            else if (gameState == GameState.ObjectFocused)
            {
                if(onTransition) return;
                
                // If already focusing inside a focusable object, set SubFocus to false.
                // If not subfocusing, go back to player
                if (currentSubFocusedObject != null)
                {
                    StartCoroutine(FocusCamera(currentFocusedObject.FocusTransform));
                    currentSubFocusedObject.Unfocus();
                    currentSubFocusedObject = null;
                }
                else
                {
                    StartCoroutine(CameraBackToPlayer());
                    
                }
            }
        }
    }

    public void ChangeState(GameState state)
    {
        gameState = state;
        switch (state)
        {
            case GameState.Moving:
                _player.EnableInput(true);
                _player._controller.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case GameState.ObjectFocused:
                _player.EnableInput(false);
                _player._controller.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Inspecting:
                _player.EnableInput(false);
                _player._controller.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case GameState.InTransition:
                _player.EnableInput(false);
                _player._controller.enabled = false;
                _player.EnableInteraction(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void PlayerInteracted(int id, IInteractable interactedWith)
    {
        OnPlayerInteracted?.Invoke(id);
    }

    public void FocusObject(IFocusable target)
    {
        if (currentSubFocusedObject != null) return;
        
        _mainCam.transform.parent = null;

        // If not focusing inside focused object, save player cam pos
        if (currentFocusedObject == null)
        {
            currentFocusedObject = target;
            
            playerLastCameraPos = _mainCam.transform.position;
            playerLastCameraRot = _mainCam.transform.rotation;
        }
        else currentSubFocusedObject = target;
        
        
        
        ChangeState(GameState.ObjectFocused);
        StartCoroutine(FocusCamera(target.FocusTransform));
    }
    IEnumerator FocusCamera(Transform target)
    {
        onTransition = true;
        _player.EnableInteraction(false); // Disable Interaction during transition
        Vector3 initialCameraPos = _mainCam.transform.position;
        Quaternion initialCameraRot = _mainCam.transform.rotation;
        
        Vector3 focusPosition = target.position;
        Quaternion focusRotation = target.rotation;
        
        float elapsedTime = 0f;
        
        while (elapsedTime < focusTime)
        {
            _mainCam.transform.position = Vector3.Lerp(initialCameraPos, focusPosition, elapsedTime / focusTime);
            _mainCam.transform.rotation = Quaternion.Lerp(initialCameraRot, focusRotation, elapsedTime / focusTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Finish Lerping
        _mainCam.transform.position = Vector3.Lerp(initialCameraPos, focusPosition, 1f);
        _mainCam.transform.rotation = Quaternion.Lerp(initialCameraRot, focusRotation, 1f);
        
        yield return null;
        onTransition = false;
        _player.EnableInteraction(true);
    }

    // HACER AMBAS CORRUTINAS EN UNA MAS GENERICA
    
    IEnumerator CameraBackToPlayer()
    {
        currentFocusedObject.Unfocus();
        onTransition = true; 
        _player.EnableInteraction(false); // Disable Interaction during transition
        
        Vector3 initialCameraPos = _mainCam.transform.position;
        Quaternion initialCameraRot = _mainCam.transform.rotation;
        
        
        float elapsedTime = 0f;
        
        while (elapsedTime < focusTime)
        {
            _mainCam.transform.position = Vector3.Lerp(initialCameraPos, playerLastCameraPos, elapsedTime / focusTime);
            _mainCam.transform.rotation = Quaternion.Lerp(initialCameraRot, playerLastCameraRot, elapsedTime / focusTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _mainCam.transform.position = Vector3.Lerp(initialCameraPos, playerLastCameraPos, 1f);
        _mainCam.transform.rotation = Quaternion.Lerp(initialCameraRot, playerLastCameraRot, 1f);
        yield return null;
        _mainCam.transform.parent = _player.gameObject.transform;
        
        ChangeState(GameState.Moving);
        //currentPuzzle.Unfocus();
        
        currentFocusedObject = null;
        onTransition = false;
        _player.EnableInteraction(true);
    }
}

public enum GameState
{
    Moving,
    ObjectFocused,
    Inspecting,
    InTransition
}
