FROM microsoft/aspnetcore:2.0
COPY . /app
WORKDIR /app/bin/Release/PublishOutput

EXPOSE 80
ENV ASPNETCORE_URLS http://*:80

ENTRYPOINT ["dotnet", "MazeSolverAPI.dll"]