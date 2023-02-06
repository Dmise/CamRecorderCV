using CameraRecordLib;
using Emgu.CV;
using Emgu.CV.Structure;
// using Emgu.Util;
// using Emge.CV.Util;

using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace TestForm
{
    public partial class Form1 : Form
    {
        // private List<Dictionary<int, string>> camerasList = new List<Dictionary<int, string>>();
        private List<VideoCapture> camerasList = new List<VideoCapture>();              
        private string _codec;
        private RecordManager _recorder = new RecordManager();       
                      
        public Form1()
        {
            InitializeComponent();
            DetectCameras();
            InitializeFormData();            
            _recorder.InitFromFile();
            _recorder.NewFrame += HndRefreshImage;
            _recorder.UpdateStatus += UIRefresh;         
        }

        private void UIRefresh(object sender, EventArgs args)
        {
            
            if (_recorder.dutyRecording)
            {
                labShowDutyVideoStatus.Text = "Recording";
            }
            else
            {
                labShowDutyVideoStatus.Text = "Idle";
            }

            if(_recorder.fragmentRecording)
            {
                labShowFragmentVideoStatus.Text = "Recording";
            }
            else
            {
                labShowFragmentVideoStatus.Text = "Idle";
            }
        }

        private void InitializeFormData()
        {
            try
            {
                cmbResolutions.Items.AddRange(ResolutionInfo.ResolutionList.ToArray());
                this.cmbResolutions.SelectedIndex = 5;
                _recorder.InitFromFile();
                this.tbSaveTo.Text = _recorder.SaveTo;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        void DetectCameras()
        {
            camerasList.Clear();
            cmbCameras.Items.Clear();
            int counter = 0;
            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    var capture = new VideoCapture(i);
                    if (capture.IsOpened)
                        camerasList.Add(capture);   
                    counter++;
                }
                // in case of exception we cancel camera serching
                catch 
                {
                    continue;
                }   
            }

            foreach (VideoCapture capture in camerasList)
            {
                cmbCameras.Items.Add(capture.BackendName);
                if (cmbCameras.Items.Count > 0)
                {
                    cmbCameras.SelectedIndex = 0;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                GrabDataFromUI();
                _recorder.Start(RecordType.Duty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception", MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Grab fields value from UI to setup Settings
        /// </summary>
        private void GrabDataFromUI()
        {                       
            _recorder.RecordSettings.resolution = (Resolution)cmbResolutions.SelectedItem;               
            _recorder.RecordSettings.SaveTo = tbSaveTo.Text;
                
            // get camera
            var index = cmbCameras.SelectedIndex;                 
            _recorder.VideoCapture = camerasList[index];           
        }
      
        private void btnStop_Click(object sender, EventArgs e)
        {            
            _recorder.StopDuty();
            this.pictureBox1.Image.Dispose();
            this.pictureBox1.Image = null;
            DetectCameras();
        }
       
        private void HndRefreshImage(Bitmap btmp)
        {
            pictureBox1.Image = btmp;
            //pictureBox1.Refresh();
        }
   
        private void tbSaveTo_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            _recorder.SaveTo = fbd.SelectedPath;
            tbSaveTo.Text = _recorder.SaveTo;
            
                     
            _recorder.SaveToFile(_recorder.SaveTo);
        }

        private void btnStartFragment_Click(object sender, EventArgs e)
        {                        
            _recorder.Start(RecordType.Fragment);
        }

        private void btnStopFragment_Click(object sender, EventArgs e)
        {
            _recorder.StopFragment();
        }
    }
}