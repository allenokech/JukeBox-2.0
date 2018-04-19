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
