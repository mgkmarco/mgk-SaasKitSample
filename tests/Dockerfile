FROM igcproget.igc.zone/gig-common-docker/dotnet/sdk:3.1.301-0.1.0.9

COPY . .
RUN dotnet restore SaasKitSample.sln
RUN dotnet build SaasKitSample.sln /p:Version=$VERSION -c Release --no-restore

ENTRYPOINT ["/scripts/test.sh"]
CMD ["-u", "-i", "-cp", "-f", "Category=Unit|Category=Integration|Category=Component", "/sln/tests"]