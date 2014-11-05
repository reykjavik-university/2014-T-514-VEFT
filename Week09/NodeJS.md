Node.js
==================
>[Node.js®] [1] is a platform built on [Chrome's JavaScript runtime] [2] for easily building fast, scalable network applications. Node.js uses an event-driven, non-blocking I/O model that makes it lightweight and efficient, perfect for data-intensive real-time applications that run across distributed devices.

A detailed tutorial for beginners can be found [here] [3]

Node.js is a fast, open source, cross-platform runtime environment for server-side and networking applications which allows JavaScript to run as a backend, using [Google's V8 VM][2].

##Download and install
Detailed instructions on how to install node.js can be found at their [github page](https://github.com/joyent/node/wiki/Installation)

Via package manager
+ [Windows](https://github.com/joyent/node/wiki/Installing-Node.js-via-package-manager#windows)
+ [OSX](https://github.com/joyent/node/wiki/Installing-Node.js-via-package-manager#osx)
+ [Linux & other](https://github.com/joyent/node/wiki/Installing-Node.js-via-package-manager#debian-and-ubuntu-based-linux-distributions)

For `.msi`, `.pkg` , `tar.gz` and etc. [Nodejs.com](http://nodejs.org/download/)

Building
+ [Windows](https://github.com/joyent/node/wiki/Installation#building-on-windows)
+ [OSX](https://github.com/joyent/node/wiki/Installation#building-on-mac)
+ [Linux](https://github.com/joyent/node/wiki/Installation#building-on-linux)

##Hello, World!
    $ node
    > console.log(Hello, World!);
    Hello, World!
   So if everything works out, create your first server code, you could call it `server.js`
   
    var http = require('http');
    http.createServer(function (req, res) {
	    res.writeHead(200, {'Content-Type': 'text/plain'});
	    res.end('Hello World\n');
	}).listen(1337, "127.0.0.1");
	console.log('Server running at http://127.0.0.1:1337/');
Now start up the server in the command line

    $ node server.js
    Server running at http://127.0.0.1:1337/
    
then visit http://127.0.0.1:1337 in your browser and you should see your message.
   


##Node package manager  (npm)
Node.js comes with a great, built-in package manager, [NPM](https://www.npmjs.org/).

You can search for a package with:

`$ npm search your-package`

and to install packages:
`$ npm install express`

Some packages worth mentioning:
+ [Angular.js](https://angularjs.org/)
    + Structural framework for dynamic web apps.
+ [Express.js](http://expressjs.com/)
    + Sinatra inspired web development framework.
+ [Underscore.js](http://underscorejs.org/)
    + Provides utility functions for common programming tasks.
+ [Grunt](http://gruntjs.com/)
    + JavaScript task runner
+ [Socket.io](http://socket.io/)
   + Enables realtime, bi-directional communication between web clients and server.
+ [Bower](http://bower.io)
    +  Front-end package management
+ [JSHint](https://github.com/jshint/jshint)
    +  Helps to detect errors and potential problems
+ [MongoDB](http://docs.mongodb.org/ecosystem/drivers/node-js/)
    + Open-source document database and the leading NoSQL database.

##Getting started
[Node->Express->MongoDB tutorial](http://cwbuecheler.com/web/tutorials/2013/node-express-mongo/)



[1]: http://nodejs.org/       "Node.js®"
[2]: https://code.google.com/p/v8/ "Chrome's JavaScript runtime"
[3]: http://www.nodebeginner.org/ "NodeBeginner"
[4]: https://github.com/joyent/node "Node GitHub"
