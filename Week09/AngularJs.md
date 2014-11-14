# AngularJS

## What.

[AngularJs](https://angularjs.org/) (often simply called Angular) is an open-source JavaScript framework written in 
JavaScript and developed by Google. It was initialy released the year 2009.

Angulars main goal is to provide software-developers tools for developement and testing by with a framework to implement the MVC pattern(Model-View-Controller) to seperate presentation, data and logic.

Angular achieves this by the use of custom html tags that are interpreted as directives, these directives tell Angular to bind parts of the html page to models that can be manipulated by JavaScript.

## How.

Since Angular is distributed as a JavaScript file, to use the Angular framework you only need to add a single script tag to your code with a reference to the AngularJs library.

```
<script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.26/angular.min.js"></script>
``` 

### ng-directives
* ng-app: defines a new Angular application.
* ng-bind: replaces the text of the specified HTML DOM with the value of a given expression.
* ng-model: Binds an select, input, textarea element to a property.
* ng-controller: Attaches a controller class to a view.
* ng-repeat: Instantiates a new template for each item in a collection.

```
<div ng-app="">
 
<p>Text to copy: <input type="text" ng-model="myangularmodel" value="type some text"></p>
<p ng-bind="myangularmodel"></p>

</div>
``` 

This code will illustrate how Angular is capable of binding values to elements

## Why.

The Angular frameworks helps developers make well structured front-end web applications that are both easily maintainable and testable.

Angular makes little work to be done by the programer in the MVC department, the programer just splits his app into components and Angular takes care of the rest by connecting them together, this will result in less code which is always nice.
A
ngular inspires RESTful thinking when constructing web APIs which result in loosely coupled code and reusable services.

## Helpful resources

10 Reasons Why You Should Use AngularJS:
http://www.sitepoint.com/10-reasons-use-angularjs/

A Step-by-Step Guide to Your First AngularJS App:
http://www.toptal.com/angular-js/a-step-by-step-guide-to-your-first-angularjs-app