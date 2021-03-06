# trellabit.net
[![Build Status](https://travis-ci.org/DC23/trellabit.net.svg?branch=master)](https://travis-ci.org/DC23/trellabit.net)
___
## Note: This project is dead. I will not be doing any development on it, and it doesn't currently do anything useful.
If you want to integrate Trello and Habitica tasks, please see [scriptabit](https://github.com/DC23/scriptabit).
___
.Net integration between Trello and Habitica. The goal is to provide both
a command-line interface to useful batch operations (such as batch application
of labels), and synchronisation between Trello and Habitica. 

Otherwise known (by DC23) as *my over-engineered hobby project that doesn't 
yet do anything that I couldn't do in a hundred lines of Python*.

The initial focus will be on one-way synchronisation from Trello to Habitica,
essentially allowing Trello cards to drive Habitica To-Dos.  Once that
functionality is usable I will look to other features such as batch editing and
bi-directional synchronisation.  This goal will drive development of the Trello
integration, and expansion of the Habitica interface.

I find the flat issue list in Github too limited for planning, and am using
a private Trello board. If you want to contribute, then please request access to
the Trello board.

## Where is 'the poisoner' that I talked about?
For various reasons, I have moved the poisoner concept into [a separate
project](https://github.com/DC23/scriptabit).
Written in Python, scriptabit is creating a flexible and extensible framework
for scripting functionality against the Habitica API (and conceptually against
any web API).

# Supported Platforms
This project has been tested on the following platforms:

* Visual Studio 2015 Community Edition, on both Windows 10 and Windows 7.
* Linux (Arch, LMDE) with Monodevelop 5 and Mono 4.4.0.
  * There is also a Makefile for use on Linux that will let you work without MonoDevelop. 
It is also required for running the XUnit tests, as MonoDevelop integration won't be available
until version 6.

## A Note about MonoDevelop
By default, [MonoDevelop](http://www.monodevelop.com/) writes console output
into a read-only panel.
This doesn't work with `trellabit.net` as it requires console input.
Therefore, make sure that `Run on external console` is checked in the
`Run-->General` options for `trellabit.net`.

# Managing Assembly Version Strings
I want to keep the assembly versions synchronised, which is a pain to edit
manually. The build and revision numbers are set to autoincrement, but major and
minor version numbers have to be maintained manually. Rather than tediously do
this for each release, I have added a configuration for the Python
[bumpversion](https://github.com/peritus/bumpversion) package. See the
bumpversion documentation for details on its use, but most of the time it boils
down to `bumpversion major` or `bumpversion minor` at a command line from the
root solution directory.

# High-level Architecture

I am using a somewhat traditional tiered architecture, with a data access layer,
business logic layer, and UI layer.

## Services / Data Access Layer
* `Trellabit.Services`: The abstract interfaces and data model classes that define the data objects and the services that manage them.
* `Trellabit.Services.Trello`: The Trello-specific data service implementation.
* `Trellabit.Services.Habitica`: The Habitica-specific data service implementation.

The key roles of the service implementations are:
* map the data model to the specific third-party service
* translate the service interface methods to the specific service API

I was planning to use [HabitRPG API .Net Client](https://github.com/marska/habitrpg-api-dotnet-client)
for the Habitica integration, but development appears to have stalled in 2014 which means no
support for the v3 Habitica API. Instead I plan to develop my own Habitica v3 API using 
[RestEase](https://github.com/canton7/RestEase) to manage the REST API.

I am using the model classes from HabitRPG API .Net Client. All these classes
have a file header indicating their origin and the Git commit ID for this repository where they
appear in their original form. All modifications to the files since that commit,
and all files in the Model directory without the header are the unique work
carried out withing this project. Changes that I make to update the Model classes to the
Habitica v3 API will be merged back to the HabitRPG API .Net Client code, which may assist in
eventually updating it to the v3 API.

## Logic Layer
* `Trellabit.Logic`: The core routines that implement the available operations such as syncing cards from Trello to Habitica.
    * Operates on the trellabit.data interfaces to keep it insulated from backend details such as the 3rd party APIs.

## User Interface Layer
* `Trellabit.Cli`: The command-line interface wrapper. Provides a CLI UI to trellabit.operations.
    * Configures logging
    * Provides scope for me to reuse the habitica assembly in other applications (such as my pomodoro app).
    * Provides scope to create a GUI interface if desired.

## Testing Assembly
`Trellabit.Tests` contains all unit tests for the other modules. The
InternalsVisibleTo assembly attribute is used to give the test code access to
internal types, thus avoiding the problem of relaxed information hiding purely
for the benefit of testing. 

## Utility Assembly
* `Trellabit.Core`: Core/common utility classes shared by many other modules.
This is not really a layer. It is a separate module that sits off to the side
and provides a grab-bag of shared functionality. Not sure what yet but I always
end up with stuff that needs a home.
    
Of course, this may all change as I move forward, but I am hoping I can
implement the operations in a generic way against a set of abstract interfaces,
with the service details hidden away.  Among other reasons, I hope this will
make the logic cleaner, simplify adding 2-way sync later, and may also simplify
adding new services at a later date. Of course, my initial interface design will
focus on my current goal of Trello / Habitica connections. It remains to be seen
how well other services may map to this feature set.

For things like Trellabit.Data.Interfaces.ITask.Difficulty, Habitica implements
this directly, and Trello can implement it as hidden labels applied to cards
(with a default for other cases).  Similar ideas should allow a sufficiently
rich set of attributes on Trello cards for mapping to Habitica in a useful way.
