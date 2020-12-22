<h1> Sample Project </h1>

In this variant, all presenters work independently, same as views do.
Models have a dependency tree and execute with a strong order.

<h2> How to </h2>
Open a scene Sample.scene with Unity 2019.4.15f.<br>
There will be only one MonoBehaviour component CompositionRoot.cs needed for initialization in Unity Runtime.

<h2> Modules </h2>

- Models - Virtualise all the data and its behaviour we want to have as model.
Easy to test. 
Have state and public API as a contract to interact with it.
- Views - Encapsulate Unity layer and work with prefabs. 
Don't depend on any other modules. 
Don't know about each other. 
Can be used without application with temporal code and fake data.
Have state because Unity directs it, but API as passive as possible.

- Presenters - Stateless entity which purpose is to send commands from view to model, and after model update send data change to view back.
Don't know about each other. Can use only public Model and View API.
<h2> Notes </h2>

- I implemented a simple alternative to events: 
<b>IFlag</b> to control the execution flow when it is needed for a subscriber instead of an observable object. 
It also gives consistency of data. 
I don't need to check consistency of data manually ar expect any bugs related to it.
- In CompositionRoot UpdateWatcher.cs creates additional noise.
The reason I add it to every model constructor is just to check that every model received one and only one update every frame. 
- I moved PhysicsScene to another entity because it needs different implementations to work with all cases:
    - Runtime
    - Editor tests
    - Play Tests.
- In ApplicationModel it's best to use State pattern, but for idea sharing reason I keep it in one class.

<h2> Graph or relations between modules </h2>

![image info](./Graphs/AppGraph.png)
