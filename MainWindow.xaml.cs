using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ConvertHEIC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string HEIC_FILE_PATH = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestExe(object sender, RoutedEventArgs e)
        {
            if(Directory.Exists(HEIC_FILE_PATH))
            {
                //string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string ps1File = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "ConvertToJpg.ps1");
                string[] files = Directory.GetFiles(HEIC_FILE_PATH);

                foreach (string file in files)
                {
                    var startInfo = new ProcessStartInfo()
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{ps1File}\" \"{file}\"",
                        UseShellExecute = false
                    };
                    Process.Start(startInfo);

                    string logText = $"{file} is converted";
                    logArea.Text = logArea.Text + "\n" + logText;
                }
            }
            else
            {
                // Configure the message box to be displayed
                string messageBoxText = "Folder does not exist";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private void LoadImages(object sender, RoutedEventArgs e)
        {
            HEIC_FILE_PATH = fileNameTextBox.Text;
            if(Directory.Exists(HEIC_FILE_PATH)==false)
            {
                // Configure the message box to be displayed
                string messageBoxText = "Folder does not exist";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            convertBtn.IsEnabled = true;
            CountHeicFiles(HEIC_FILE_PATH);
        }

        private void CountHeicFiles(string path)
        {
            int counter = 0;
            string[] files = Directory.GetFiles(path);
            foreach(string file in files)
            {
                if(file.EndsWith(".heic"))
                {
                    counter++;
                }
            }

            string logText = $"{counter} heic-files are found";
            logArea.Text = logArea.Text + "\n" + logText;
        }
    }
}
