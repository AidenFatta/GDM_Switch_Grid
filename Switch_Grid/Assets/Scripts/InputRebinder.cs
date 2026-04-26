using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class InputRebinder : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public InputAction shoot;
    public Button shootBindButton;
    public TextMeshProUGUI shootBindLabel;

    public InputAction action;
    public Button actionBindButton;
    public TextMeshProUGUI actionBindLabel;

    public InputAction zoom;
    public Button zoomBindButton;
    public TextMeshProUGUI zoomBindLabel;

    public InputAction pause;
    public Button pauseBindButton;
    public TextMeshProUGUI pauseBindLabel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var playerActionMap = inputActions.FindActionMap("Player");

        BindButton(shootBindButton, playerActionMap.FindAction("Attack"), shootBindLabel);
        BindButton(actionBindButton, playerActionMap.FindAction("Interact"), actionBindLabel);
        BindButton(zoomBindButton, playerActionMap.FindAction("Camera Toggle"), zoomBindLabel);
        BindButton(pauseBindButton, playerActionMap.FindAction("Pause"), pauseBindLabel);

        //ResetAllBindings(); 

        LoadAllRebinds();
    }

    //For Debuging or removing errors in map in critical situations, not intended for player use currently. Can be added to a settings menu if desired to restore default controls.
    public void ResetAllBindings()
    {
        foreach (var map in inputActions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                action.RemoveAllBindingOverrides();
                PlayerPrefs.DeleteKey(action.name + "_rebinds");
            }
        }

        // Refresh UI labels
        shootBindLabel.text = shoot.GetBindingDisplayString();
        actionBindLabel.text = action.GetBindingDisplayString();
        zoomBindLabel.text = zoom.GetBindingDisplayString();
        pauseBindLabel.text = pause.GetBindingDisplayString();
    }

    private void BindButton(Button button, InputAction action, TextMeshProUGUI label)
    {
        button.onClick.AddListener(() => StartRebind(action, label));
        label.text = action.GetBindingDisplayString();
    }

    private void StartRebind(InputAction action, TMP_Text label)
    {
        label.text = "Press a key...";

        action.Disable();

        rebindingOperation = action.PerformInteractiveRebinding()
            .WithCancelingThrough("<Keyboard>/escape")
            .OnPotentialMatch(operation =>
            {
                var control = operation.selectedControl;

                // Special rule: Pause cannot use left click to avoid conflicts with the clicking button
                if (action.name == "Pause" && InputControlPath.Matches("<Mouse>/leftButton", control))
                {
                    label.text = "Error";
                    operation.Cancel();
                    action.Enable();
                    return;
                }

                // Duplicate binding check
                if (IsBindingAlreadyUsed(action, control))
                {
                    label.text = "Key in use";
                    operation.Cancel();
                    action.Enable();
                    return;
                }
            })
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                if (operation.canceled)
                {
                    operation.Dispose();
                    return;
                }

                action.Enable();
                operation.Dispose();

                label.text = action.GetBindingDisplayString();
                SaveRebind(action);
            })
            .Start();
    }

    private bool IsBindingAlreadyUsed(InputAction currentAction, InputControl newControl)
    {
        foreach (var map in inputActions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                if (action == currentAction)
                    continue;

                foreach (var binding in action.bindings)
                {
                    if (string.IsNullOrEmpty(binding.effectivePath))
                        continue;

                    if (InputControlPath.Matches(binding.effectivePath, newControl))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void SaveRebind(InputAction action)
    {
        string json = action.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(action.name + "_rebinds", json);
    }
    private void LoadAllRebinds()
    {
        foreach (var map in inputActions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                string key = action.name + "_rebinds";
                if (PlayerPrefs.HasKey(key))
                {
                    action.LoadBindingOverridesFromJson(PlayerPrefs.GetString(key));
                }
            }
        }
    }
}
