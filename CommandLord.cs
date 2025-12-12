using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;

namespace CommandLord
{
    public partial class CommandLord : Form
    {
        private bool isListening = false;
        private bool runOnceKillAndRun = false;
        private TraceEventSession? session;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private List<string> allFoundCommands = new List<string>();
        private HashSet<string> manuallyAddedCommands = new HashSet<string>(); // Add List Found ile eklenenler

        public CommandLord()
        {
            InitializeComponent();

            // Çoklu seçim modunu aktifleþtir - TÜM LISTBOXLAR ÝÇÝN
            listBoxFound.SelectionMode = SelectionMode.MultiExtended;
            listenListBox.SelectionMode = SelectionMode.MultiExtended;
            listBoxCmd.SelectionMode = SelectionMode.MultiExtended;

            // ListBoxFound için owner-draw modu
            listBoxFound.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxFound.DrawItem += ListBoxFound_DrawItem!;

            // Event handler'larý ekle
            listBoxFound.KeyDown += ListBoxFound_KeyDown!;
            listBoxFound.MouseDown += ListBoxFound_MouseDown!;

            listenListBox.KeyDown += ListenListBox_KeyDown!;
            listBoxCmd.KeyDown += ListBoxCmd_KeyDown!;

            this.MouseDown += CommandLord_MouseDown!;
        }

        private void CommandLord_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            if (isListening) StopListening();
            else if (!checkBoxAllApps.Checked && listenListBox.Items.Count == 0)
                MessageBox.Show("Liste boþ! Bir process ekleyin veya All Apps iþaretleyin.");
            else
                StartListening();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string text = textBoxListen.Text.Trim();
            if (!string.IsNullOrEmpty(text) && !listenListBox.Items.Contains(text))
                listenListBox.Items.Add(text);
            else
                MessageBox.Show("Boþ veya zaten ekli!");
        }

        private void checkBoxAllApps_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAllApps.Checked)
            {
                checkBoxKill.Checked = false;
                checkBoxKillAndRun.Checked = false;
            }
        }

        private void checkBoxKill_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKill.Checked)
            {
                checkBoxAllApps.Checked = false;
                checkBoxKillAndRun.Checked = false;
            }
        }

        private void checkBoxKillAndRun_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKillAndRun.Checked)
            {
                checkBoxAllApps.Checked = false;
                checkBoxKill.Checked = false;
            }
        }

        private void buttonAllClear_Click(object sender, EventArgs e)
        {
            // Listening aktifse durdur
            if (isListening)
            {
                StopListening();
            }

            // Listeleri ve textBox'larý temizle
            listenListBox.Items.Clear();
            listBoxFound.Items.Clear();
            listBoxCmd.Items.Clear();
            allFoundCommands.Clear();
            textBoxListen.Clear();
            textBoxSearch.Clear();

            // Checkbox ve butonlarý resetle
            checkBoxAllApps.Checked = false;
            checkBoxKill.Checked = false;
            checkBoxKillAndRun.Checked = false;
        }

        private void StartListening()
        {
            isListening = true;
            buttonListen.Text = "Listening...";
            buttonListen.ForeColor = Color.Green;
            buttonListen.BackColor = Color.Chartreuse;

            Task.Run(() =>
            {
                session = new TraceEventSession("CMDSNIFFER");
                session.EnableKernelProvider(KernelTraceEventParser.Keywords.Process);

                session.Source.Kernel.ProcessStart += (ProcessTraceData data) =>
                {
                    string procName = data.ImageFileName;
                    string cmdLine = data.CommandLine ?? "";
                    bool allApps = false;
                    bool killMode = false;
                    bool killAndRun = false;
                    List<string> targets = new List<string>();

                    Invoke(() =>
                    {
                        allApps = checkBoxAllApps.Checked;
                        killMode = checkBoxKill.Checked;
                        killAndRun = checkBoxKillAndRun.Checked;

                        foreach (object item in listenListBox.Items)
                            targets.Add(Path.GetFileName(item.ToString())?.ToLower() ?? "");
                    });

                    bool shouldAdd = false;

                    if (allApps)
                    {
                        shouldAdd = true;
                    }
                    else
                    {
                        string procFileNameOnly = Path.GetFileName(procName)?.ToLower() ?? "";
                        if (targets.Any(t => t == procFileNameOnly))
                            shouldAdd = true;
                    }

                    if (shouldAdd)
                    {
                        Invoke(() =>
                        {
                            allFoundCommands.Add(cmdLine);
                            UpdateListBox();
                        });

                        if (!allApps)
                        {
                            if (killMode)
                            {
                                try { Process.GetProcessById(data.ProcessID).Kill(); } catch { }
                            }

                            if (killAndRun && !runOnceKillAndRun)
                            {
                                runOnceKillAndRun = true;
                                try { Process.GetProcessById(data.ProcessID).Kill(); } catch { }

                                if (!string.IsNullOrWhiteSpace(cmdLine))
                                {
                                    try
                                    {
                                        Process.Start(new ProcessStartInfo
                                        {
                                            FileName = "cmd.exe",
                                            Arguments = $"/c start \"\" {cmdLine}",
                                            CreateNoWindow = true,
                                            UseShellExecute = false
                                        });
                                    }
                                    catch { }
                                }

                                Invoke(() =>
                                {
                                    checkBoxKillAndRun.Checked = false;
                                    buttonListen.Text = "Listen";
                                    buttonListen.ForeColor = Color.Black;
                                });

                                StopListening();
                            }
                        }
                    }
                };

                session.Source.Process();
            });
        }

        private void StopListening()
        {
            try { session?.Dispose(); } catch { }

            isListening = false;
            runOnceKillAndRun = false;

            Invoke(() =>
            {
                buttonListen.Text = "Listen";
                buttonListen.ForeColor = Color.White;
                buttonListen.BackColor = Color.FromArgb(54, 54, 54);
            });
        }

        private void UpdateListBox()
        {
            listBoxFound.Items.Clear();

            bool hideNulls = checkBoxHideNullValue.Checked;
            string searchText = textBoxSearch.Text.Trim().ToLower();

            var filtered = hideNulls
                ? allFoundCommands.Where(cmd =>
                {
                    string trimmed = cmd.Trim();

                    if (string.IsNullOrWhiteSpace(trimmed))
                        return false;

                    if (!trimmed.Contains(":"))
                        return false;

                    bool hasValidPath = trimmed.Contains(":\\") || trimmed.Contains(":/");
                    if (!hasValidPath)
                        return false;

                    if (trimmed.StartsWith("\""))
                    {
                        int closingQuote = trimmed.IndexOf("\"", 1);
                        if (closingQuote != -1)
                        {
                            string afterQuote = trimmed.Substring(closingQuote + 1).Trim();
                            if (string.IsNullOrWhiteSpace(afterQuote))
                                return false;
                        }
                    }
                    else
                    {
                        int firstSpace = trimmed.IndexOf(' ');
                        if (firstSpace == -1)
                            return false;

                        string afterSpace = trimmed.Substring(firstSpace + 1).Trim();
                        if (string.IsNullOrWhiteSpace(afterSpace))
                            return false;
                    }

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        return trimmed.ToLower().Contains(searchText);
                    }

                    return true;
                })
                : allFoundCommands.Where(cmd =>
                {
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        return cmd.ToLower().Contains(searchText);
                    }
                    return true;
                });

            foreach (var cmd in filtered)
            {
                listBoxFound.Items.Add(cmd);
            }
        }

        private void checkBoxHideNullValue_CheckedChanged(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        // ListBoxFound için özel çizim - Manuel eklenenler turuncu
        private void ListBoxFound_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            string itemText = listBoxFound.Items[e.Index]?.ToString() ?? "";

            if (itemText == lastAddedManually && (e.State & DrawItemState.Selected) == 0)
            {
                using (SolidBrush brush = new SolidBrush(Color.Orange))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                using (SolidBrush textBrush = new SolidBrush(Color.Black))
                {
                    e.Graphics.DrawString(itemText, e.Font!, textBrush, e.Bounds);
                }
            }
            else
            {
                e.Graphics.DrawString(itemText, e.Font!, new SolidBrush(e.ForeColor), e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        private void ListBoxFound_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (listBoxFound.SelectedItems.Count > 0)
                {
                    var selectedItems = listBoxFound.SelectedItems.Cast<object>()
                        .Select(item => item?.ToString() ?? "");

                    string textToCopy = string.Join(Environment.NewLine, selectedItems);
                    Clipboard.SetText(textToCopy);
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedItems();
            }
        }

        // ListenListBox için KeyDown - YENÝ
        private void ListenListBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedListenItems();
            }
        }

        // ListBoxCmd için KeyDown - YENÝ
        private void ListBoxCmd_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedCmdItems();
            }
        }

        // ListenListBox'tan seçili itemleri sil - YENÝ
        private void DeleteSelectedListenItems()
        {
            if (listenListBox.SelectedItems.Count == 0)
                return;

            // Seçili itemleri topla (geriye doðru sil)
            for (int i = listenListBox.SelectedIndices.Count - 1; i >= 0; i--)
            {
                listenListBox.Items.RemoveAt(listenListBox.SelectedIndices[i]);
            }
        }

        // ListBoxCmd'den seçili itemleri sil - YENÝ
        private void DeleteSelectedCmdItems()
        {
            if (listBoxCmd.SelectedItems.Count == 0)
                return;

            // Seçili itemleri topla (geriye doðru sil)
            for (int i = listBoxCmd.SelectedIndices.Count - 1; i >= 0; i--)
            {
                listBoxCmd.Items.RemoveAt(listBoxCmd.SelectedIndices[i]);
            }
        }

        private void ListBoxFound_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxFound.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    if (!listBoxFound.SelectedIndices.Contains(index))
                    {
                        listBoxFound.SelectedIndex = index;
                    }

                    ContextMenuStrip contextMenu = new ContextMenuStrip();

                    ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
                    deleteItem.Click += (s, ev) =>
                    {
                        DeleteSelectedItems();
                    };
                    contextMenu.Items.Add(deleteItem);

                    ToolStripMenuItem runCmdItem = new ToolStripMenuItem("Run with CMD");
                    runCmdItem.Click += (s, ev) =>
                    {
                        RunSelectedWithCmd();
                    };
                    contextMenu.Items.Add(runCmdItem);

                    contextMenu.Show(listBoxFound, e.Location);
                }
            }
        }

        private void DeleteSelectedItems()
        {
            if (listBoxFound.SelectedItems.Count == 0)
                return;

            List<string> itemsToRemove = new List<string>();
            foreach (var item in listBoxFound.SelectedItems)
            {
                string? itemStr = item?.ToString();
                if (!string.IsNullOrEmpty(itemStr))
                    itemsToRemove.Add(itemStr);
            }

            foreach (var item in itemsToRemove)
            {
                allFoundCommands.Remove(item);
                manuallyAddedCommands.Remove(item); // Manuel eklenenleri de temizle
            }

            UpdateListBox();
        }

        private void RunSelectedWithCmd()
        {
            if (listBoxFound.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seçili item yok!");
                return;
            }

            if (listBoxCmd.Items.Count == 0)
            {
                MessageBox.Show("CMD listesi boþ! Önce komut ekleyin.");
                return;
            }

            foreach (var selectedItem in listBoxFound.SelectedItems)
            {
                string? fullCommand = selectedItem?.ToString();
                if (string.IsNullOrEmpty(fullCommand))
                    continue;

                string arguments = ExtractArguments(fullCommand);

                if (string.IsNullOrWhiteSpace(arguments))
                {
                    MessageBox.Show($"Argüman bulunamadý: {fullCommand}");
                    continue;
                }

                foreach (var cmdItem in listBoxCmd.Items)
                {
                    string exePath = cmdItem?.ToString()?.Trim() ?? "";
                    if (string.IsNullOrEmpty(exePath))
                        continue;

                    string finalCommand = $"{exePath} {arguments}";

                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c start \"\" {finalCommand}",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Çalýþtýrma hatasý: {ex.Message}");
                    }
                }
            }
        }

        private string ExtractArguments(string fullCommand)
        {
            string trimmed = fullCommand.Trim();

            if (trimmed.StartsWith("\""))
            {
                int closingQuote = trimmed.IndexOf("\"", 1);
                if (closingQuote != -1)
                {
                    return trimmed.Substring(closingQuote + 1).Trim();
                }
            }

            int firstSpace = trimmed.IndexOf(' ');
            if (firstSpace != -1)
            {
                return trimmed.Substring(firstSpace + 1).Trim();
            }

            return "";
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CommandLord_Load(object sender, EventArgs e) { }

        private void CommandLord_KeyDown(object sender, KeyEventArgs e) { }

        private void buttonAddCmd_Click(object sender, EventArgs e)
        {
            string text = textBoxListen.Text.Trim();
            if (!string.IsNullOrEmpty(text) && !listBoxCmd.Items.Contains(text))
                listBoxCmd.Items.Add(text);
            else
                MessageBox.Show("Boþ veya zaten ekli!");
        }

        private void listBoxCmd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private string? lastAddedManually = null; // en son eklenen manuel item

        private void buttonAddListFound_Click(object sender, EventArgs e)
        {
            string text = textBoxListen.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("TextBox boþ!");
                return;
            }

            if (listBoxFound.Items.Cast<object>().Any(i => i?.ToString() == text))
            {
                MessageBox.Show("Bu deðer zaten List Found'da var!");
                return;
            }

            allFoundCommands.Add(text);
            manuallyAddedCommands.Add(text);

            lastAddedManually = text; // en son eklenen olarak iþaretle

            UpdateListBox();
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}