# Quick makefile for Linux. Assumes that I mostly want fine control in Debug,
# and for Release builds I just want to do everything.
# TODO: add nuget updates

RUNNER=./packages/xunit.runner.console.2.1.0/tools/xunit.console.exe
RUNNER_FLAGS=
DEBUG_ASSEMBLIES=./tests/bin/Debug/trellabit.tests.dll
RELEASE_ASSEMBLIES=./tests/bin/Release/trellabit.tests.dll

.PHONY: build
build:
	xbuild /t:Build /p:Configuration=Debug ./trellabit.net.sln

.PHONY: clean
clean:
	xbuild /t:Clean /p:Configuration=Debug ./trellabit.net.sln

.PHONY: tests
tests: build
	mono $(RUNNER) $(RUNNER_FLAGS) $(DEBUG_ASSEMBLIES)

.PHONY: release
release:
	xbuild /t:Clean /p:Configuration=Release ./trellabit.net.sln
	xbuild /t:Build /p:Configuration=Release ./trellabit.net.sln
	mono $(RUNNER) $(RUNNER_FLAGS) $(RELEASE_ASSEMBLIES)
