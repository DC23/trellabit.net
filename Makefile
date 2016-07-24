# Quick makefile for Linux. Assumes that I mostly want fine control in Debug,
# and for Release builds I just want to do everything.

RUNNER=./packages/xunit.runner.console.2.1.0/tools/xunit.console.exe
RUNNER_FLAGS=
DEBUG_ASSEMBLIES=./tests/bin/Debug/trellabit.tests.dll
RELEASE_ASSEMBLIES=./tests/bin/Release/trellabit.tests.dll
SOLUTION=trellabit.net.sln

.PHONY: restore
restore:
	nuget restore $(SOLUTION)

.PHONY: update
update:
	nuget update $(SOLUTION)

.PHONY: build
build:
	xbuild /p:Configuration=Debug $(SOLUTION)

.PHONY: clean
clean:
	xbuild /t:Clean /p:Configuration=Debug .$(SOLUTION)

.PHONY: tests
tests: build
	mono $(RUNNER) $(RUNNER_FLAGS) $(DEBUG_ASSEMBLIES)

.PHONY: release
release:
	xbuild /t:Clean /p:Configuration=Release $(SOLUTION)
	xbuild /t:Build /p:Configuration=Release $(SOLUTION)
	mono $(RUNNER) $(RUNNER_FLAGS) $(RELEASE_ASSEMBLIES)
