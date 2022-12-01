using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;

namespace MyWordPad.xaml
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetF1CommandBinding();
        }

        private void SetF1CommandBinding()
        {
            CommandBinding helpBinding = new CommandBinding(ApplicationCommands.Help);
            helpBinding.CanExecute += CanHelpExecute;
            helpBinding.Executed += HelpExecuted;
            CommandBindings.Add(helpBinding);
        }
        private void CanHelpExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Look, it is not that difficult. Just type something!", "Help!");
        }

        protected void FileExit_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
       protected void ToolsSpellingHints_Click(object sender, RoutedEventArgs args)
        {
            string spellingHints = string.Empty;
            SpellingError error = txtData.GetSpellingError(txtData.CaretIndex);
            if(error != null)
            {
                foreach (string s in error.Suggestions)
                {
                    spellingHints += string.Format("{0}/n", s);
                }
                lblSpellingHints.Content = spellingHints;
                expanderSpelling.IsExpanded = true;
            }
        }
        protected void MouseEnterExitArea(object sender, RoutedEventArgs args)
        {
            statBarText.Text = "Exit the Application";
        }
        protected void MouseEnterToolsHintsArea(object sender, RoutedEventArgs args)
        {
            statBarText.Text = "Show Spelling Suggestons";
        }
        protected void MouseLeaveArea(object sender, RoutedEventArgs args)
        {
            statBarText.Text = "Ready";
        }

        private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Text Files |*.txt";

            if(true == openDlg.ShowDialog())
            {
                string dataFromFile = File.ReadAllText(openDlg.FileName);
                txtData.Text = dataFromFile;
            }
        }
        private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "Text Files |*.text";

            if(true == saveDlg.ShowDialog())
            {
                File.WriteAllText(saveDlg.FileName, txtData.Text);
            }
        }
    }
}
