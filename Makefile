# Quick makefile for Linux. Assumes that I mostly want fine control in Debug,
# and for Release builds I just want to do everything.

NUGET=./packages/NuGet.CommandLine.3.4.3/tools/NuGet.exe
RUNNER=./packages/xunit.runner.console.2.1.0/tools/xunit.console.exe
RUNNER_FLAGS=
DEBUG_ASSEMBLIES=./tests/bin/Debug/trellabit.tests.dll
RELEASE_ASSEMBLIES=./tests/bin/Release/trellabit.tests.dll
SOLUTION=trellabit.net.sln
XBUILD_FLAGS=/verbosity:minimal

.PHONY: all
all: clean build tests

.PHONY: restore
restore:
	mono $(NUGET) restore $(SOLUTION)

#.PHONY: update
#update:
	#nuget update $(SOLUTION)

.PHONY: build
build:
	xbuild $(XBUILD_FLAGS) /p:Configuration=Debug $(SOLUTION)

.PHONY: clean
clean:
	xbuild $(XBUILD_FLAGS) /t:Clean /p:Configuration=Debug $(SOLUTION)

.PHONY: tests
tests: build
	mono $(RUNNER) $(RUNNER_FLAGS) $(DEBUG_ASSEMBLIES)

.PHONY: release
release:
	xbuild $(XBUILD_FLAGS) /t:Clean /p:Configuration=Release $(SOLUTION)
	xbuild $(XBUILD_FLAGS) /t:Build /p:Configuration=Release $(SOLUTION)
	mono $(RUNNER) $(RUNNER_FLAGS) $(RELEASE_ASSEMBLIES)
