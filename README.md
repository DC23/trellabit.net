# trellabit.net
Experimental .Net integration between Trello and Habitica. The goal is to provide both a command-line interface to useful batch operations (such as batch application of labels), and synchronisation between Trello and Habitica.

# Key Dependencies

* [HabitRPG API .Net Client](https://github.com/marska/habitrpg-api-dotnet-client)
* [Manatee.Trello](https://bitbucket.org/gregsdennis/manatee.trello)
* [MadMilkman.Ini](https://github.com/MarioZ/MadMilkman.Ini)
* [NLog](http://nlog-project.org/)

# Development
This project has been tested on the following platforms:

* Visual Studio 2015 Community Edition, on both Windows 10 and Windows 7.
* Linux (Arch, LMDE) with Monodevelop 5.10 and Mono 4.4.0.

## A Note about Monodevelop
By default, Monodevelop writes console output into a read-only panel.
This doesn't work with `trellabit.net` as it requires console input.
Therefore, make sure that `Run on external console` is checked in the `Run-->General` options for `trellabit.net`.