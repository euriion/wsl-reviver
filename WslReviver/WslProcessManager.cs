using System.Diagnostics;

public class WslProcessManager
{
    public WslProcessManager()
    {
        // 초기화 코드
    }

    // 현재 실행 중인 모든 프로세스를 반환
    public Process[] GetProcessList()
    {
        // 시스템에서 실행 중인 모든 프로세스를 가져와 반환
        return Process.GetProcesses();
    }

    // "wsl" 이름의 프로세스를 찾고 반환
    public Process[] findWslProcess()
    {
        // "wsl" 이름을 가진 모든 프로세스를 검색하여 반환
        var wslProcs = Process.GetProcessesByName("wsl");
        return wslProcs;
    }

    // "wsl" 이름의 프로세스를 종료
    public bool KillWslProcess()
    {
        // "wsl" 이름의 프로세스를 찾음
        var wslProcs = Process.GetProcessesByName("wsl");

        // 프로세스가 실행 중인지 확인
        if (wslProcs.Any())
        {
            foreach (var proc in wslProcs)
            {
                try
                {
                    proc.Kill(); // 프로세스 종료
                    proc.WaitForExit(); // 종료될 때까지 대기
                }
                catch
                {
                    // 프로세스 종료 실패 시 false 반환
                    return false;
                }
            }
            // 모든 프로세스 종료 성공 시 true 반환
            return true;
        }
        // 실행 중인 WSL 프로세스가 없을 경우 false 반환
        return false;
    }

    // 새로운 WSL 프로세스를 실행
    public void LaunchWslProcess()
    {
        // WSL 프로세스가 이미 실행 중이면 실행하지 않음
        if (IsWslProcessRunning())
            return;

        try
        {
            // WSL 프로세스를 실행하기 위한 설정
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "wsl.exe", // 실행할 파일 이름
                Arguments = "", // 인자를 비워두거나 필요에 따라 조정
                RedirectStandardOutput = true, // 표준 출력 리다이렉트
                RedirectStandardError = true,  // 표준 에러 리다이렉트
                UseShellExecute = false, // 반드시 false로 해야 콘솔 창이 뜨지 않음
                CreateNoWindow = true // 창 생성 비활성화
            };

            // 프로세스 실행
            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start(); // 프로세스 시작
                                 // WaitForExit을 호출하지 않음: wsl.exe가 바로 종료되는 것을 방지
            }
        }
        catch (Exception ex)
        {
            // 예외 발생 시 로그 등 처리 (필요시)
            // 예: Console.WriteLine($"WSL 실행 오류: {ex.Message}");
        }
    }

    // WSL 프로세스를 복구 (필요 시 실행)
    public bool ReviverWslProcess()
    {
        // WSL 프로세스가 이미 실행 중인 경우
        if (this.IsWslProcessRunning())
        {
            return false; // 복구가 필요하지 않음
        }
        else
        {
            // WSL 프로세스가 실행되지 않은 경우 새로 실행
            LaunchWslProcess();
            return true; // 복구 성공
        }
    }

    // WSL 프로세스가 실행 중인지 확인
    public bool IsWslProcessRunning()
    {
        // "wsl" 이름의 프로세스가 있는지 확인
        var wslProcs = Process.GetProcessesByName("wsl");
        if (wslProcs.Any())
        {
            return true; // WSL 프로세스가 실행 중
        }
        else
        {
            return false; // WSL 프로세스가 실행 중이 아님
        }
    }
}