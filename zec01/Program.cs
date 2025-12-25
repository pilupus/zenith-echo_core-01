using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 9000;
IPEndPoint ipPoint = new(IPAddress.Any, port);

using Socket server = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    server.Bind(ipPoint);
    server.Listen(100);  // 최대 100개의 대기 연결 허용

    Console.WriteLine(">>> [ZENITH-ECHO_CORE-01] SESSION-2 ONLINE");
    Console.WriteLine(">>> STATUS: MUILTI_RECV ENABLED / RESOURCE TRACKING ON");

    while (true)
    {
        // 1. 새로운 클라이언트 수락
        Socket client = await server.AcceptAsync();
        Console.WriteLine($"[CONN] New client: {client.RemoteEndPoint}");
        
        // 2. 비동기 처리를 위해 별도의 작업(Task)으로 분리 (멀티태스킹의 기초)
        _ = Task.Run(async () =>
        {
            // 'using'을 통해 이 범위가 끝나면 자동으로 소켓 자원 해제
            using (client)
            {
                byte[] buffer = new byte[1024];
                
                try
                {
                    while (true)
                    {
                        // 3. 데이터 수신 대기 (병목을 방지하는 비동기 I/O)
                        int received = await client.ReceiveAsync(buffer, SocketFlags.None);
                        
                        // 4. 수신 데이터가 0이면 클라이언트가 정상 종료한 것
                        if (received == 0) break;
                        
                        // 5. 받은 데이터를 그대로 응답 (Echo)
                        await client.SendAsync(buffer.AsMemory(0, received), SocketFlags.None);
                        
                        // 로그 출력 (시스템이 살아있음을 확인)
                        string msg = Encoding.UTF8.GetString(buffer, 0, received).Trim();
                        Console.WriteLine($"[DATA] From {client.RemoteEndPoint}: {msg}");
                        
                    }
                }
                catch (Exception)
                {
                    // 연결 강제종료 시 처리
                    client.Shutdown(SocketShutdown.Both);
                }
                
                Console.WriteLine($"[DISCONN] Client left: {client.RemoteEndPoint}");
                // 여기서 client는 using에 의해 메모리에서 해제됨. (수동 자원해제의 C# 버전)
            }
        });
    }
}
catch (Exception ex)
{
    Console.WriteLine($"[FATAL] {ex.Message}");
}