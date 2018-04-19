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
