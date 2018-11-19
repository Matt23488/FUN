using FUN.Config;
using FUN.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FUN
{
    class Controller
    {
        private static Controller _instance;
        public static void Start(IMainView mainView)
        {
            if (_instance == null) _instance = new Controller(mainView);
        }

        private const string FILE0 = "file0";
        private const string UNDERTALE_INI = "undertale.ini";

        private IMainView _mainView;

        private Configuration _config;
        private Dictionary<string, FileInfo> _undertaleConfigFiles;

        public Controller(IMainView mainView)
        {
            _mainView = mainView;

            Initialization();
        }

        private void Initialization()
        {
            _config = Configuration.Load();
            _undertaleConfigFiles = new Dictionary<string, FileInfo>();

            CheckConfiguration();
            LoadFormData();
        }

        private void LoadFormData()
        {
            // Not sure which FUN value wins out. I'm going to go with undertale.ini for the time being.
            // TODO: Read from the .ini file and set it in the main form
            _mainView.FunValue = 66; // TODO: This is temporary
        }

        private void CheckConfiguration()
        {
            var undertaleDataDir = new DirectoryInfo(_config.UndertaleDataPath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UNDERTALE"));
            if (!undertaleDataDir.Exists)
            {
                PromptUserForUndertaleDataDirectory();
            }

            var files = new[] { FILE0, UNDERTALE_INI };
            var dataFiles = undertaleDataDir.GetFiles();
            var matchingFileCount = 0;
            foreach (var file in files)
            foreach (var dataFile in dataFiles)
            {
                if (dataFile.Name.Equals(file, StringComparison.InvariantCultureIgnoreCase))
                {
                    _undertaleConfigFiles[file] = dataFile;
                    matchingFileCount++;
                    break;
                }
            }

            if (matchingFileCount != files.Length)
            {
                PromptUserForUndertaleDataDirectory();
            }
        }

        private void PromptUserForUndertaleDataDirectory()
        {
            MessageBox.Show("Undertale data directory not found. Please browse for it now.", "Undertale directory error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            dialog.ShowDialog();
        }
    }
}
