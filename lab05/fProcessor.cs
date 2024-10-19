using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Lab05
{
    public partial class fProcessor : Form
    {
        internal Processor TheProcessor;

        internal fProcessor(Processor p)
        {
            TheProcessor = p;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            TheProcessor.name = tbName.Text.Trim();
            TheProcessor.manufacturer = tbManufacturer.Text.Trim();
            TheProcessor.core = int.Parse(tbCores.Text.Trim());
            TheProcessor.frequency = int.Parse(tbFrequency.Text.Trim());
            TheProcessor.tdp = int.Parse(tbTDP.Text.Trim());
            TheProcessor.performancePerCore = int.Parse(tbPerformancePerCore.Text.Trim());

            TheProcessor.multiPrecision = chbMP.Checked;
            TheProcessor.energySaving = chbES.Checked;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void fProcessor_Load(object sender, EventArgs e)
        {

            if (TheProcessor != null)
            {
                tbName.Text = TheProcessor.name;
                tbManufacturer.Text = TheProcessor.manufacturer;
                tbCores.Text = TheProcessor.core.ToString();
                tbFrequency.Text = TheProcessor.frequency.ToString();
                tbTDP.Text = TheProcessor.tdp.ToString();
                tbPerformancePerCore.Text = TheProcessor.performancePerCore.ToString();

                chbMP.Checked = TheProcessor.multiPrecision;
                chbES.Checked = TheProcessor.energySaving;
            }
        }
    }
}
