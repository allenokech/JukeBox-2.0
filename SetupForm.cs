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
