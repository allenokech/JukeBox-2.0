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
        
        // Allows user to copy tracks to specific genres//
        private void btn_CopyToGenre_Click(object sender, EventArgs e)
        {
            RSave = true;
            if (lst_Imported.SelectedIndex == -1)
            {
                MessageBox.Show("You must Select an item to Copy.");
            }
            else
            {      
                lst_PresentGenreTracks.Items.Add(lst_Imported.Items[lst_Imported.SelectedIndex]);
                Setup_Media_Library[Int_ShowGenreNumber].Items.Add(lst_Imported.Items[lst_Imported.SelectedIndex]);
                if (!File.Exists(string.Concat(Str_CopyTracksTo, lst_Imported.Items[lst_Imported.SelectedIndex])))

                {
                    //   File.Copy(string.Concat(Str_CopyTracksFrom, lst_Imported.Items[lst_Imported.SelectedIndex]), string.Concat(Str_CopyTracksTo, lst_Imported.Items[lst_Imported.SelectedIndex]));
                    File.Copy(string.Concat(Str_CopyTracksFrom, lst_Imported.Items[lst_Imported.SelectedIndex]), string.Concat(Str_CopyTracksTo, lst_Imported.Items[lst_Imported.SelectedIndex]));
                }
            }
        }
        // Moves selected tracks to specific genres// 
        private void btn_Move_To_Genre_Click(object sender, EventArgs e)
        {// Prompts you to select a track before moving forward// 
            RSave = true;
            if (lst_Imported.SelectedIndex == -1)
            {
                MessageBox.Show("You must Select an item to Move.");
            }
            else
            { 
                lst_PresentGenreTracks.Items.Add(lst_Imported.Items[lst_Imported.SelectedIndex]);
                Setup_Media_Library[Int_ShowGenreNumber].Items.Add(lst_Imported.Items[lst_Imported.SelectedIndex]);
                if (!File.Exists(string.Concat(Str_CopyTracksTo, lst_Imported.Items[lst_Imported.SelectedIndex])))
                {
                    //  File.Copy(string.Concat(Str_CopyTracksFrom, lst_Imported.Items[lst_Imported.SelectedIndex]), string.Concat(Str_CopyTracksTo, lst_Imported.Items[lst_Imported.SelectedIndex]));
                    File.Copy(string.Concat(Str_CopyTracksFrom, lst_Imported.Items[lst_Imported.SelectedIndex]), string.Concat(Str_CopyTracksTo, lst_Imported.Items[lst_Imported.SelectedIndex]));
                }
                lst_Imported.Items.RemoveAt(lst_Imported.SelectedIndex);
            }
        }
        // This saves the selection// 
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (RSave)
            {
                if (!Save_Media_List())
                {
                    MessageBox.Show("Could not Save the new configuration!");
                }
            }
            Close();
        }
        
        // Enables the user to add a genre//
        private void btn_AddGenre_Click(object sender, EventArgs e)
        {
            string i;
            RSave = true;
            for (i = My_Dialogs.InputBox("Please enter the 'Genre Title'."); i == ""; i = My_Dialogs.InputBox("Please enter the 'Genre Title'."))
            {
                MessageBox.Show("You must give the 'Genre' a title!");
            }
            if (i != "")
            {
                Int_SetupNumberofGenre++;
                Int_ShowGenreNumber = Int_SetupNumberofGenre;
                if (Setup_Media_Library != null)
                {
                    Array.Resize<ListBox>(ref Setup_Media_Library, Int_SetupNumberofGenre + 1);
                }
                Setup_Media_Library[Int_SetupNumberofGenre - 1] = new ListBox();
                Setup_Media_Library[Int_SetupNumberofGenre - 1].Items.Add(i);
                AddToGList(Int_ShowGenreNumber);
                btn_NextGenre.Enabled = false;
                btn_PreviousGenre.Enabled = true;
                btn_DeleteGenre.Enabled = true;
            }
        }
