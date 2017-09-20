FROM microsoft/dotnet:2.0.0-runtime-jessie
COPY . /app
WORKDIR /app/bin/Release/PublishOutput

EXPOSE 80
ENV ASPNETCORE_URLS http://*:80

ENTRYPOINT ["dotnet", "MazeAPI.dll"]