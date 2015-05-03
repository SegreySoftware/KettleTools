# Kettle Tools

This project intends to provide a number of tools related to the Kettle ETL tool. Currently, it has a tool for managing Kettle file based repositories. The project is developed in C#, admittedly an odd choice resulting from some practical circumstances.
The app is currently very basic, but it does offer the possibility to rename files while keeping references intact, a functionality which Spoon lacks.

## Features

* View Jobs and Transformations in a repository
* Filter list of Jobs and Transformations
* View dependencies between Jobs and Transformations
* Rename and Move Jobs and Transformations. References are also updated.

## Usage

* Edit the file KettleToolsCmd\Repositories.config. Add any repositories you want to manage.
* Build the solution
* Start KettleToolsCmd\bin\Debug\KettleToolsCmd.exe
* When you click on the name of a repository, you see a list of all Jobs and Transformations in the repository
* The list can be filtered using the links at the top
* When clicking on a Job/Transformation, you see details, including dependencies.
* In the details page, you have the option to change the name or directory of an item. When doing so, all references to the job will be updated.

