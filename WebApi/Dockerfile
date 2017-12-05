FROM microsoft/dotnet:1.1.5-runtime-jessie
WORKDIR /app
COPY ./app .

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "./DockerSample.dll"]