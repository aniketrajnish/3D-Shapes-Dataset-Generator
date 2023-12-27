#if UNITY_STANDALONE_WIN
using Ookii.Dialogs;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AnotherFileBrowser.Windows
{
    public class FileBrowser
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        public FileBrowser() { }

        /// <summary>
        /// FileDialog for picking a single file
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="filepath">User picked path (Callback)</param>
        public void OpenFileBrowser(BrowserProperties browserProperties, Action<string> filepath)
        {
            var ofd = new VistaOpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = browserProperties.title == null ? "Select a File" : browserProperties.title;
            ofd.InitialDirectory = browserProperties.initialDir == null ? @"C:\" : browserProperties.initialDir;
            ofd.Filter = browserProperties.filter == null ? "All files (*.*)|*.*" : browserProperties.filter;
            ofd.FilterIndex = browserProperties.filterIndex + 1;
            ofd.RestoreDirectory = browserProperties.restoreDirectory;

            if (ofd.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK)
            {
                filepath(ofd.FileName);
            }
        }

        /// <summary>
        /// FolderDialog for picking a folder
        /// </summary>
        /// <param name="browserProperties">Special Properties of Folder Dialog</param>
        /// <param name="folderPath">User picked folder path (Callback)</param>
        public void OpenFolderBrowser(BrowserProperties browserProperties, Action<string> folderPath)
        {
            var fbd = new VistaFolderBrowserDialog();
            fbd.Description = browserProperties.title == null ? "Select a Folder" : browserProperties.title;
            fbd.SelectedPath = browserProperties.initialDir == null ? @"C:\" : browserProperties.initialDir;
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK)
            {
                folderPath(fbd.SelectedPath);
            }
        }
    }

    public class BrowserProperties
    {
        public string title; //Title of the Dialog
        public string initialDir; //Where dialog will be opened initially
        public string filter; //aka File Extension for filtering files
        public int filterIndex; //Index of filter, if there is multiple filter. Default is 0. 
        public bool restoreDirectory = true; //Restore to last return directory


        public BrowserProperties() { }
        public BrowserProperties(string title) { this.title = title; }
    }

    public class WindowWrapper : IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
}
#endif
