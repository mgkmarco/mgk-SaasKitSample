FROM igcproget.igc.zone/gig-common-docker/dotnet/aspnet:3.1 AS base
ENV InfoManagement__Checksum__Root=/checksum/
ENV ASPNETCORE_URLS=http://++:8080
EXPOSE 8080

FROM igcproget.igc.zone/gig-common-docker/dotnet/sdk:3.1 AS build
COPY ["src/SampleBarApi/SampleBarApi.csproj", "src/SampleBarApi/"]
RUN dotnet restore "src/SampleBarApi/SampleBarApi.csproj"
COPY . .
ARG VERSION=1.0.0
ARG INFORMATIONAL_VERSION=$VERSION
RUN dotnet build /p:Version=$VERSION /p:InformationalVersion=$INFORMATIONAL_VERSION -c Release 
WORKDIR "/sln/artifacts/nugets"
RUN ["/scripts/checksum.sh", "/app/checksum/", "checksum.artifacts.list.txt"]

FROM build AS publish
WORKDIR "/sln/src/SampleBarApi"
RUN dotnet publish "SampleBarApi.csproj" -c Release -o /app/publish --no-build
WORKDIR "/app/publish"
RUN ["/scripts/checksum.sh", "/app/checksum/", "checksum.app.list.txt", "checksum.app.txt"]

FROM publish AS push
ENTRYPOINT ["/scripts/push.sh"]

FROM base AS final
COPY --from=publish /app/publish .
COPY --from=publish /app/checksum /checksum
ENTRYPOINT ["dotnet", "SampleBarApi.dll"]