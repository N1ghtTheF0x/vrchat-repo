# MultiAudioTrack

Play layerbased Music/Sound Effects

## What is layerbased

layerbased means that the audio track is split into multiple files. With the ability to `add`/`change to` a layer, you can create dynamic music (one good example is [Buoy Base Galaxy][buoybasegalaxy-gameplay], the music changes after Mario enters the water)

[buoybasegalaxy-gameplay]: https://www.youtube.com/watch?v=6dZ7O-HONZM

## Guide

1. Place an Empty GameObject somewhere
2. Give this GameObject a `MultiAudioTrack` Component
    - add Unity's `AudioSource` to `AudioSources` (you can add as many as you want)
    - set starting layer or autoplay if you want
    - keep in mind that the first `AudioSource` IS the main audio track (and reference)
3. In a UdonSharp script you can push/pop layers:

```c#
using NTF.VRChat;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SomeUdonSharpScript : UdonSharpBehaviour
{
    public MultiAudioTrack track;
    void Start()
    {
        track.PushLayer(); // add the next layer onto the main one
        track.PopLayer(); // removes the last layer from the main one
        track.PushToggle(); // turns off the current layer and turns on the next layer
        track.PushPop(); // turns off the current layer and turns on the previous layer
        track.Reference; // this is the main audio track, changing volume, pitch, etc. will affect the other layers too
    }
}

```

## What's the difference between `PushLayer` and `PushToggle`?

Very simple: `PushLayer` _adds_ the next layer, `PushToggle` _plays_ the next layers and mutes all the others (including the main one!)
