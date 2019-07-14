# Vonk.Plugin.ExampleOperation
A template for creating plugins for the Vonk FHIR server [(vonk.fire.ly)](vonk.fire.ly).

## Getting Started
This project was designed to help you get started with developing a plug-in for Vonk.<br>
It's a skeleton which you can adopt to create more complex custom operations.<br>
For more details about developing a Vonk plug-in, please see [Vonk documentation - Vonk FHIR Components](http://docs.simplifier.net/vonk/components/components.html).

### Install
For instructon on how to run the plug-in and the Vonk server, please see the offical [Vonk documentation](http://docs.simplifier.net/vonk/index.html).

### Build dependencies
The following configuration has been succesfully tested for building and running the project:
* Vonk FHIR server - Version 2.0.1
* Visual Studio for Mac - Version 8.x.x
* Visual Studio for Windows - Version 16.x.x
* .Net Core - Version 2.2

## Tests

The $document operation is defined in FHIR for multiple interactions:

* Type level interaction:<br>
    > POST [base]/[Resource]/$test

* Instance level interaction:<br>
    > GET [base]/[Resource]/[id]/$test
    
 You should get a response back from the FHIR server containing an OperationOutcome.<br>
 It should report that $test was executed sucessfully.
    
## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
