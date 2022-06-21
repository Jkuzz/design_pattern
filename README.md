# Example Design Pattern Implementation

This an implementation of design patterns in a larger project. 
The project is a procedural terrain generator written in Unity. The shown code handles how water is added to the scene. 

A prototype water asset is instantiated in the Unity editor and passed to the manager script. 
([Asset that is used](https://assetstore.unity.com/packages/vfx/shaders/stylized-water-for-urp-162025))
The editor-configured water chunk is treated as a Prototype. This reduces complexity, because the water asset has a [large amount of
parameters](https://alexander-ameye.gitbook.io/stylized-water/features/shader-properties) that must be configured.
When new chunks are requested, the prototype is cloned using the 
Unity builtin [Object.Instantiate](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html).

Another pattern in use is the Pooling pattern. As chunks are unloaded in their Update methods, to avoid re-instantiating new water objects for the scene,
water chunks are instead deactivated and stored in a Pool, from which they can be re-enabled when new water is requested.
This saves performance at a slight RAM cost.
