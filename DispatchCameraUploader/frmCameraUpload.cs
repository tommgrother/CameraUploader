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

namespace DispatchCameraUploader
{
    public partial class frmCameraUpload : Form
    {
        public frmCameraUpload()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar2.Minimum = 0;
                progressBar2.Maximum = 100;
               
                progressBar2.Value = 0;

                float totalFileCount;
                float newFileCount;
                float progressValue;
                int progressValueInt;



                string path = txtSource.Text;
                var files = Directory.EnumerateFiles(path, "*.jpg", SearchOption.AllDirectories)
                         .Select(fn => new FileInfo(fn));
                var fileDateGroups = files.GroupBy(fi => fi.LastWriteTime.Date);

                totalFileCount = files.Count();


                

                foreach (var dateGroup in fileDateGroups)
                {
                    string dir = Path.Combine(@"\\designsvr1\Public\Dispatch pictures", dateGroup.Key.ToString("yyyy-MM-dd"));
                    //string dir = Path.Combine(@"C:\Users\Aled\Desktop\2", dateGroup.Key.ToString("yyyy-MM-dd"));

                    Directory.CreateDirectory(dir);
                    foreach (var file in dateGroup)
                    {
                        string newPath = Path.Combine(dir, file.Name);
                        File.Copy(file.FullName, newPath,true);
                        File.Delete(file.FullName);

                        newFileCount = files.Count();

                        progressValue = (1 - (newFileCount / totalFileCount)) * 100 ;
                        progressValueInt = (int)Math.Round(progressValue);

                        progressBar2.Value = progressValueInt;
                        progressBar2.Update();


                    }
                }
                MessageBox.Show("File Upload Complete!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        
    }
    }

