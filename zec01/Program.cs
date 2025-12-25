using System.Net;
using System.Net.Sockets;

// 1. 서버 주소 설정 (port 9000 대기)
const int port = 9000;
IPEndPoint ipPoint = new(IPAddress.Any, port);

// 2. 소켓 엔진 초기화 (TCP/IP 통신용)
using Socket server = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    server.Bind(ipPoint);
    server.Listen(10);  // 최대 10개의 대기 연결 허용

    Console.WriteLine(">>> [ZENITH-ECHO_CORE-01] SYSTEM ONLINE");
    Console.WriteLine($">>> STATUS: LISTENING ON PORT {port}");
    Console.WriteLine(">>> ARCH: ARM64 NATIVE (SNAPDRAGON X)");

    while (true)
    {
        // 3. 접속 대기 (비동기 IOCP 방식)
        using Socket client = await server.AcceptAsync();
        Console.WriteLine($"[LOG] CONNECTED: {client.RemoteEndPoint}");
        
        // 4. 데이터 수신용 버퍼 (4KB)
        byte[] buffer = new byte[4096];
        
        // 5. 에코(ECHO) 로직: 받은 대로 돌려주기
        int received = await client.ReceiveAsync(buffer, SocketFlags.None);
        if (received > 0)
        {
            // 받은 데이터를 다시 클라이언트에게 전송
            await client.SendAsync(buffer.AsMemory(0, received), SocketFlags.None);
            Console.WriteLine($"[LOG] ECHOED {received} BYTES TO {client.RemoteEndPoint}");
        }
        
        // 6. 안전하게 연결 종료
        client.Shutdown(SocketShutdown.Both);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"[CRITICAL ERROR] {ex.Message}");
}