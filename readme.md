# ZENITH-ECHO_CORE-01

**.NET 10 기반의 고성능 비동기 에코 서버 엔진 - [단계 02: 다중 접속 및 지속 연결]**

## 1. 프로젝트 개요 (Overview)
Phase 01의 단발성 통신을 확장하여, 다중 클라이언트의 동시 접속을 지원하고 연결이 유지되는 동안 실시간 데이터를 처리하는 **병렬 처리 에코 엔진**을 구축합니다.

## 2. 하드웨어 및 개발 환경 (Environment)
- **Machine**: Surface Pro 11 (5G)
- **Architecture**: Windows on ARM (Snapdragon(R) X Elite/Plus)
- **Runtime**: .NET 10.0 (ARM64 Native)
- **IDE**: Visual Studio Code (VS Code)
- **Language**: C#

## 3. 핵심 구현 명세 (Core Specifications)
- **Multi-Threading (Task.Run)**: 각 클라이언트 연결을 개별 태스크로 분리하여 멀티코어 기반의 병렬 처리 구현.
- **Persistent Connection**: 연결 종료 시그널(`received == 0`)이 오기 전까지 세션을 유지하는 루프 구조 설계.
- **Resource Management (RAII)**: `using` 블록을 활용하여 세션 종료 시 소켓 자원(핸들) 자동 해제 처리.
- **Modern C# Syntax**: Target-typed new(`new(...)`) 및 최신 비동기 구문을 활용한 코드 최적화.

## 4. 두 번째 단계의 성과 (Phase 02 Achievement)
- **동시성(Concurrency) 확보**: 다수의 클라이언트가 독립적으로 데이터를 송수신하는 멀티 에코 환경 확인.
- **비정상 종료 대응**: 클라이언트의 강제 연결 해제 시에도 서버 시스템이 중단되지 않고 자원을 회수하는 안정성 확보.

## 5. 실행 (Run)
```bash
dotnet run --project zec01