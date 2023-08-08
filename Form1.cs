using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DamagedPixelDetection
{
    public partial class Form1 : Form
    {
        Image originalimage1;
        Image originalimage2;
        string mode = "";

        int borderx = 0;
        int bordery = 0;
        int borderw = 0;
        int borderh = 0;

        string file1 = "";
        string file2 = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void btnOpenRGB_Click(object sender, EventArgs e)
        {
            //파일오픈창 생성 및 설정
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "파일 오픈 예제창";
            ofd.FileName = "";
            ofd.Filter = "그림 파일 (*.jpg, *.gif, *.bmp) | *.jpg; *.gif; *.bmp; | 모든 파일 (*.*) | *.*";

            //파일 오픈창 로드
            DialogResult dr = ofd.ShowDialog();

            //OK버튼 클릭시
            if (dr == DialogResult.OK)
            {
                //File명과 확장자를 가지고 온다.
                string fileName = ofd.SafeFileName;
                //File경로와 File명을 모두 가지고 온다.
                string fileFullName = ofd.FileName;
                //File경로만 가지고 온다.
                string filePath = fileFullName.Replace(fileName, "");

                pictureBox1.ImageLocation = fileFullName;
                file1 = fileFullName;
            }
        }

        private void btnOpenSpectrum_Click(object sender, EventArgs e)
        {
            //파일오픈창 생성 및 설정
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "파일 오픈 예제창";
            ofd.FileName = "";
            ofd.Filter = "그림 파일 (*.jpg, *.gif, *.bmp) | *.jpg; *.gif; *.bmp; | 모든 파일 (*.*) | *.*";

            //파일 오픈창 로드
            DialogResult dr = ofd.ShowDialog();

            //OK버튼 클릭시
            if (dr == DialogResult.OK)
            {
                //File명과 확장자를 가지고 온다.
                string fileName = ofd.SafeFileName;
                //File경로와 File명을 모두 가지고 온다.
                string fileFullName = ofd.FileName;
                //File경로만 가지고 온다.
                string filePath = fileFullName.Replace(fileName, "");

                pictureBox2.ImageLocation = fileFullName;
                file2 = fileFullName;
            }
        }

        private void btnCalibration_Click(object sender, EventArgs e)
        {
            mode = "calibration";
            status.Text = "Mode: Calibration";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PropertyInfo imageRectangleProperty = typeof(PictureBox).GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);


            if (pictureBox1.Image != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                Bitmap original = (Bitmap)pictureBox1.Image;

                Color color = Color.White;
                switch (pictureBox1.SizeMode)
                {
                    case PictureBoxSizeMode.Normal:
                    case PictureBoxSizeMode.AutoSize:
                        {
                            color = original.GetPixel(me.X, me.Y);
                            break;
                        }
                    case PictureBoxSizeMode.CenterImage:
                    case PictureBoxSizeMode.StretchImage:
                    case PictureBoxSizeMode.Zoom:
                        {
                            Rectangle rectangle = (Rectangle)imageRectangleProperty.GetValue(pictureBox1, null);
                            if (rectangle.Contains(me.Location))
                            {
                                using (Bitmap copy = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height))
                                {
                                    using (Graphics g = Graphics.FromImage(copy))
                                    {
                                        g.DrawImage(pictureBox1.Image, rectangle);

                                        color = copy.GetPixel(me.X, me.Y);
                                    }
                                }
                            }
                            break;
                        }
                }

                if (color == Color.White)
                {
                    //User clicked somewhere there is no image
                }
                else
                {
                    //use color.Value
                    c1.BackColor = color;
                    c1.Text = color.R.ToString() + ", " + color.G.ToString() + ", " + color.B.ToString() + ", " + color.A.ToString();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PropertyInfo imageRectangleProperty = typeof(PictureBox).GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);


            if (pictureBox2.Image != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                Bitmap original = (Bitmap)pictureBox2.Image;

                if (mode == "calibration")
                {
                    Color color = Color.White;
                    switch (pictureBox2.SizeMode)
                    {
                        case PictureBoxSizeMode.Normal:
                        case PictureBoxSizeMode.AutoSize:
                            {
                                color = original.GetPixel(me.X, me.Y);
                                break;
                            }
                        case PictureBoxSizeMode.CenterImage:
                        case PictureBoxSizeMode.StretchImage:
                        case PictureBoxSizeMode.Zoom:
                            {
                                Rectangle rectangle = (Rectangle)imageRectangleProperty.GetValue(pictureBox2, null);
                                if (rectangle.Contains(me.Location))
                                {
                                    using (Bitmap copy = new Bitmap(pictureBox2.ClientSize.Width, pictureBox2.ClientSize.Height))
                                    {
                                        using (Graphics g = Graphics.FromImage(copy))
                                        {
                                            g.DrawImage(pictureBox2.Image, rectangle);

                                            color = copy.GetPixel(me.X, me.Y);
                                        }
                                    }
                                }
                                break;
                            }
                    }

                    if (color == Color.White)
                    {
                        //User clicked somewhere there is no image
                    }
                    else
                    {
                        //use color.Value
                        c2.BackColor = color;
                        c2.Text = color.R.ToString() + ", " + color.G.ToString() + ", " + color.B.ToString() + ", " + color.A.ToString();
                    }
                }
                else if(mode == "border")
                {
                    Image ti1 = pictureBox1.Image;
                    Image ti2 = pictureBox2.Image;
                    int rx = CoordinateCalculation1(me.X, pictureBox2.ClientSize.Width, ti2.Width);
                    int ry = CoordinateCalculation1(me.Y, pictureBox2.ClientSize.Height, ti2.Height);

                    if (borderx == 0 && bordery == 0)
                    {
                        borderx = rx;
                        bordery = ry;
                        status.Text = "New Border Coordinate X:" + rx.ToString () + " Y:"+ry.ToString();
                    }
                    else if(borderw == 0 && borderh == 0)
                    {
                        borderw = rx - borderx;
                        borderh = ry - bordery;
                        status.Text = "New Border Coordinate X:" + rx.ToString() + " Y:" + ry.ToString() + " W:" + borderw.ToString() + " H:" + borderh.ToString();

                        Graphics grp = Graphics.FromImage(ti1);
                        grp.DrawRectangle(new Pen(Color.Black, 8), new Rectangle(borderx, bordery, borderw, borderh));
                        pictureBox1.Image = ti1;

                        grp = Graphics.FromImage(ti2);
                        grp.DrawRectangle(new Pen(Color.Black, 8), new Rectangle(borderx, bordery, borderw, borderh));
                        pictureBox2.Image = ti2;
                    }
                }
                else if(mode == "limit")
                {
                    Image ti1 = pictureBox1.Image;
                    Image ti2 = pictureBox2.Image;
                    int rx = CoordinateCalculation1(me.X, pictureBox2.ClientSize.Width, ti2.Width);
                    int ry = CoordinateCalculation1(me.Y, pictureBox2.ClientSize.Height, ti2.Height);

                    Graphics grp = Graphics.FromImage(ti1);
                    grp.FillRectangle(new SolidBrush(Color.Black), new Rectangle(rx-50, ry-50, 100, 100));
                    pictureBox1.Image = ti1;

                    grp = Graphics.FromImage(ti2);
                    grp.FillRectangle(new SolidBrush(Color.Black), new Rectangle(rx - 50, ry - 50, 100, 100));
                    pictureBox2.Image = ti2;
                }
            }
        }

        private void btnBorder_Click(object sender, EventArgs e)
        {
            mode = "border";
            status.Text = "Mode: Border";

            borderx = 0;
            bordery = 0;
            borderw = 0;
            borderh = 0;
        }

        private void btnLimit_Click(object sender, EventArgs e)
        {
            mode = "limit";
            status.Text = "Mode: Limit";
        }

        public int CoordinateCalculation1(int a, int b, int c)
        {
            RedEyeEngine.Engine Engine = new RedEyeEngine.Engine();
            string result = Engine.HttpSend("CONTENTS", "utf-8", "GET", "https://superblaze.net/app/damagedpixelanalysis/process.php?key=n&c=cc&code=" + txtCode.Text + "&p1="+a.ToString()+"&p2="+b.ToString()+"&p3=" + c.ToString(), new List<string>(), new StringBuilder(), "", 0);

            int rc = -1;
            try
            {
                rc = Convert.ToInt32(result);
            }
            catch
            {

            }

            return rc;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {

        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            if(txtCode.Text == "")
            {
                MessageBox.Show("코드없음");
                return;
            }

            if(file1 == "" || file2 == "")
            {
                MessageBox.Show("파일없음");
                return;
            }

            if (borderx == 0 || bordery == 0 || borderw == 0 || borderh == 0)
            {
                MessageBox.Show("보더설정안됨");
                return;
            }

            RedEyeEngine.Engine Engine = new RedEyeEngine.Engine();
            string result2 = Engine.HttpSend("CONTENTS", "utf-8", "GET", "https://superblaze.net/app/damagedpixelanalysis/process.php?key=n&c=ic&code=" + txtCode.Text + "&name=border&data=" + borderx.ToString() + "/" + bordery.ToString() + "/" + borderw.ToString() + "/" + borderh.ToString(), new List<string>(), new StringBuilder(), "", 0);

            System.Net.WebClient Client = new System.Net.WebClient();
            Client.Headers.Add("Content-Type", "binary/octet-stream");

            byte[] result = Client.UploadFile("https://superblaze.net/app/damagedpixelanalysis/uploadjpg.php", "POST", Application.StartupPath + "\\temp1.jpg");
            string s1 = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);

            result = Client.UploadFile("https://superblaze.net/app/damagedpixelanalysis/uploadjpg.php", "POST", Application.StartupPath + "\\temp2.jpg");
            string s2 = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);

            info.Text = s1 + " / " + s2;
            txtResult.Text = s1 + "/" + s2;
        }

        private void btnPreprocess_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save(Application.StartupPath + "\\temp1.jpg");
            pictureBox2.Image.Save(Application.StartupPath + "\\temp2.jpg");
        }
    }
}
