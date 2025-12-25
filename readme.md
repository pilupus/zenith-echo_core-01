# ZENITH-ECHO_CORE-01

**.NET 10 기반의 고성능 비동기 에코 서버 엔진 - [단계 01: 시스템 초기화 및 기본 통신]**

## 1. 프로젝트 개요 (Overview)
본 프로젝트는 하드웨어와 운영체제 커널에 밀착된 **C# 기반 고성능 네트워크 엔진**을 구축하기 위한 첫 번째 단계입니다. 코드 레벨에서 시스템 자원을 정교하게 제어하는 것을 목표로 합니다.

## 2. 하드웨어 및 개발 환경 (Environment)
- **Machine**: Surface Pro 11 (5G)
- **Architecture**: Windows on ARM (Snapdragon(R) X Elite/Plus)
- **Runtime**: .NET 10.0 (ARM64 Native)
- **IDE**: Visual Studio Code (VS Code)
- **Language**: C#

## 3. 핵심 구현 명세 (Core Specifications)
- **Asynchronous Acceptance**: `AcceptAsync()`를 통한 IOCP(I/O Completion Port) 모델 활용으로 불필요한 스레드 점유 방지.
- **Direct Memory Access**: `byte[]` 버퍼를 직접 할당하여 데이터의 실체(Byte)를 직접 제어.
- **Resource Lifecycle**: `using` 구문을 활용하여 소켓 핸들의 즉각적인 해제 및 안정성 확보.

## 4. 첫 번째 단계의 성과 (Phase 01 Achievement)
- **Zero-State 탈출**: .NET 10 환경 구축 및 ARM64 네이티브 바이너리 실행 확인.
- **통신 기초 확립**: TCP/IP 9000번 포트 리스닝 및 단일 클라이언트 에코(Echo) 성공.

## 5. 실행 (Run)
```bash
dotnet run --project zec01