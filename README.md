# UnityTestProject

This project can be tested in the Unity Editor (2019.4.16f1+) after enabling the `KeyboardInputController` on the `Managers` gameObject in the `Playground` scene.

Keyboard controls are:
- W,A,S,D for movement
- LEFT, RIGHT arrow keys for camera rotation

NOTE: Object scaling and rotation in create mode does **NOT** work in the editor.

### Testing on android / iOS:
- App is compatible to landscape and portrait mode
- Camera and character movement can be controled via the virtual joysticks
- Objects can be created in the create mode by tapping on the object in the BuildMenu
- Scale / Rotation can be changed after selecting the respective icons in the floating UI
	- Rotation can be changed by swiping on the screen after the rotate tool has been selected.
	- Scale can be chanegd by 2-finger scale gesture after the scale tool has been selected.
		- **NOTE:** Only uniform scale is supported.

### Modules used:
- Unity StarterAssets pack
- [GraphQL client](https://github.com/gazuntype/graphQL-client-unity)
- [Joystick pack](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631)
- Zenject Dependency Injection Framework
For questions feel free to contact [steegmueller@tuta.io](mailto:steegmueller@tuta.io)

created by Daniel Steegmüller after ~11hours of work
