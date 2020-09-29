// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/MainIA.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Game
{
    public class @MainIA : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MainIA()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainIA"",
    ""maps"": [
        {
            ""name"": ""MapControl"",
            ""id"": ""afe49863-f4ab-47bf-b531-9a6fa5621f09"",
            ""actions"": [
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""6ac442bf-dfed-47ee-a022-34c43e975d76"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4e022bcc-1dc2-4e9b-83d7-f0814ee71e48"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaceBlock"",
                    ""type"": ""PassThrough"",
                    ""id"": ""18304ff9-a955-4218-b93b-1c857df1eaab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""Value"",
                    ""id"": ""fd13ed68-6711-4b5c-8e65-ec4e4b16d4fd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""d562c860-538c-44d9-910f-ae221916de57"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1479fcc0-e0f5-4f42-933a-49866bdcc354"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""52d08fea-5c64-4066-becb-386b6e42dbaf"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""626731d6-137b-49dd-ba86-ce036e4f441a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5b141963-b33e-4760-84d6-03d2e42fcee6"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e5ede2f4-374e-41be-81d4-b38a651658cf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""da32b3b5-e659-4860-bcdf-3419cbdfcfb3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a44924c4-e0b9-48d6-b7a3-6c255276696b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""71f88ab9-218b-4772-9384-df41be356e16"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ea6c9d86-88a3-4540-8834-1462b5b21a27"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ZoomCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d837c6c-0f55-4829-a129-832feb882f6f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""PlaceBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9c26e66-ee3e-4e05-ab40-19dbf40bfb71"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""PlaceBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efb4b838-cad5-4fbe-a9e7-2100a78a4fed"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""PlaceBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71c71b93-052a-4abe-bbf4-501aee7f6f0d"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""1a019e19-5f02-4b39-845f-c1601bec1afb"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9cc12809-8859-4dd9-aa2f-847f9bf8c434"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""03fd7ade-f3e7-4cac-9ed8-d5a43ad7fec4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ExitGame"",
                    ""type"": ""Button"",
                    ""id"": ""ee479dfe-9498-4f9e-a4f0-3754f5d6671d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""af8551ad-4560-4e0a-ab59-041f46ee862f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1e5722a4-0ae7-4a84-9734-27d65fcf9f6b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8c82a8e2-6f5f-49ac-8969-ce1b073851bf"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b0b80bab-32f6-4cc9-9f99-1f0d09c096cb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""944b0486-540f-4f5f-9efe-893398de264f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cedb8c43-8193-410e-a12e-ede289098094"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""308c675d-de12-4024-9a85-d76f3b56558c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ExitGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""7a691268-984d-4358-a204-12b99664625e"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // MapControl
            m_MapControl = asset.FindActionMap("MapControl", throwIfNotFound: true);
            m_MapControl_MoveCamera = m_MapControl.FindAction("MoveCamera", throwIfNotFound: true);
            m_MapControl_ZoomCamera = m_MapControl.FindAction("ZoomCamera", throwIfNotFound: true);
            m_MapControl_PlaceBlock = m_MapControl.FindAction("PlaceBlock", throwIfNotFound: true);
            m_MapControl_MouseMove = m_MapControl.FindAction("MouseMove", throwIfNotFound: true);
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
            m_Player_ExitGame = m_Player.FindAction("ExitGame", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // MapControl
        private readonly InputActionMap m_MapControl;
        private IMapControlActions m_MapControlActionsCallbackInterface;
        private readonly InputAction m_MapControl_MoveCamera;
        private readonly InputAction m_MapControl_ZoomCamera;
        private readonly InputAction m_MapControl_PlaceBlock;
        private readonly InputAction m_MapControl_MouseMove;
        public struct MapControlActions
        {
            private @MainIA m_Wrapper;
            public MapControlActions(@MainIA wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveCamera => m_Wrapper.m_MapControl_MoveCamera;
            public InputAction @ZoomCamera => m_Wrapper.m_MapControl_ZoomCamera;
            public InputAction @PlaceBlock => m_Wrapper.m_MapControl_PlaceBlock;
            public InputAction @MouseMove => m_Wrapper.m_MapControl_MouseMove;
            public InputActionMap Get() { return m_Wrapper.m_MapControl; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MapControlActions set) { return set.Get(); }
            public void SetCallbacks(IMapControlActions instance)
            {
                if (m_Wrapper.m_MapControlActionsCallbackInterface != null)
                {
                    @MoveCamera.started -= m_Wrapper.m_MapControlActionsCallbackInterface.OnMoveCamera;
                    @MoveCamera.performed -= m_Wrapper.m_MapControlActionsCallbackInterface.OnMoveCamera;
                    @MoveCamera.canceled -= m_Wrapper.m_MapControlActionsCallbackInterface.OnMoveCamera;
                    @ZoomCamera.started -= m_Wrapper.m_MapControlActionsCallbackInterface.OnZoomCamera;
                    @ZoomCamera.performed -= m_Wrapper.m_MapControlActionsCallbackInterface.OnZoomCamera;
                    @ZoomCamera.canceled -= m_Wrapper.m_MapControlActionsCallbackInterface.OnZoomCamera;
                    @PlaceBlock.started -= m_Wrapper.m_MapControlActionsCallbackInterface.OnPlaceBlock;
                    @PlaceBlock.performed -= m_Wrapper.m_MapControlActionsCallbackInterface.OnPlaceBlock;
                    @PlaceBlock.canceled -= m_Wrapper.m_MapControlActionsCallbackInterface.OnPlaceBlock;
                    @MouseMove.started -= m_Wrapper.m_MapControlActionsCallbackInterface.OnMouseMove;
                    @MouseMove.performed -= m_Wrapper.m_MapControlActionsCallbackInterface.OnMouseMove;
                    @MouseMove.canceled -= m_Wrapper.m_MapControlActionsCallbackInterface.OnMouseMove;
                }
                m_Wrapper.m_MapControlActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MoveCamera.started += instance.OnMoveCamera;
                    @MoveCamera.performed += instance.OnMoveCamera;
                    @MoveCamera.canceled += instance.OnMoveCamera;
                    @ZoomCamera.started += instance.OnZoomCamera;
                    @ZoomCamera.performed += instance.OnZoomCamera;
                    @ZoomCamera.canceled += instance.OnZoomCamera;
                    @PlaceBlock.started += instance.OnPlaceBlock;
                    @PlaceBlock.performed += instance.OnPlaceBlock;
                    @PlaceBlock.canceled += instance.OnPlaceBlock;
                    @MouseMove.started += instance.OnMouseMove;
                    @MouseMove.performed += instance.OnMouseMove;
                    @MouseMove.canceled += instance.OnMouseMove;
                }
            }
        }
        public MapControlActions @MapControl => new MapControlActions(this);

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Move;
        private readonly InputAction m_Player_Interact;
        private readonly InputAction m_Player_ExitGame;
        public struct PlayerActions
        {
            private @MainIA m_Wrapper;
            public PlayerActions(@MainIA wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @Interact => m_Wrapper.m_Player_Interact;
            public InputAction @ExitGame => m_Wrapper.m_Player_ExitGame;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @ExitGame.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitGame;
                    @ExitGame.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitGame;
                    @ExitGame.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitGame;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                    @ExitGame.started += instance.OnExitGame;
                    @ExitGame.performed += instance.OnExitGame;
                    @ExitGame.canceled += instance.OnExitGame;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        public struct UIActions
        {
            private @MainIA m_Wrapper;
            public UIActions(@MainIA wrapper) { m_Wrapper = wrapper; }
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface IMapControlActions
        {
            void OnMoveCamera(InputAction.CallbackContext context);
            void OnZoomCamera(InputAction.CallbackContext context);
            void OnPlaceBlock(InputAction.CallbackContext context);
            void OnMouseMove(InputAction.CallbackContext context);
        }
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnExitGame(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
        }
    }
}
