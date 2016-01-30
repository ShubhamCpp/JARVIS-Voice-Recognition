using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.Threading;
using System.IO;
using System.IO.Ports;


namespace WindowsVoiceApp1
{
    class KeyHandle
    {
        private static Int32 WM_KEYDOWN = 0x100;
        private static Int32 WM_KEYUP = 0x101;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(int Msg, System.Windows.Forms.Keys wParam, int lParam);

        public static void SendKeyUp(System.Windows.Forms.Keys key)
        {
            PostMessage(WM_KEYUP, key, 0);
        }

        public static void SendKeyDown(System.Windows.Forms.Keys key)
        {
            PostMessage(WM_KEYDOWN, key, 0);
        }
    }
    public partial class Form1 : Form
    {
        // Form Declarations
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();
        SerialPort ardo = new SerialPort();
        WMPLib.WindowsMediaPlayer mplayer = new WMPLib.WindowsMediaPlayer();
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Start Button Click

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            clist.Add(new string[] { "hello", "how are you", "what is the time", "open chrome", "thank you", "close", "taste my stick", "What is the weather", "which languages do you speak?", "open youtube", "play some music", "open vlc", "pause the music" , "continue playing the music", "play some coldplay", "switch light on", "switch light off", "shut down the pc", "Restart the computer", "Log Off the computer", "Put the computer to sleep", "Scroll Up", "scroll down" });
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            // ardo = new SerialPort();
            ardo.PortName = "COM6";
            ardo.BaudRate = 9600;

            // Configure the audio input.
            sre.SetInputToDefaultAudioDevice();
            // Configure the audio output. 
            ss.SetOutputToDefaultAudioDevice();

            //See the Voices Installed
            // seeInstalledVoices(ss);

            // Set a Voice
            //ss.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            ss.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);

            try
            {
                // Speak a string.
                ss.Speak("Hello Sir, I'm JARVIS you're Smart Assistant.");
                // ss.Speak("Hope you are doing fine today, How may I assist you?");
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }



        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //If Selected Functions Keywords were Recognized
            switch(e.Result.Text.ToString())
            {
                case "hello":
                    {
                        ss.SpeakAsync("hello Sir");
                        break;
                    }
                case "how are you":
                    {
                        ss.SpeakAsync("I'm doing Great");
                        break;
                    }
                case "what is the time":
                    {
                        ss.SpeakAsync("Current Time is" + DateTime.Now.ToLongTimeString());
                        break;
                    }
                case "open chrome":
                    {
                        ss.SpeakAsync("Yes rightaway Sir");
                        Process.Start("chrome.exe","http:\\www.google.com");
                        break;
                    }
                case "Scroll Up":
                    {
                        ss.SpeakAsync("Yes Sir");
                        KeyHandle.SendKeyUp(Keys.PageUp);
                        break;
                    }
                case "scroll down":
                    {
                        ss.SpeakAsync("Yes Sir");
                        KeyHandle.SendKeyDown(Keys.PageDown);
                        break;
                    }
                case "open youtube":
                    {
                        ss.SpeakAsync("Yes rightaway Sir");
                        Process.Start("chrome.exe", "http:\\www.youtube.com");
                        break;
                    }
                case "switch light on":
                    {
                        string status = "1";
                        ss.SpeakAsync("Light is on");
                        ardo.Open();
                        ardo.Write(status);
                        ardo.Close();
                        break;
                    }
                case "switch light off":
                    {
                        ss.SpeakAsync("Light is off");
                        ardo.Open();
                        ardo.Write("0");
                        ardo.Close();
                        break;
                    }
                case "play some music":
                    {
                        /* ss.SpeakAsync("Yes I have just the right thing for you");
                        Thread.Sleep(2000);
                        Process.Start(@"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe", @"D:\Songs\Apologize.mp3");
                        break; */
                        // music = new Microsoft.DirectX.AudioVideoPlayback.Audio(D:\Songs\Apologize.mp3);
                        // music.Play();
                        // WMPLib.WindowsMediaPlayer mplayer = new WMPLib.WindowsMediaPlayer();
                        mplayer.URL = @"D:\Songs\Apologize.mp3";
                        mplayer.controls.play();
                        break;
                    }
                case "play some coldplay":
                    {
                        ss.SpeakAsync("Yes Sir");
                        Process.Start(@"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe", @"D:\Songs\Coldplay-Fix_You.mp3");
                        break;
                    }
                case "pause the music":
                    {
                        // WMPLib.WindowsMediaPlayer mplayer = new WMPLib.WindowsMediaPlayer();
                        // mplayer.URL = @"D:\Songs\Apologize.mp3";
                        mplayer.controls.pause();
                        break;
                    }
                case "continue playing the music":
                    {
                        // WMPLib.WindowsMediaPlayer mplayer = new WMPLib.WindowsMediaPlayer();
                        // mplayer.URL = @"D:\Songs\Apologize.mp3";
                        mplayer.controls.play();
                        break;
                    }
                case "open vlc":
                    {
                        ss.SpeakAsync("Yes rightaway Sir");
                        Process.Start(@"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe");
                        break;
                    }
                case "thank you":
                    {
                        ss.SpeakAsync("No problem");
                        break;
                    }
                case "close":
                    {
                        ss.SpeakAsync("Okay Sir");
                        Application.Exit();
                        break;
                    }
                case "taste my stick":
                    {
                        ss.SpeakAsync("I would glady Sir,     but unfortunately I don't have a mouth");
                        break;
                    }
                case "What is the weather":
                    {
                        ss.SpeakAsync("It's pretty hot outside.");
                        break;
                    }
                case "which languages do you speak?":
                    {
                        ss.SpeakAsync("Presently I only understand and speak English.");
                        break;
                    }
                case "Log Off the computer":
                    {
                        ss.SpeakAsync("Logging off the System");
                        ExitWindowsEx(0, 0);
                        break;
                    }
                case "Put the computer to sleep":
                    {
                        ss.SpeakAsync("Putting the computer to sleep");
                        SetSuspendState(false, true, true);
                        break;
                    }
                case "shut down the pc":
                    {
                        ss.SpeakAsync("Doing a System Shut Down");
                        Application.Exit();
                        Process.Start("shutdown", "/s /t 0");
                        break;
                    }
                case "Restart the computer":
                    {
                        ss.SpeakAsync("Doing a System Restart");
                        Application.Exit();
                        Process.Start("shutdown", "/r /t 0");
                        break;
                    }
            }
            textBox1.Text = e.Result.Text.ToString() + Environment.NewLine;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // Button Stop Functions
            sre.RecognizeAsyncStop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        public void seeInstalledVoices(SpeechSynthesizer synth)
        {
            // Output information about all of the installed voices. 
            textBox1.Text = "Installed voices -";
            foreach (InstalledVoice voice in synth.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;
                string AudioFormats = "";
                foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
                {
                    AudioFormats += String.Format("{0}\n",
                    fmt.EncodingFormat.ToString());
                }

                textBox1.AppendText(" Name:          " + info.Name);
                textBox1.AppendText(" Culture:       " + info.Culture);
                textBox1.AppendText(" Age:           " + info.Age);
                textBox1.AppendText(" Gender:        " + info.Gender);
                textBox1.AppendText(" Description:   " + info.Description);
                textBox1.AppendText(" ID:            " + info.Id);
                textBox1.AppendText(" Enabled:       " + voice.Enabled);
                if (info.SupportedAudioFormats.Count != 0)
                {
                    textBox1.AppendText(" Audio formats: " + AudioFormats);
                }
                else
                {
                    textBox1.AppendText(" No supported audio formats found");
                }

                string AdditionalInfo = "";
                foreach (string key in info.AdditionalInfo.Keys)
                {
                    AdditionalInfo += String.Format("  {0}: {1}\n", key, info.AdditionalInfo[key]);
                }

                textBox1.AppendText(" Additional Info - " + AdditionalInfo);
                textBox1.AppendText("/n");
            }
        }
    }
}
