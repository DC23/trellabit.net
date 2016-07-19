# trellabit.net
.Net integration between Trello and Habitica. The goal is to provide both a command-line interface to useful batch operations (such as batch application of labels), and synchronisation between Trello and Habitica. The initial focus is on one-way synchronisation from Trello to Habitica, essentially allowing Trello cards to drive Habitica To-Dos. Once that functionality is usable I will look to other features such as batch editing and bi-directional synchronisation.

Initially I am putting all projects into this single repository for rapid development, but I expect to refactor them out into separate repositories once development reaches the point where I want to use the functionality in other applications.

I find the flat issue list in Github too limited for planning, and am using
a private Trello board. If you want to contribute, then please request access to
the Trello board.

# Supported Platforms
This project has been tested on the following platforms:

* Visual Studio 2015 Community Edition, on both Windows 10 and Windows 7.
* Linux (Arch, LMDE) with Monodevelop 5.10 and Mono 4.4.0.

## A Note about Monodevelop
By default, Monodevelop writes console output into a read-only panel.
This doesn't work with `trellabit.net` as it requires console input.
Therefore, make sure that `Run on external console` is checked in the `Run-->General` options for `trellabit.net`.

# High-level Architecture

I am using a somewhat traditional tiered architecture, with a data access layer, business logic layer, and UI layer.

## Data Access Layer
* `trellabit.data`: The abstract interfaces and data model classes that define the data objects and the services that manage them.
* `trellabit.data.trello`: The Trello-specific data service implementation.
* `trellabit.data.habitica`: The Habitica-specific data service implementation.

The key roles of the service implementations are:
* map the data model to the specific third-party service
* translate the service interface methods to the specific service API

I was planning to use [HabitRPG API .Net Client](https://github.com/marska/habitrpg-api-dotnet-client) for the Habitica integration, but development appears to have stalled in 2014 which means no support for the v3 Habitica API. Instead I plan to develop my own Habitica v3 API using Refit to manage the REST API.

## Logic Layer
* `trellabit.logic`: The core routines that implement the available operations such as syncing cards from Trello to Habitica.
    * Operates on the trellabit.model interfaces to keep it insulated from backend details such as the 3rd party APIs.

## User Interface Layer
* `trellabit.cli`: The command-line interface wrapper. Provides a CLI UI to trellabit.operations.
    * Configures logging
    * Provides scope for me to reuse the habitica assembly in other applications (such as my pomodoro app).
    * Provides scope to create a GUI interface if desired.

## Utility Assembly
* `trellabit.core`: Core/common utility classes shared by many other modules.
This is not really a layer. It is a separate module that sits off to the side and provides a grab-bag of shared functionality. Not sure what yet but I always end up with stuff that needs a home.
    
Of course, this may all change as I move forward, but I am hoping I can implement the operations in a generic way against a set of abstract interfaces, with the service details hidden away.
Among other reasons, I hope this will make the logic cleaner, simplify adding 2-way sync later, and may also simplify adding new services at a later date. Of course, my initial interface 
design will focus on my current goal of Trello / Habitica connections. It remains to be seen how well other services may map to this feature set.

For things like trellabit.model.interfaces.ITask.Difficulty, Habitica implements this directly, and Trello can implement it as hidden labels applied to cards (with a default for other cases).
Similar ideas should allow a sufficiently rich set of attributes on Trello cards for mapping to Habitica in a useful way.