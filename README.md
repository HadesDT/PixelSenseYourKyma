<h1>Pixelsense Your Kyma</h1>

Proof of concept made for KISS 2013 (Kyma International Sound Symposium - http://http://kiss2013.symbolicsound.com ).

<h2>What are the principles behind the project?</h2>
There are three main thing:<br />
First is of course the kyma patch.<br />
After that we have the OSC message which will be used to control the kyma via ethernet connection.<br />
Finally we have the Microsoft Pixelsense table wich will generate the OSC message and send them to the kyma.<br />

The goal is to control kyma with something intuitive and fun. Of course the application is really limited, but we count on your imagination to put this concept on another level.

<h2>How does it work?</h2>
After making the patch and getting the OSC parameter to control the kyma we can build the application. I use an OSC C# class made by Mingming Zhang to send OSC message.
The Pixelsense SDK is really easy to use and you can get some great result in short time.

<h3>First, the design part</h3>
The design part is describe by SurfaceWindow1.xaml file.
The thing to have here is a general canvas where we put some event action when the table get a "touch down", "touch up" and a "touch move"
Once you have that, you can imagine whatever you want to have something functionnal and beautiful. For this concept, I decided to stay really sober.

<h3>The logic part</h3>
The logic will be find in the touchdown, touchmove and touchup functions.

<h4>touchdown</h4>
On this function, first we check if the touch down is made by a tag. If it is the case, we will simply send an OSC message. This OSC message will contain the parameter to move (depend of the tag value) and the value of this paramter (depend of the orientation of the tag).

<h4>touchmove</h4>
This function will do the same thing as touchdown. The goal here is to change parameter when we turn the object, just like a potentiometer.

<h4>touchup</h4>
Here we just put the parameter to a default value.