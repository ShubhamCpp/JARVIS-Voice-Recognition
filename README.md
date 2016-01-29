# JARVIS-Voice-Recognition

## A Voice Recognition and Assistant for Windows

JARVIS is designed as a Windows Form Appilcation. You can give it commands and it will execute those commands for you.
Open the Project file with Microsoft Visual Studio. This is made in VS Pro 2013, however newer VS 2015 and older VS2010,VS2012 should work just fine.

## Working and Development

JARVIS uses the Microsoft System.Speech Library to understand Voice inputs. The System.Speech Library is a part of Microsoft's .NET Framework.

For more information about the Library and the Documentation, visit :- [Microsoft System.Speech Documentation](https://msdn.microsoft.com/en-us/library/gg145021%28v=vs.110%29.aspx)
Also visit :- [Microsoft System.Speech.Synthesis Documentation](https://msdn.microsoft.com/en-in/library/system.speech.synthesis%28v=vs.110%29.aspx)

### Understanding the Code

Visit this link for a proper understanding of the code, the System.Speech Library and the included functions, and how to work with it in C#[Code Project Speen Recognition with C#](http://www.codeproject.com/Articles/483347/Speech-recognition-speech-to-text-text-to-speech-a)

## Reviewing, Adding and Editing Commands

The commands included are given in clist in the C# code. This is the file Form.cs.
To add new commands, add them to the list.

Then in the Switch statement below add the action you want to be implemented when that command is given.

# See it in action

## Some of the Included Commands are :

"hello", "how are you", "what is the time", "open chrome", "thank you", "close", 
"What is the weather", "which languages do you speak?",
"open youtube", "play some music", "open vlc", "pause the music" , "continue playing the music",
"play some coldplay", "switch light on", "switch light off", "shut down the pc", 
"Restart the computer", "Log Off the computer", "Put the computer to sleep", "Scroll Up", "scroll down"

Note :- The commands Scroll Up and Scroll Down are currently not working. I'll update them once I'am done properly implementing them.

Also, the Commands "switch light on", "switch light off" can be implemented by connecting to the Arduino.
This connection is Serial in nature. Specify the COM Port and the baud rate(9600 for Arduino) you wish to communicate with the Arduino at.
This would require you to do some Hardware work and some Arduino Coding.

I'll be updating the Arduino Codes in a while. 

To make the Lights controllable, you need to Interface them through a Relay with the Arduino. 
For a Instructable on controlling lights with the Arduino and connecting a relay check out : [Controlling AC Light with Arduino through Relay Module](http://www.instructables.com/id/Controlling-AC-light-using-Arduino-with-relay-modu/)

Then, it's as simple as sending a HIGH or LOW signal to the Arduino from the Program, which correspondingly switches the Lights On or Off.

I'll post the Arduino Codes soon enough though. Also, I'll make an Instructable demonstrating the same.

Thanks......
If you have any queries, mail me at shubham.chopra2906@gmail.com

Have a Nice Day!!!!!!
