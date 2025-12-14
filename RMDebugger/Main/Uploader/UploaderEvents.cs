using BootloaderProtocol;
using RMDebugger.Main.Properties;
using StaticSettings;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        private void AddUploaderEvents()
        {
            HexTimeout.ValueChanged += (s, e) => Options.hexTimeout = (int)HexTimeout.Value;

            HexCheckCrc.CheckedChanged += (s, e) => Options.checkCrc = HexCheckCrc.Checked;
            HexUploadClearFileList.Click += (s, e) => MainFileObjects.Clear();

            HexUploadButton.Click += HexUploadButtonClick;
            void AddFileToFilePathListBox()
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "Hex файл (*.hex)|*.hex",
                    Title = "Выберите файлы с расширением *.hex"
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    BindingList<FileInfoClass> list = GetFileDataSource(ListOfMainFiles);
                    foreach (string file in fileDialog.FileNames)
                    {
                        FileInfoClass fileIn = list.OfType<FileInfoClass>().SingleOrDefault(x => x.Filepath == file);
                        if (fileIn == null)
                            list.Add(new FileInfoClass(file));
                    }
                    SelectedIndexChangedForFileList(ListOfMainFiles, null);
                }
            }
            HexUploadPathButton.Click += (s, e) => AddFileToFilePathListBox();

            ListOfMainFiles.DataSource = MainFileObjects;
            ListOfMainFiles.DisplayMember = "Filepath";


            HexUploadUpFile.Click += (s, e) =>
            {
                FileInfoClass file = (FileInfoClass)ListOfMainFiles.SelectedItem;
                int indexOfItem = MainFileObjects.IndexOf(file);
                if (indexOfItem > 0)
                {
                    MainFileObjects.RemoveAt(indexOfItem);
                    MainFileObjects.Insert(indexOfItem - 1, file);
                    ListOfMainFiles.SetSelected(indexOfItem - 1, true);
                }
            };
            HexUploadDownFile.Click += (s, e) =>
            {
                FileInfoClass file = (FileInfoClass)ListOfMainFiles.SelectedItem;
                int indexOfItem = MainFileObjects.IndexOf(file);
                if (indexOfItem < MainFileObjects.Count - 1)
                {
                    MainFileObjects.RemoveAt(indexOfItem);
                    MainFileObjects.Insert(indexOfItem + 1, file);
                    ListOfMainFiles.SetSelected(indexOfItem + 1, true);
                }
            };

            void DragEnter(object sender, DragEventArgs e)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
            }

            BindingList<FileInfoClass> GetFileDataSource(ListBox box)
                => (BindingList<FileInfoClass>)box.DataSource;

            void DragDropForFileList(object sender, DragEventArgs e)
            {
                string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                BindingList<FileInfoClass> list = GetFileDataSource((ListBox)sender);
                foreach (string path in filePaths)
                {
                    FileInfoClass fileIn = list.OfType<FileInfoClass>().SingleOrDefault(x => x.Filepath == path);
                    if (fileIn == null)
                        list.Add(new FileInfoClass(path));
                }
                SelectedIndexChangedForFileList(sender, null);
            }

            void SelectedIndexChangedForFileList(object sender, EventArgs e)
            {
                ListBox box = (ListBox)sender;
                if (box.SelectedIndex >= 0)
                {
                    FileInfoClass file = (FileInfoClass)box.SelectedItem;
                    UpdateBar.Maximum = file.LinesCount;
                    SetHexUploadProgress(file.Filename, end: file.LinesCount);
                }
                else
                {
                    HexFirmwareFilename.Text = "";
                    UpdateBar.Maximum = 100;
                    SetHexUploadProgress("");
                }
            }

            void RemoveFromFilePathListBox(object sender)
            {
                ListBox box = (ListBox)sender;
                BindingList<FileInfoClass> list = GetFileDataSource(box);
                list.Remove((FileInfoClass)box.SelectedItem);
            }
            void OpenInExplorer()
            {
                if (ListOfMainFiles.SelectedIndex >= 0 && HexFirmwareFilename.Text != "")
                {
                    FileInfoClass fileInfo = (FileInfoClass)ListOfMainFiles.SelectedItem;
                    if (File.Exists(fileInfo.Filepath))
                        Process.Start("explorer.exe", $" /select, \"{fileInfo.Filepath}\"");
                    else
                        MessageBox.Show($"Файла {fileInfo.Filepath} не существует");
                }
            }
            HexFirmwareFilename.Click += (s, e) => OpenInExplorer();
            HexUploadOpenInExplorer.Click += (s, e) => OpenInExplorer();
            HexUploadQueueModeCheck.CheckedChanged += (s, e) => Options.checkQueue = HexUploadQueueModeCheck.Checked;
            QueueFirstFileIsMainCheck.CheckedChanged += (s, e) => Options.checkFirstMain = QueueFirstFileIsMainCheck.Checked;

            ListOfMainFiles.DragEnter += DragEnter;

            ListOfMainFiles.DragDrop += DragDropForFileList;

            ListOfMainFiles.SelectedIndexChanged += SelectedIndexChangedForFileList;

            ListOfMainFiles.DoubleClick += (s, e) =>
            {
                ListBox box = (ListBox)s;
                BindingList<FileInfoClass> list = GetFileDataSource(box);
                if (list.Count > 0) list.Remove((FileInfoClass)box.SelectedItem);
                else AddFileToFilePathListBox();
            };

            ListOfMainFiles.KeyDown += (s, e) =>
            {
                if (e.KeyValue == (char)Keys.Delete)
                    RemoveFromFilePathListBox(s);
            };

            MainFileObjects.ListChanged += (s, e)
                => HexUploadButton.Enabled =
                    HexUploadOpenInExplorer.Enabled =
                    HexUploadDownFile.Enabled =
                    HexUploadUpFile.Enabled =
                    MainFileObjects.Count > 0;

            void HideCotrolPanel(bool sw)
            {
                Panel panel = HexUpdateSettingsPanel;
                panel.Tag = sw ? "Hided" : "Unhided";
                HexUploadControlPanelButton.Image = sw ? Resources.Settings : Resources.Unhide;
                panel.Location = new Point(sw ? panel.Location.X + 143 : panel.Location.X - 143, 0);
            }
            HexUploadControlPanelButton.Click += (s, e) => HideCotrolPanel((string)HexUpdateSettingsPanel.Tag == "Unhided");
            HideCotrolPanel((string)HexUpdateSettingsPanel.Tag == "Unhided");
        }
    }
}
