using MyDialogs;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JukeBox
{
    public partial class SetupForm : Form
    {
        private bool RSave = false;

        private ListBox[] Setup_Media_Library;

        private int Int_SetupNumberofGenre;

        private int Int_ShowGenreNumber = 1;

        private string Str_CopyTracksTo = string.Concat(Directory.GetCurrentDirectory(), "\\Tracks\\");

        private string Setup_StrApplicationMediaPath = Directory.GetCurrentDirectory();

        private string Str_CopyTracksFrom = "";

        public SetupForm()
        {
            InitializeComponent();
        }
        
        // Used to import tracks to selected genres//
        private void btn_ImportTracks_Click(object sender, EventArgs e)
        {   // Opens file browser to allow you to select files to input//
            RSave = true;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Limits the user to ability to import certain music file formats//
                foreach (string str in
                    from s in Directory.EnumerateFiles(folderBrowserDialog1.SelectedPath, "*.*", SearchOption.AllDirectories)
                    where (s.EndsWith(".mp3") || s.EndsWith(".wma") || s.EndsWith(".wav") || s.EndsWith(".MP3") || s.EndsWith(".WMA") ? true : s.EndsWith(".WAV"))
                    select s)
                {
                    lst_Imported.Items.Add(str);
                }
                if (lst_Imported.Items.Count <= -1)
                {
                    btn_ImportTracks.Enabled = true;
                }
                else
                {
                    btn_ImportTracks.Enabled = false;
                }

            }  // Clears music files within the imported items listbox//
        private void btn_ClearImportTracks_Click(object sender, EventArgs e)
        {
            lst_Imported.Items.Clear();
            Str_CopyTracksTo = "";
        }
