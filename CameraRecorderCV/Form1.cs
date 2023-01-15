using Emgu.CV;
using Emgu.CV.Structure;
// using Emgu.Util;
// using Emge.CV.Util;

using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace CameraRecorderCV
{
    public partial class Form1 : Form
    {
        // private List<Dictionary<int, string>> camerasList = new List<Dictionary<int, string>>();
        private List<VideoCapture> camerasList = new List<VideoCapture>();
        
        
        private string _codec;        
        private Settings _settings = new Settings();
        private Recorder _recorder = new Recorder();
        
        public EventHandler _onRefresh;
        public delegate void UIRefresher();
        public event UIRefresher OnRefresh;             
        

        public Form1()
        {
            InitializeComponent();
            DetectCameras();
            InitializeComponentForm();
            
            _settings.InitFromFile();
            _recorder.NewFrame += HndRefresh;
            
        }

        private void InitializeComponentForm()
        {                   
            cmbResolutions.Items.AddRange(ResolutionInfo.ResolutionList.ToArray());
            this.cmbResolutions.SelectedIndex = 5;
            
            _settings.InitFromFile();
            tbSaveTo.Text = _settings.saveTo;
           
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
                var stngs = RecordSettings;
                stngs.isDuty = true;
                _recorder.Start(stngs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception", MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }
        }
        
        private RecordSettings RecordSettings
        {
            get
            {
                var settings = new RecordSettings();
                settings.resolution = (Resolution)cmbResolutions.SelectedItem;
                settings.fourcc = -1;
                settings.folderPath = tbSaveTo.Text;
                
                // get camera
                var index = cmbCameras.SelectedIndex;
                 
                settings.videoCapture = camerasList[index];
                return settings;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {            
            _recorder.Stop();
            this.pictureBox1.Image.Dispose();
            this.pictureBox1.Image = null;
            DetectCameras();
        }
       
        private void HndRefresh(Bitmap btmp)
        {
            pictureBox1.Image = btmp;
            //pictureBox1.Refresh();
        }
   
        private void tbSaveTo_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            _settings.saveTo = fbd.SelectedPath;
            tbSaveTo.Text = _settings.saveTo;
            _settings.SaveToFile();
        }
    }
}