using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab05
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void Lab04_Load(object sender, EventArgs e)
        {

            gvProcessor.AutoGenerateColumns = false;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "name";
            column.Name = "Назва";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "manufacturer";
            column.Name = "Виробник";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "core";
            column.Name = "Кількість ядер";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "frequency";
            column.Name = "Частота";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "tdp";
            column.Name = "Тепловіділення";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "performancePerCore";
            column.Name = "Продуктивність";
            gvProcessor.Columns.Add(column);


            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "multiPrecision";
            column.Name = "Багатопоточність";
            column.Width = 100;
            gvProcessor.Columns.Add(column);


            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "energySaving";
            column.Name = "Режим енергозбереження";
            column.Width = 120;
            gvProcessor.Columns.Add(column);


            bindSrcProcessor.Add(new Processor("Inter Core  і5","Inter", 4, 3.0, 65, 250, true, true));
            EventArgs args = new EventArgs(); OnResize(args);

        }


        private void fMain_Resize(object sender, EventArgs e)
        {
            int buttonsSize = 5 * btnAdd.Width + 2 * tsSeparator1.Width + 30;
            btnExit.Margin = new Padding(Width - buttonsSize, 0, 0, 0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Processor processor = new Processor();

            fProcessor ft = new fProcessor(processor);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                bindSrcProcessor.Add(processor);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Processor processor = (Processor)bindSrcProcessor.List[bindSrcProcessor.Position];

            fProcessor ft = new fProcessor(processor);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                bindSrcProcessor.List[bindSrcProcessor.Position] = processor;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити поточний запис?", "Видалення запису",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                bindSrcProcessor.RemoveCurrent();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистити таблицю?\n\nВсі дані будуть втрачені", "Очищення даних",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                bindSrcProcessor.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити застосунок?", "Вихід з програми", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void FMain_Resize(object sender, EventArgs e)
        {
            int buttonsSize = 9 * btnAdd.Width + 3 * tsSeparator1.Width + 30;
            btnExit.Margin = new Padding(Width - buttonsSize, 0, 0, 0);
        }


        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
            {
                saveFileDialog.Filter = "Текстові файли (*.txt) |*.txt|All files (*.*) |*.*";
                saveFileDialog.Title = "Зберегти дані у текстовому форматі";
                saveFileDialog.InitialDirectory = Application.StartupPath;
                StreamWriter sw;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                    try
                    {
                        foreach (Processor processor in bindSrcProcessor.List)
                        {
                            sw.Write(processor.name + "\t" + processor.manufacturer + "\t" +
                                processor.core + "\t" + processor.frequency + "\t" +
                                processor.tdp + "\t" + processor.performancePerCore +
                                "\t" + processor.multiPrecision + "\t" + processor.energySaving + "\t\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
            }
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            {
                saveFileDialog.Filter = "Файли даних (*.processor) |*.processor|All files (*.*) |*.*";
                saveFileDialog.Title = "Зберегти дані у бінарному форматі";
                saveFileDialog.InitialDirectory = Application.StartupPath;
                BinaryWriter bw;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bw = new BinaryWriter(saveFileDialog.OpenFile());
                    try
                    {
                        foreach (Processor processor in bindSrcProcessor.List)
                        {
                            bw.Write(processor.name);
                            bw.Write(processor.manufacturer);
                            bw.Write(processor.core);
                            bw.Write(processor.frequency);
                            bw.Write(processor.tdp);
                            bw.Write(processor.performancePerCore);
                
                            bw.Write(processor.multiPrecision);
                            bw.Write(processor.energySaving);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        bw.Close();
                    }
                }
            }
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Текстові файли (*.txt) |*.txt|All files (*.*) |*.*";
                openFileDialog.Title = "Прочитати дані у текстовому форматі";
                openFileDialog.InitialDirectory = Application.StartupPath;
                StreamReader sr;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bindSrcProcessor.Clear();
                    sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
                    string s;
                    try
                    {
                        while ((s = sr.ReadLine()) != null)
                        {
                            string[] split = s.Split('\t');
                            Processor processor = new Processor(split[0], split[1], int.Parse(split[2]),
                             double.Parse(split[3]), double.Parse(split[4]), double.Parse(split[5]),
                             bool.Parse(split[6]), bool.Parse(split[7]));

                            bindSrcProcessor.Add(processor);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sr.Close();
                    }
                }
            }
        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Файли даних (*.processor) |*.processor|All files (*.*) |*.*";
                openFileDialog.Title = "Прочитати дані у бінарному форматі";
                openFileDialog.InitialDirectory = Application.StartupPath;
                BinaryReader br;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bindSrcProcessor.Clear();
                    br = new BinaryReader(openFileDialog.OpenFile());
                    try
                    {
                        Processor processor; while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            processor = new Processor();
                            for (int i = 1; i <= 10; i++)
                            {
                                switch (i)
                                {
                                    case 1:
                                        processor.name = br.ReadString(); break;
                                    case 2:
                                        processor.manufacturer = br.ReadString(); break;
                                    case 3:
                                        processor.core = br.ReadInt32(); break;
                                    case 4:
                                        processor.frequency = br.ReadDouble(); break;
                                    case 5:
                                        processor.tdp = br.ReadDouble(); break;
                                    case 6:
                                        processor.performancePerCore = br.ReadDouble(); break;
                                    case 7:
                                        processor.multiPrecision = br.ReadBoolean(); break;
                                    case 8:
                                        processor.energySaving = br.ReadBoolean(); break;
                                }
                            }
                            bindSrcProcessor.Add(processor);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        br.Close();
                    }
                }
            }
        }
    }
}
