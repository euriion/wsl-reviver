using Quartz;
using Quartz.Impl;
using WslReviver;
using Tomlyn;
using System.IO;

namespace WslReviver
{
    public partial class MainForm : Form
    {
        // status variable
        private int statusCode = 0;
        private IScheduler? scheduler;
        private int intervalMinutes = 1; // 기본 5분 간격
        private int maxLogLines = 50000;
        public WslProcessManager ProcController;
        private string configPath = "config.toml";

        public MainForm()
        {
            InitializeComponent();
            LoadConfig();
            ProcController = new WslProcessManager();
            InitializeScheduler();
            UpdateButtonText();
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(configPath))
                {
                    string configText = File.ReadAllText(configPath);
                    var config = Toml.ToModel<ConfigModel>(configText);
                    
                    intervalMinutes = config.General.IntervalMinutes;
                    maxLogLines = config.General.MaxLogLines;
                    
                    // UI 업데이트
                    intervalNumericUpDown.Value = intervalMinutes;
                }
                else
                {
                    AddLogLine("설정 파일을 찾을 수 없습니다. 기본값을 사용합니다.");
                }
            }
            catch (Exception ex)
            {
                AddLogLine($"설정 파일 로드 중 오류 발생: {ex.Message}");
            }
        }

        private void UpdateButtonText()
        {
            btnStart.Text = statusCode == 0 ? "시작" : "중지";
        }

        private async void InitializeScheduler()
        {
            // Quartz 스케줄러 초기화
            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();
        }

        private async Task ScheduleJob()
        {
            if (scheduler == null) return;

            // 작업 정의
            IJobDetail job = JobBuilder.Create<WslReviverJob>()
                .WithIdentity("wslReviverJob", "group1")
                .Build();

            // 트리거 정의
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("wslReviverTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(intervalMinutes)
                    .RepeatForever())
                .Build();

            // 작업 스케줄링
            await scheduler.ScheduleJob(job, trigger);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (scheduler == null) return;

            if (statusCode == 0)
            {
                // start the process
                statusLabel.Text = "Process started";
                statusCode = 1;
                await scheduler.Start();
                await ScheduleJob();
            }
            else if (statusCode == 1)
            {
                // stop the process
                statusLabel.Text = "Process stopped";
                statusCode = 0;
                await scheduler.Shutdown();
            }
            else
            {
                statusLabel.Text = "Unknown status";
            }
            UpdateButtonText();
        }

        private async void SetInterval(int minutes)
        {
            intervalMinutes = minutes;
            if (statusCode == 1 && scheduler != null)
            {
                await scheduler.Shutdown();
                await scheduler.Start();
                await ScheduleJob();
            }
        }

        private void intervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SetInterval((int)intervalNumericUpDown.Value);
        }

        public void AddLogLine(String message)
        {
            if (textBoxLogging.Lines.Length >= maxLogLines)
            {
                textBoxLogging.Lines = textBoxLogging.Lines.Skip(textBoxLogging.Lines.Length - maxLogLines + 1).ToArray();
            }

            textBoxLogging.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
            textBoxLogging.SelectionStart = textBoxLogging.Text.Length;
            textBoxLogging.ScrollToCaret();
        }
        private void btnShowProcessList_Click(object sender, EventArgs e)
        {
            // this.ProcController.ShowProcessList();

            String currentStatus = "";

            if (ProcController.IsWslProcessRunning())
            {
                currentStatus = "WSL 프로세스가 실행 중입니다.";
            }
            else
            {
                currentStatus = "WSL 프로세스가 실행되어 있지 않습니다.";
            }
            this.AddLogLine(currentStatus);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.AddLogLine("WSL을 시작합니다");
            this.ProcController.ReviverWslProcess();
        }

        private void stopWslbutton_Click(object sender, EventArgs e)
        {
            this.AddLogLine("WSL을 종료합니다");
            this.ProcController.killWslProcess();
        }
    }

    public class WslReviverJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            // UI 스레드에서 실행하기 위해 MainForm 인스턴스에 접근
            if (Application.OpenForms["MainForm"] is MainForm form)
            {
                form.Invoke(new Action(() =>
                {
                    form.AddLogLine($"{DateTime.Now}: 스케줄 태스크 작동");
                    form.ProcController.ReviverWslProcess();
                }));
            }

            return Task.CompletedTask;
        }
    }

    public class ConfigModel
    {
        public GeneralConfig General { get; set; } = new();
        public ProcessConfig Process { get; set; } = new();
    }

    public class GeneralConfig
    {
        public int IntervalMinutes { get; set; } = 1;
        public int MaxLogLines { get; set; } = 50000;
    }

    public class ProcessConfig
    {
        public string WslProcessName { get; set; } = "wsl.exe";
    }
}
