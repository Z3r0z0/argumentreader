#!/usr/bin/env bash


dotnet nuget pack ArgumentsLib/bin/debug/CommandLineArgumentsLib.1.0.0.nupkg -Version $TEST_VERSION -Verbosity detailed && \
dotnet nuget push ArgumentsLib/bin/debug/CommandLineArgumentsLib.1.0.0.nupkg $NUGET_ORG_KEY -Verbosity detailed -Source nuget.org
