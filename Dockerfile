FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o release
WORKDIR /app/release
ENTRYPOINT ["dotnet", "DockerEssentials.dll"]