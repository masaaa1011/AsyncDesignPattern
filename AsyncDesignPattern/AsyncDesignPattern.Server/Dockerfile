#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AsyncDesignPattern.Server/AsyncDesignPattern.Server.csproj", "AsyncDesignPattern.Server/"]
COPY ["AsyncDesignPattern.TaskFamily/AsyncDesignPattern.TaskFamily.csproj", "AsyncDesignPattern.TaskFamily/"]
COPY ["DesignPatterns/GuardedSuspention/GuardedSuspention.csproj", "DesignPatterns/GuardedSuspention/"]
COPY ["DesignPatterns/TwoPhaseTermination/TwoPhaseTermination.csproj", "DesignPatterns/TwoPhaseTermination/"]
COPY ["DesignPatterns/ProducerConsumer/ProducerConsumer.csproj", "DesignPatterns/ProducerConsumer/"]
COPY ["DesignPatterns/ActiveObject/ActiveObject.csproj", "DesignPatterns/ActiveObject/"]
COPY ["AsyncDesignPattern.Common/AsyncDesignPattern.Common.csproj", "AsyncDesignPattern.Common/"]
COPY ["DesignPatterns/ThreadSpecificStrage/ThreadSpecificStrage.csproj", "DesignPatterns/ThreadSpecificStrage/"]
COPY ["DesignPatterns/SingleThreadedExecution/SingleThreadedExecution.csproj", "DesignPatterns/SingleThreadedExecution/"]
COPY ["DesignPatterns/Immutable/Immutable.csproj", "DesignPatterns/Immutable/"]
COPY ["DesignPatterns/Balking/Balking.csproj", "DesignPatterns/Balking/"]
COPY ["DesignPatterns/ThreadPerMessage/ThreadPerMessage.csproj", "DesignPatterns/ThreadPerMessage/"]
COPY ["DesignPatterns/ReadWriteLock/ReadWriteLock.csproj", "DesignPatterns/ReadWriteLock/"]
COPY ["DesignPatterns/Future/Future.csproj", "DesignPatterns/Future/"]
COPY ["DesignPatterns/WorkerThread/WorkerThread.csproj", "DesignPatterns/WorkerThread/"]
COPY ["AsyncDesignPattern.Repository/AsyncDesignPattern.Repository.csproj", "AsyncDesignPattern.Repository/"]
COPY ["AsyncDesignPattern.SenderReciever/AsyncDesignPattern.SenderReciever.csproj", "AsyncDesignPattern.SenderReciever/"]
RUN dotnet restore "AsyncDesignPattern.Server/AsyncDesignPattern.Server.csproj"
COPY . .
WORKDIR "/src/AsyncDesignPattern.Server"
RUN dotnet build "AsyncDesignPattern.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AsyncDesignPattern.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsyncDesignPattern.Server.dll"]
