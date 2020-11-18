--- Notes and points ---
- App is fully driven by code and tests.
- Unity encapsulated into thin view layer leafs.
- Execution is not influenced by random unity update and callbacks order.
- No any static classes at all. All static legacy or unity API should be encapsulated inside models or views for tests.
- Client modules interaction has no events or direct subscriptions. So it's possible to interrupt sequences or reorder calls.
- Good testability of every layer. Unit tests available for presenters and models, test interactions with views and automated tests too.
- Every view has responsibilities only about himself. Every view is autonomous. 
- If you check usings, you may see:
	* no usings of Root anywhere;
	* all views don't know about presenters or models.
	* models don't know about presenters and views.
	* presenters know about models and views but only describes interaction between them. presenters stateless as possible.
- Injection of every class goes always through constructors.
- Without direct subscriptions every model data change is consistent. All values are changed before presenters react to it. (using EventDelay.cs)
- Most of bugs is easier to track, because logging data and states only inside one update gives a deterministic and full picture of calls. Easy to reproduce.
- In HoV potentially possible to switch old state machine states to new states one by one.
- Possible to create views without any dependencies or additional supporting code just to test it in unity with fake data. Also important for testing edge cases without waiting configurations on backend.
- All executions goes in predictable order and always without skipping any frames:
	Every frame:
	* Input and Unity callbacks check
	* Model processions
	* Visualization of model changes

- All views and models has singleton lifestyle. Inner presenter state recreates every time state change. Encapsulated Unity part can be reinstanced or recreated inside views if needed for memory management.
Less code in presenters, some of them now duplicate models APIs without profit.

- There only few types of classes in architecture:
	* Views
	* MonoBehaviours without logic just to gather links from prefabs. Postfix *PrefabLinks in examples
	* Presenter and it's states
	* Sub-presenters (to reuse code inside presenter states if different states operate same complex views)
	* Models
	* Composition Root (class tree creation and factories then needed somewhere)
	
