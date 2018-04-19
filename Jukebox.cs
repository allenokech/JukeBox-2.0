using AxWMPLib;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace JukeBox
{
    public partial class Jukebox : Form
    {
    public string StrApplicationMediaPath = Directory.GetCurrentDirectory();

        public int Int_NumberofGenre;

        private ListBox[] Media_Library;

        bool IsPlaying = false;
    
    public Jukebox()
        {
            InitializeComponent();
        }
    
    // Show About Dialog //
    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog();
        }
        
     private void hozScroll_Genre_ValueChanged(object sender, EventArgs e)
        {
            txt_Genre_Title.Text = Media_Library[hozScroll_Genre.Value - 1].Items[0].ToString();
            AddToGList(hozScroll_Genre.Value - 1);
        }

        private bool Initailise()
        {
            hozScroll_Genre.Maximum = Int_NumberofGenre;
            hozScroll_Genre.Value = hozScroll_Genre.Minimum;
            AddToGList(hozScroll_Genre.Value - 1);
            return true;
        }
        
        // Checks if the file is possible to play
        private bool PossibleToPlay()
        {
            bool trigger;
            trigger = (lst_Playlist.Items.Count <= 0 ? false : true);
            return trigger;
        }

        // Messages when files fail to load
        private void JukeBox_Shown(object sender, EventArgs e)
        {
            if (!Load_Media_Lists())
            {
                MessageBox.Show("Unable to load the 'Media Content'.");
                InitiateDValue();
            }
            if (!Initailise())
            {
                MessageBox.Show("Unable to display the 'Media Content'.");
                Close();
            }
        }
        
        // Loads music file and plays it
        private void FileLoadAndPlay()
        {
            if ((!PossibleToPlay() ? false : !IsPlaying))
            {
                txt_Playing.Text = lst_Playlist.Items[0].ToString();
                lst_Playlist.Items.Remove(lst_Playlist.Items[0]);
                MediaPlayer.URL = string.Concat(StrApplicationMediaPath, "\\Tracks\\", txt_Playing.Text);
                IsPlaying = true;
                MediaPlayer.Ctlcontrols.play();
            }
        }
        
        // Initializes default values
        private void InitiateDValue()
        {
            Int_NumberofGenre = 1;
            if (Int_NumberofGenre > 1)
            {
                Media_Library = new ListBox[Int_NumberofGenre];
            }
            Media_Library[1] = new ListBox();
            Media_Library[1].Items.Add("Genrel");
        }
        
        // Location from which the files are loaded from
        private bool Load_Media_Lists()
        {
            bool trigger;
            if (!File.Exists(string.Concat(StrApplicationMediaPath, "\\Media\\Media.txt")))
            {
                trigger = false;
            }
            else
            {
                try
                {
                    StreamReader TextFileReader = new StreamReader(string.Concat(StrApplicationMediaPath, "\\Media\\Media.txt"));
                    try
                    {
                        Int_NumberofGenre = Convert.ToInt32(TextFileReader.ReadLine());
                        Media_Library = new ListBox[Int_NumberofGenre];
                        for (int i = 0; i < Int_NumberofGenre; i++)
                        {
                            Media_Library[i] = new ListBox();
                            int num = Convert.ToInt32(TextFileReader.ReadLine());
                            Media_Library[i].Items.Add(TextFileReader.ReadLine());
                            for (int j = 0; j < num; j++)
                            {
                                string str = TextFileReader.ReadLine();
                                Media_Library[i].Items.Add(str);
                            }
                        }
                        TextFileReader.Close();
                    }
                    finally
                    {
                        if (TextFileReader != null)
                        {
                            ((IDisposable)TextFileReader).Dispose();
                        }
                    }
                    trigger = true;
                }
                catch (Exception exception)
                {
                    trigger = false;
                }
            }
            return trigger;
        }

        // When double clicked it will add to playlist
        private void lst_Genre_DoubleClick(object sender)
        {
            if (lst_Genre.Items.Count > -1)
            {
                // If condition check if music is playing, if returms false it loads file and plays music
                lst_Playlist.Items.Add(lst_Genre.Items[lst_Genre.SelectedIndex]);
                if (!IsPlaying)
                {
                    FileLoadAndPlay();
                }
            }
        }
        // Changes state of the media player
        private void myMediaPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent call)
        {
            if (call.newState == 1)
            {
                IsPlaying = false;
                timer1.Enabled = true;
            }
        }

        // Adds to genre list
        private void AddToGList(int DiscGenre)
        {
            lst_Genre.Items.Clear();
            txt_Genre_Title.Text = Media_Library[DiscGenre].Items[0].ToString();
            for (int i = 1; i <= Media_Library[DiscGenre].Items.Count - 1; i++)
            {
                lst_Genre.Items.Add(Media_Library[DiscGenre].Items[i].ToString());
            }
        }
        // Opens Setup form
        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new SetupForm()).ShowDialog();
        }

        // 3 second interval between tracks 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MediaPlayer.playState == WMPPlayState.wmppsPlaying)
            {// Checks status of the media player, if its playing sets bool to 2 else if the media player stops it disables the timer 
                IsPlaying = true;
            }
            else if (MediaPlayer.playState == WMPPlayState.wmppsStopped)
            {
                timer1.Enabled = false;
                FileLoadAndPlay();
            }
        }
    }
}
