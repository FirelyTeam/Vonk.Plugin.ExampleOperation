|Develop|Master|
|---|---|
|[![Build Status Develop](https://firely.visualstudio.com/vonk%20public%20plugins/_apis/build/status/FirelyTeam.Vonk.Plugin.ExampleOperation?branchName=develop)](https://firely.visualstudio.com/vonk%20public%20plugins/_build/latest?definitionId=33&branchName=develop)| [![Build Status Master](https://firely.visualstudio.com/vonk%20public%20plugins/_apis/build/status/FirelyTeam.Vonk.Plugin.ExampleOperation?branchName=master)](https://firely.visualstudio.com/vonk%20public%20plugins/_build/latest?definitionId=33&branchName=master)|

# Vonk.Plugin.ExampleOperation
A template for creating plugins for the Firely server [(server.fire.ly)](server.fire.ly).

## Getting Started
This project was designed to help you get started with developing a plug-in for Firely Server.<br>
It's a skeleton which you can adopt to create more complex custom operations.<br>
For more details about developing a Firely Server plug-in, please see [Vonk documentation - Vonk FHIR Components](http://docs.simplifier.net/vonk/components/components.html).

### Install
For instructions on how to run the plug-in and the Firely Server, please see the offical [Firely Server documentation](http://docs.simplifier.net/vonk/index.html).

### Build dependencies
The following configuration has been succesfully tested for building and running the project:
* Firely Server (Vonk) - Version 3.1.0
* Visual Studio for Mac - Version 8.x.x
* Visual Studio for Windows - Version 16.x.x
* .Net Core - Version 2.2

## Tests

The $test operation is defined for multiple interactions:

* Type level interaction:<br>
    > POST [base]/[Resource]/$test

* Instance level interaction:<br>
    > GET [base]/[Resource]/[id]/$test
    
 You should get a response back from the FHIR server containing an OperationOutcome.<br>
 It should report that $test was executed sucessfully. [Executing $test using FHIR STU3 and R4 is supported](http://docs.simplifier.net/vonk/features/multiversion.html).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/8eec15ad88bf9c7ba9a6)

A Postman collection for all the requests mentioned above can also be found in the 'data' folder.
    
## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
