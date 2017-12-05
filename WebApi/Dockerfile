FROM microsoft/dotnet:1.1.5-runtime-jessie

ENV DOTNET_HOSTING_OPTIMIZATION_CACHE /packagescache

RUN for version in '1.1.2' '1.1.3' '1.1.4' '1.1.5'; do \
        curl -o /tmp/aspnetcore.cache.$version.tar.gz \
            https://dist.asp.net/packagecache/$version/debian.8-x64/aspnetcore.cache.tar.gz \
        && mkdir -p /packagescache && cd /packagescache \
        && tar xf /tmp/aspnetcore.cache.$version.tar.gz \
        && rm /tmp/aspnetcore.cache.$version.tar.gz; \
    done

WORKDIR /app
COPY ./app .

EXPOSE 5000

ENTRYPOINT ["dotnet", "./DockerSample.dll"]