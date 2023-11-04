namespace Windrad.Application.WinFormAPP
{
  public partial class Form1 : Form
  {
    private PictureBox pictureBox;
    private int rotationSpeed = 0;
    const string settingFileName = $"windradSetting.txt";
    const string settingFilePath = $"@C:\\Temp\\Windrad";
    public Form1()
    {
      InitializeComponent();
      SetWindradSettings();
      DrawWindrad();
      StartThreadOperation();
    }

    private void SetWindradSettings()
    {
      if (File.Exists(settingFileName))
      {
        try
        {
          string fileContent = File.ReadAllText(settingFilePath);
          if (int.TryParse(fileContent, out int speedFromFile))
          {
            if (speedFromFile > 0 && speedFromFile <= 10)
            {
              rotationSpeed = speedFromFile;
            }
          }
        }
        catch (Exception)
        {

          throw;
        }
      }
      else
      {
        MessageBox.Show("You need to set a setting file to starting to windrad rotation.");
      }
    }

    private void DrawWindrad()
    {
      PictureBox pictureBox = new();
      pictureBox.Image = Image.FromFile("sampleWindrad.png");
      pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      pictureBox.Location = new Point(10, 10);
      pictureBox.Size = new System.Drawing.Size(150, 150);
      Controls.Add(pictureBox);
    }

    private void StartThreadOperation()
    {
      Thread thread = new Thread(RotateWindrad);
      thread.Start();
    }

    private void RotateWindrad(object? obj)
    {
      while (true) 
      {
        pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
        Thread.Sleep(rotationSpeed); 
        break;
      }
    }
  }
}