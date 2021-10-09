First off, Thank you for trying my tool out. Please contact me on Github or at samuel.r.jacobs23@Gmail.com if you'd like a more indepth discussion.

#Disclaimer
I am in no way affiliated with Microsoft, which originally made magnification API. This program simply piggybacks off of the maginification API.
I am an extremely novice computer scientest (it's really just a hobby for me) so if there are bugs or this doesn't work, please contact me and I'll dig around
There are likely random notes scattered throught the code with profanity. Sorry in advance

WARNING: Using the "RANDOM" button MAY CAUSE YOUR SCREEN TO GO BLACK OR BECOME UNREADABLE. This can be fixed by shuting down your computer or clicking around till you hit the "standard" button

#Using RainbowMagnification API
This tool should be pretty straight forward. Dropdowns can be used from the color select option, random bottom will set a random color, and so on. 
The easiest way to use this tool is to try it out!

#How it Works.
Overview
Magnification API is used on all windows 10 computers. If you type "Magnifier" into the start window, you will see where it is primarily used. RainbowMagnificationAPI piggybacks off this tool.
Magnification API holds tools to invert the color of the screen by using a color matrix. Simply put, the color matrix can go from 1's to -1's, which will invert the screen's colors.
However, these numbers can be alterted to make all sorts of color alterations and modifications. I simply input some standard color matrixes and tell magnification API to apply those color "shifts" to the screen.

Specific
1. Initialize a magnification window using MagInitialize()
2. Define a float array such as the one shown below (TestColor):
TestColor = new float[] {   //Identity needs to be reinstated because identity was getting rewritten during cycling idkwhy
                1.0f,  0.0f,  0.0f,  0.0f,  0.0f ,
                0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
                0.0f,  0.0f,  1.0f, 0.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  1.0f,  0.0f ,
                0.0f,  0.0f,  0.0f,  0.0f,  1.0f};
3. Set the screen to the desired color MagSetFullscreenColorEffect(TestColor)

Those three steps are truly all that is happening. The rest just changing the "TestColor" or inputing random numbers into the array. 

#References
Obviously the OG, microsoft windows docs for magnification API
https://docs.microsoft.com/en-us/windows/win32/api/_magapi/

A great example of magnification API
https://github.com/perevoznyk/karna-magnification

An excellent tool for understanding color matrices and how to alter them
https://zerowidthjoiner.net/colormatrix-viewer