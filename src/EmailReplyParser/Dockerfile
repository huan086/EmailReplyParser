FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as build

ARG projname=EPEmailReplyParser
ARG key=TODO
ARG packageversion=1.0.1
ARG nugetserver=http://nuget.dev.logicsoftware.net

WORKDIR /src

COPY EmailReplyParser/$projname.csproj $projname/
COPY EmailReplyParser/EmailReplyParser.nuspec $projname/
#COPY NuGet.config .

RUN dotnet restore $projname/$projname.csproj

COPY EmailReplyParser $projname/

WORKDIR /src/$projname
RUN dotnet pack /p:Version=$packageversion -o packages
RUN dotnet nuget push packages/LogicSoftware.EPEmailReplyParser.$packageversion.nupkg -k $key -s $nugetserver