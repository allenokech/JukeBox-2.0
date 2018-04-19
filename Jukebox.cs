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
