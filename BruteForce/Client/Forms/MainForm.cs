using DotNetAsyncExamples.BruteForce.Client.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetAsyncExamples.BruteForce.Client.Forms
{
    public partial class MainForm : Form
    {
        #region Fields

        private CancellationTokenSource _cancellationTokenSource;
        private HackerEngine _engine;
        
        #endregion

        #region Ctor
        
        public MainForm()
        {
            InitializeComponent();
        } 

        #endregion

        #region Overrides
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            CancellRun();
            base.OnFormClosing(e);
        } 

        #endregion
        
        #region Event Handlers

        private void OnButtonRunClick(object sender, EventArgs e)
        {
            _richTextBox.Clear();
            _cancellationTokenSource = new CancellationTokenSource();
            _engine = new HackerEngine(_cancellationTokenSource, _trackBarPassworLength.Value);
            SetControlsEnabled(false);
            _engine.Run();
            Task.Factory.StartNew(() =>
             {
                 foreach (var message in _engine.MessagePipe.GetConsumingEnumerable())
                 {
                     _richTextBox.AppendText(message);
                     _richTextBox.AppendText(Environment.NewLine);
                     Application.DoEvents();
                 }
             }, _cancellationTokenSource.Token, TaskCreationOptions.PreferFairness, TaskScheduler.FromCurrentSynchronizationContext())
             .ContinueWith(x =>
             {
                 SetControlsEnabled(true);
             }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void SetControlsEnabled(bool enabled)
        {
            _numericUpDownPassworLength.Enabled = enabled;
            _trackBarPassworLength.Enabled = enabled;
            _buttonRun.Enabled = enabled;
            _buttonCancel.Enabled = !enabled;
        }

        private void OnButtonCancelClick(object sender, EventArgs e)
        {
            SetControlsEnabled(true);
            CancellRun();
        }

        private void OnNumericUpDownValueChanged(object sender, EventArgs e)
        {
            _trackBarPassworLength.Value = Convert.ToInt32(_numericUpDownPassworLength.Value);
        }

        private void OnTrackBarValueChanged(object sender, EventArgs e)
        {
            _numericUpDownPassworLength.Value = _trackBarPassworLength.Value;
        }

        #endregion

        #region Private Methods

        private void CancellRun()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        } 

        #endregion

    }
}
