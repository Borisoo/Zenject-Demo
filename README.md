# Asteroids-Game
Asteroid Project made with using Zenject Framwork for Dependency Injection



Unity Version 2020.1.4f1




Controls -

Move up - Press W Move Down - Press S Rotate Right - Press D Rotate Left - Press A

Shoot Bullets - Press Space Bar

White Spheres represents Meteors.

Red Spheres are UFO/ Flying Saucer who can shoot at player


Using a component based architecture to write a decoupled and modular code. Using S.O.L.I.D principles and OOP. 

Using Zenject Dependency Injection Framework to inject dependencies during runtime. 

All the Data is managed with  scriptable objects which are injected during runtime to expose all the parameters like settings for designers.

All the dependencies are injected using interfaces so that they’re no dependency on any concrete classes. To make sure that code is decoupled and modular so that the game remains scalable and will have reusable code base.

Using System.Reflection to filter data for different types of Asteroids like (small/medium/big) and UFO (small UFO / big UFO) which has different attributes.
