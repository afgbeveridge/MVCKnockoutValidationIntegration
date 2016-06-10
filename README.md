# MVC Knockout Validation integration
This project provides an integration between MVC data annotation style validation and Knockout validation style directives. See my blog for some details about what this project lets you do at [http://tb-it.blogspot.co.nz/2016/06/knockout-validation-and-aspnet-mvc-view.html] (http://tb-it.blogspot.co.nz/2016/06/knockout-validation-and-aspnet-mvc-view.html).

# License
This project is released under the [MIT license](https://opensource.org/licenses/MIT).

# Overview
This from the blog (which is a better summary, this document the better reference):

> The problem to solve is to somehow have the validation attributes that have been applied to the C# view models applied in 
> the same manner to view models created in a (Knockout JS based) client.

Two distinct parts form this implementation:

1. ASP.NET Web Api REST endpoint
2. Javascript client use

## Web Api endpoint
The Javascript client can communicate with a hosted web api endpoint, or existing web api or controller methods can be enhanced to insert validation metadata into a response to a JS client call.

In the solution, see the DynamicValidationController.cs file for the simplest validation returning endpoint implementation; for an example of how to directly insert validation metadata into a view model response, see ViewModelServingController.cs, method WrappedSimpleViewModel.

Inserting the metadata into a response is more invasive, but obviates the need for another network call to gather metadata.

## Client use
Clients will operate in one of two scenarios typically; one where the metadata must be gathered and applied out of band, one where it is served with an API request.

An example of an out of band call is:
```javascript
           $.getJSON("/api/ViewModelServing/SimpleViewModel", null, function (response) {
                var obj = ko.mapping.fromJS(response);
                vmi.decorateAsync({
                    typeName: 'MVCKnockoutValidationIntegration.Models.SimpleViewModel',
                    model: obj,
                    enableLogging: true,
                    insertedValidatedObservableName: 'validation',
                    done: function (validationTree, parsedMetadata) {
                        ko.validation.init({ insertMessages: false });
                        ko.applyBindings(obj, $('#koContainer')[0]);
                    }
                });
                
            });
```
Briefly, we:
* Issue an AJAX call to some endpoint that returns a JSON form of a view model
* Map that response into a JS object
* Asynchronously decorate the model just created using the supplied module, noting:
  * the server side type name of interest
  * the JS model we created to decorate with validation
  * whether to log actions during decoration
  * the property name to create on the JS model that holds a validated observable for the entire model
  * a function that is called when decoration is finished. Here, we just do some binding (is only an example)

An example of a 'munged' call is:
```javascript
            $.getJSON("/api/ViewModelServing/WrappedSimpleViewModel", null, function (response) {
                var obj = ko.mapping.fromJS(response.Model);
                vmi.decorate({
                    model: obj,
                    parsedMetadata: response.ValidationMetadata,
                    enableLogging: true,
                    insertedValidatedObservableName: 'validation' 
                });
                ko.validation.init({ insertMessages: false });
                ko.applyBindings(obj, $('#koContainer')[0]);
            });
  ```
Slightly more simply, we:
* Issue an AJAX call to some endpoint that returns a JSON form of a view model including metadata
* Map that responses _Model_ property into a JS object
* Decorate the model just created using the supplied module, noting:
  * the JS model we created to decorate with validation
  * the metadata we received from the 'munged' call
  * whether to log actions during decoration
  * the property name to create on the JS model that holds a validated observable for the entire model
* Bind the model when the decorate call has finished

## Decorate function options
The call to vmi.decorate or vm.decorateAsync requires an object be passed as the single argument, with possible properties as shown below. The _Role_ column has values of A (asynchronous decoration only), S (synchronous decoration only) and B (both asynchronous and synchronous decoration). The _Required_ column is Y(es) or N(o) denoting applicability.

| Property      | Semantics     |Role|Required|
| ------------- |-------------|---|---|
| model      | a javascript object |B|Y|
| enableLogging     | log actions or not      |B|N|
| ignoreList | array of properties to ignore in metadata      |B|N|
| insertedValidatedObservableName | property in the model to hold a validated observable for the model      |B|N|
| parsedMetadata | metadat object supplied if using a munged version     |S|Y|
| done | a function invoked post decoration      |A|Y|
| endpoint | endpoint that serves metadata      |A|N|



## Examples
If you open and build the solution, press F5, you should be taken to:
* http://localhost:65212/Home/SimpleViewModel

Other examples:
* http://localhost:65212/Home/NestedViewModel
* http://localhost:65212/Home/WrappedViewModel
