<h1 align="center">✨ Observer Pattern ✨</h1>

<h3>⭐ Instruction </h3>
<ul>
  <li> <img align="right" width="40%" src="https://github.com/Yunasawa/YNL-Utilities/assets/113672166/dee35538-5bc3-4b51-8321-384843821714"> Firstly, you have to create a struct to be the event data, it is used to injects into registered objects. The structs should have a static property used to assign values into events. The structure is as you can see the example in this image. </li>
  <br></br>
  <br></br>
  <br></br>
  <br></br>
  <li> <img align="right" width="40%" src="https://github.com/Yunasawa/YNL-Utilities/assets/113672166/470d0aee-fcdb-4ae2-a60a-575915d6fb81"> After you created the struct, inside of the objects you want to register, you must implement them using an interface <img height="15px" src="https://github.com/Yunasawa/YNL-Utilities/assets/113672166/be59bbe0-0443-4987-89e5-fb131d8c2ee3">. Then you have to implement a method <img height="15px" src="https://github.com/Yunasawa/YNL-Utilities/assets/113672166/c73efaa9-a32a-4dbc-aa2a-2114d1e300aa">, this will contain your logic to handle the data. If the objects can receive more than one event, just implement the suitable interfaces.</li>
  <br></br>
  <li> <img align="right" width="27%" src="https://github.com/Yunasawa/YNL-Utilities/assets/113672166/e5548ff6-4200-470a-85e1-a297961e2853">Now you can invoke the event using this line of code <img height="20px" src="https://github.com/Yunasawa/YNL-Utilities/assets/113672166/c5357499-3736-480a-84b8-aa89c26c6f80"></li>
  <br></br>
  <br></br>
</ul>
<h3>⭐ Notice </h3>
<ul>
  <li> If each event (struct) just trigger only one listener, you can use <b><i>this.RegisterSingle()</i></b> and <b><i>this.UnregisterSingle()</i></b> to register and unregister the listener, this can enhance performance a little bit.
  <li>For <b>Monobehaviour</b> objects, you can register and unregister inside <b><i>Awake()<i></b> and <b><i>OnDestroy()</i></b> or inside <b><i>OnEnable()</i></b> and <b><i>OnDisable()</i></b></li>
</ul>
