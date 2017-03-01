# Multiplatform Backlight Changer
A simple application that works on both Windows and Linux to change the system backlight level.

Once built (open in Visual Studio or MonoDevelop and hit Build) you can run it by:

(Linux)
`sudo mono IBC.exe 10` (Sets backlight to 10% of max)

(Windows)
 `IBC.exe 10` (Same effect, but Windows doesn't require root.)


I shouldn't have to mention this, but I will anyway: This obviously requires you to have a screen whos brightness can be controlled by software - IE most Laptops, and almost never Desktops.
