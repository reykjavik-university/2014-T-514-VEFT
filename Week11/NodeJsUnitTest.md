#Unit Testing

Just like in week 3 we need to be able to unit test our business logic for Node js. The business logic is certainly not the only code that needs to be tested, but it can be argued that it is the most important code to write unit tests for.

Other parts of the code could be tested using integration tests, and having both is probably a good idea.

If we want to unit test Node Js we will need Mocha.

## How to install Mocha
You can get it by opening the terminal and typing in
```
$npm install -g mocha
```

You should see something like this if it succeeded:

	/usr/local/bin/mocha -> /usr/local/lib/node_modules/mocha/bin/mocha
	/usr/local/bin/_mocha -> /usr/local/lib/node_modules/mocha/bin/_mocha
	mocha@2.0.1 /usr/local/lib/node_modules/mocha
	├── escape-string-regexp@1.0.2
	├── diff@1.0.8
	├── growl@1.8.1
	├── commander@2.3.0
	├── debug@2.0.0 (ms@0.6.2)
	├── jade@0.26.3 (commander@0.6.1, mkdirp@0.3.0)
	├── mkdirp@0.5.0 (minimist@0.0.8)
	└── glob@3.2.3 (inherits@2.0.1, graceful-fs@2.0.3, minimatch@0.2.14)


## The node JS function under test.
The next step is to create a small program to test. You can place the program wherever you want just make sure to note down the location for later.
Open up your favorite editor and copy these line in and save as christmas.js

```javascript
			function daysTC(){
				var oneMinute = 60 * 1000;
				var oneHour = oneMinute * 60;
				var oneDay = oneHour * 24;
				var today = new Date();
				var nextXmas = new Date();
				nextXmas.setMonth(11);
				nextXmas.setDate(24);
				if (today.getMonth() === 11 && today.getDate() > 24) {
					nextXmas.setFullYear(nextXmas.getFullYear() + 1);
				}
				var diff = nextXmas.getTime() - today.getTime();
				diff = Math.floor(diff/oneDay);
				console.log("Days til XMAS:" + diff);
				return diff;
			};

			module.exports = {'daysTillChristmas' : daysTC()};
```
The module.exports is so you can get access to the function in your test.

Save the file and get the location of code by using the command 
```
$pwd
```

##Our first Mocha test

Navigate to the location where you would like your test to be.
```
$mkdir test
```
You need to create the test with your favorite editor, I prefer vim so ill use it in this example.

```
$vim test/test.js
```

```javascript
			var assert = require("assert")
			var myCode = require('/PwdLocationOfFile/christmas.js')
			
			describe('#test', function(){
				describe('daysTillChristmasTest', function(){ 
					it('Should return 40 on the day 14th of november 2014', function(){
						assert.equal(myCode.daysTillChristmas,40);
					});
				});
				describe('checkIfNegative', function(){
					it('Should return true if the number is not negative', function(){
						assert(myCode.daysTillChristmas>=0);
					});
				});
			});
```
__REMEMBER__ to change the name of the myCode to the location of the code we wrote earlier.

By looking at this code it might not be clear what we are doing at first.
We need assert so we require that is included and also our code so we have access to our function.

[Assert](http://nodejs.org/api/assert.html) has several methods to help checking that the method we were
testing had the intended consequences. I recommend checking the documentation for more assert commands.

The first describe lets us name the test suite, within this you can describe another couple of tests.
The second describe is the name of the specific test case. You could add a couple of test such as check if positive or something alike.

We then assert that the function returns the same number of days as we expect. If that fails the test fails.


As you can see by the code we are expecting 40 days, you most likely need to fix the code a little bit. You can get the current number of days by running the days function like so
```
$node testDaysTillChristmas.js
```
You could also run the test and see what value we get from the assert.

## Running Mocha

Now the next step is to test the code.
We can test the code by using the following command in the folder where the test is located
```
$mocha
```

Or you can specify the location if you are not in the folder by using

```
$mocha locationOfTest/someFolder/
```

You should then get an ouput that says something like
```
Days til XMAS:40


  #test
    daysTillChristmasTest
      ✓ Should return 40 on the day 14th of november 2014
    checkIfNegative
      ✓ Should return true if the number is not negative


  2 passing
```

Congratulations you just passed your first test! This is not recommended for TDD but good job!


