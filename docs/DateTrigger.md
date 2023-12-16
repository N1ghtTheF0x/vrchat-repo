# DateTrigger

Animate game objects based on time and/or date locally/globally

## Guide

1. Place an Empty GameObject somewhere
2. Give this GameObject a `DateProvider` Component
    - Change settings if you want
3. Place another Empty GameObject somewhere
4. Give this GameObject a `DateTrigger` Component
    - Assign the `DateProvider` object you created
    - Assign a Unity Animator
    - Assign a Animator Paramater that you use in the Animator
    - You can change the method for checking the range
    - Adjust the date/time settings for your liking (there should be at least one enabled)

## Things to avoid

- a lot of `DateProvider` objects (one is enough actually)
- the animator deactivating any `DateProvider` and `DateTrigger` objects

## What does `Exclusive` and `Inclusive` in `DateTrigger` mean?

`Exclusive` means it will check only inside the range  
`Inclusive` means it will check from the value to the value

### Example

Let's say it's 6:00:00 PM and our Hour Check is from `6` to `13`:

`Exclusive` won't trigger because the `6` is ignored  
`Inclusive` will trigger because the `6` is included

## What's the difference between `System` and `Networking` in `DateProvider`?

`System` will retrieve your time on your computer/headset (local)  
`Networking` will retrieve the time of the VRChat Server your in (global)  

`System` doesn't really make sense for this game, but I added it as an option anyways to avoid potential requests
