#Unit Testing

One of the main reason why we've designed our code the way we've done, is to be able to unit test all our business logic. The business logic is certainly not the only code that needs to be tested, but it can be argued that it is the most important code to write unit tests for.

Other parts of the code could be tested using integration tests, and having both is probably a good idea.

For testing javascript you will need mochajs

## How to install Mocha
You can get it on by opening the terminal and typing in
```
$npm install -g mocha
```

You should see something like this:

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

The next step is to create a small program to test. You can place the program wherever you want just make sure to note down the location for later.
Open up your favorite editor and copy these line in and save as testDaysTillChristmas.js

```javascript
			var days = function(){
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
				console.log(diff);
				return diff;
			};

			module.exports = {'daysTillChristmas' : days()}
```


Now save the code and get the location of code by using the command $pwd.

Now navigate to the location where you would like your test to be.
mkdir test
now you need to create the test with your favorite editor, I prefer vim so ill use it in this example.

```
$vim test/test.js
```

```javascript
			var assert = require("assert")
			var myCode = require('../locationOfCodeFromAbove/testDaysTillChristmas.js')

			describe('#testDays', function(){
				describe('daysTillChristmas', function(){
					it('Should return 40 on the day 14th of november 2014', function(){
						assert.equal(myCode.daysTillChristmas,40);
					});
				});
			});
```

As you can see by the code we are expecting 40 days you might need to fix the code a little bit. You can get the current number of days by running the days function by node testDaysTillChristmas.js like so
```
$node testDaysTillChristmas.js
```
