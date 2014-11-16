>Before we begin I want it to be noted that I work on OSX. So the following examples have not been tested on __linux__. All parts that are specially about __linux__ are written to the best of my knowledge. For example how to download node.js on linux. It would be good if someone using linux would confirm that the following (download and examples) work on linux. 
Also I assume that you are familiar with javascript. If you are not I recommend that you take some tutorials in javascript and get familiar with it before you start here. For example on [codeacademy.com](http://www.codecademy.com/learn). Thank you very much.

# Node.js

[Node.js](http://nodejs.org/) is an open source, cross-platform runtime environment for server-side and networking applications. Node.js applications are written in JavaScript, and can be run within the Node.js runtime on OS X, Microsoft Windows, Linux and FreeBSD. You can read more about node.js on its homepage [http://nodejs.org/](http://nodejs.org/) and on [wikipedia](http://en.wikipedia.org/wiki/Node.js).

## How to install Node.js

To install Node.js on OSX, open a terminal window and type

	brew install node

*Remember it is a good idea to do __brew update__ before you do brew install to ensure your Homebrew is up to date. If you don't have Homebrew I refer to Week08 Python_evironment_install_OSX.md where Hlynur explains how.

on linux, type in terminal

	sudo apt-get install nodejs

*It is a good idea to do **sudo apt-get update** before you do sudo apt-get install to ensure your apt-get is up to date.

If you are running an Ubuntu linux distro you might have some [conflicts](http://askubuntu.com/questions/235655/node-js-conflicts-sbin-node-vs-usr-bin-node) with a legacy package (Amateur Packet Radio Node Program) this command should get you back on track **sudo apt-get install nodejs-legacy** 

[npm](https://www.npmjs.org/) is node.js package manager. The abbreviate npm stands for Node Packaged Modules but often talked about as node package manager. npm is simular to pip in python.

[npm](https://www.npmjs.org/) installs automatically on OSX when __brew install node__ is executed. But on linux it must be installed separately by executing the following command in Terminal window.

	sudo apt-get install nodejs npm

## Nodejs shell

Node.js has a command line shell. Also called nodejs terminal or [REPL](http://nodejs.org/api/repl.html). To activate it execute the following command in terminal

	node

and the prompt in terminal should change (something like this)

```nodejs
☁  ~  node
>
```

Inside this shell you can write javascript code. Here below we write a simple function that takes in a number and doubles it.

```javascript
> function double(value){ return value * 2; }
```
Then we run our function in the nodejs shell by executing the command

```javascript
> double(2.3)
```

And the terminal window will show

```nodejs
> function double(value){ return value * 2; }
undefined
> double(2.3)
4.6
> 
```

Node.js shell is an interactive javascript shell that is mainly used to test statements. The main power of node.js is when we write a javascript file and run it with node.js. Lets start with writing a very simple __helloWorld.js__ file and run it with node.js. Open your favorite text editor and write the following.

```javascript
console.log('Hello World!');
```

Now save this file as **helloWorld.js** on your computer (in any directory you like).

Lets run **helloWorld.js** from a terminal window. You open the terminal window and go to the directory where the file is saved. In my case I made a directory on my desktop called `nodejsTest`, so in my terminal window I write `cd ~desktop/nodejsTest/` and execute it.
To run the file with node.js you execute:

```bash
node helloWorld.js
```
Your terminal window should show something very simular to this.
```bash
☁  nodejsTest   cd ~
☁  ~  cd ~/Desktop/nodejsTest
☁  nodejsTest  node helloWorld.js
Hello World!
☁  nodejsTest  
```

## Node.js is single threaded and asynchronous

Ok, now we have been playing with very easy stuff. It is time to dive in and try to understand one very interesting part of node.js, that many have had hard time understanding. That is **node.js is single threaded and asynchronous**.

Lets write a little code and try to understand this. We will use the [`setTimeout()`](http://www.w3schools.com/jsref/met_win_settimeout.asp) function.

```javascript
setTimeout(function(){
	console.log('Nirvana BEST');
}, 3000);

console.log('Hello world!');
```

What will be written to the Terminal window first **`Nirvanda BEST`** or **`Hello world!`**? What do you think? And why?

Lets save the file as `helloTest.js` in the same directory as before. And run it by executing `node helloTest.js` in Terminal window (of course in the right directory). The Terminal window will show:

```bash
☁  nodejsTest  node helloTest.js 
Hello world!
Nirvana BEST
```

Did you guess right? If you did, good job. Many programmers that are beginners with node.js think that the above code runs like this:

```javascript
1. The program starts to execute the setTimeout function
	* In it we tell it to wait for 3 sec. So the programs stops and wait for 3 sec.  
2. After these 3 sec. it should run the console.log('Nirvana BEST'); line.
3. Then it will run the console.log('hello world!'); line
```

Why does the code not run like that?
The reason is because **node.js** is asynchronous. What really happens is this:

```javascript
1. The program starts to execute the setTimeout function
	* In it we tell it to wait for 3 sec. 
	* What happens now is that Node.js has a task that will be run after 3 sec.
	** it places this task somewhere and continues with the code.  That is it runs the console.log('hello world!'); line
	** after 3 sec. Node.js will receive a interrupt and when it receives this interrupt it will run the console.log('Nirvana BEST'); line.
```

This is because node.js is asynchronous. This is really the pattern in node.js code, it will never stop. It will allways continue to run. You define tasks and callback but node.js will allways continue to run. This means that there is really no way to stop in node.js code. Of course you can do something like we did above with setTimeout() or a callback. But node.js will not stop even if you do it, it will keep on going and when it receives the interrupt it will run the task or callback.

The reason for this behavior is because node.js is single threaded. It is not possible to created threads in node.js. And node.js is collections of libraries that are focused on networking.

Don't worry if you are not getting this right away. Lets take another example. Lets change the code above like this, using the [setInterval](http://www.w3schools.com/jsref/met_win_setinterval.asp) function.

```javascript
setInterval(function(){
	console.log('Nirvana BEST');
}, 3000);

console.log('Hello world!');
```

What happens now, can you assume it before we run the code? Guess, and then save and run it and see what happens. (I am assuming that by now you know how to save and run node.js code, because we have done that a few times here above). The terminal window should show something like this.

```bash
☁  nodejsTest  node helloTest.js
Hello world!
Nirvana BEST
Nirvana BEST
Nirvana BEST
Nirvana BEST
Nirvana BEST
.
.
.
```

So this code will never stop running. Lets add another Interval to this code, like this.

```javascript
setInterval(function(){
	console.log('Nirvana BEST');
}, 3000);

setInterval(function(){
	console.log('Hello world!');
}, 4000);

```

Note that we are logging different text and also have different times. What will happen now? Guess and then run the code.
The terminal window is showing something like this:

```bash
☁  nodejsTest  node helloTest.js
Nirvana BEST
Hello world!
Nirvana BEST
Hello world!
Nirvana BEST
Hello world!
Nirvana BEST
Nirvana BEST
Hello world!
.
.
.
```

This program will run forever. Note that Node.js is single threaded, still we are getting seperate tasks running at the same time. It looks like we have two threads but we just have one. This is because of the interrupt I talked about here above.

Lets think of this from a web service point a view. We have a web service that is single threaded and NOT asynchronous. And this web service gets a conneciton from a client, for example requesting some data from database. What will happen? If it's single threaded and NOT asynchronous the web service is occupied and is not able to do any other job. If we have more clients that want to connect to our single threaded web service they just have to wait until the one client that has the connection with our web service is done. Node.js can handle this scenario, even though it is single threaded. Because it is asynchronous. If we go through the same scenario with nodejs. The first client connects and asks for data from the database. Nodejs sends a request to the database and while it waits for the response from the database it handles other clients requests. When the database is finished getting the data and sends the response back to nodejs. Nodejs gets an interrupt signal, receives the data and gives it to the first client that connected and was asking for this data.

## Echo server

Now it is time to create a simple socket server. As mentioned above node.js contains a cluster of libraries that are mainly focused on networking. In this example we are going to write a little echo server using the [**net** library](http://nodejs.org/api/net.html) that comes with nodejs. To use it we use the command `require('')`. **require('')** is a simular thing as **import** in **Python**. To require net library we simply write the following code:

```javascript
var net = require('net');
``` 

Next we create the server with the createServer function in the net library, it takes in a callback function and that callback function takes in one argument, the `socket`.

```javascript
var server = net.createServer(function(socket){
	
});
``` 

We can bind this socket server by calling a function named `listen()`. Like this.

```javascript
server.listen(6000)
``` 

Each time the operating system gets a connection on port 6000 this function is called.

We can test this by adding some action to our code. Something that we want to be done when there is a connection on port 6000. For example writing a text to the terminal window. E.g. "*incoming connection*"
The whole code looks like this:

```javascript
var net = require('net');

var server = net.createServer(function(socket){
	console.log('incoming connection');
});

server.listen(6000)
``` 

Lets save this file as `socket.js` and run it with node (write `node socket.js` in terminal).

Now we have the socket server up and running but we need to test it. We can use programs like [telnet](http://en.wikipedia.org/wiki/Telnet) or [Netcat](http://en.wikipedia.org/wiki/Netcat). I am going to use Netcat. Open up a new Terminal window and execute the following command:

```bash
nc localhost 6000
```
In the terminal window that we are running node socket.js we now see:

```bash
☁  nodejsTest  node helloTest.js
incoming connection
```
And if we open up a new Terminal window and in it we execute `nc localhost 6000` we see the following in the Terminal window running node socket.js:

```bash
☁  nodejsTest  node helloTest.js
incoming connection
incoming connection
```

OK now we have a socket server but we still need to be able to receive data and echo it. So lets add to our server code so it looks like this:

```javascript
var net = require('net');

var server = net.createServer(function(socket){
	console.log('incoming connection');

	socket.on('data', function(data){
		console.log('Data from client: ' + data);
	});
});

server.listen(6000)
```

Now lets start our node.js server again in terminal window and start nc localhost 6000 from two separte Terminal windows. Now if we write some text in our terminal windows we can see the server terminal window echoing our data.

So we have written a socket server in node.js. It can handle multible socket connections on a single threaded server, by using callback events to handle them.

But it is still not echoing incoming data, so it is not an echo server. The only thing he does is write it to the Terminal window. So we add to it `socket.write(data);` see whole code:

```javascript
var net = require('net');

var server = net.createServer(function(socket){
	console.log('incoming connection');

	socket.on('data', function(data){
		console.log('Data from client: ' + data);
		socket.write(data);
	});
});

server.listen(6000)
```
So now if we start up the three Terminal windows:
> Terminal window 1 -> node server.js
> Terminal window 2 -> nc localhost 6000
> Terminal window 3 -> nc localhost 6000

If we write some text in terminal window 2 we get a echo response back and same happens if you type in text in terminal window 3 you get a response back.
Lets add to this. Lets echo the incoming message to all connected users. We do this by adding every socket connection to an array and when the server receives a message he will send that message to all connections in his array. In the code below we have implemented this:

```javascript
var net = require('net');

var connectedClients = [];

var server = net.createServer(function(socket){
	console.log('incoming connection');
	connectedClients.push(socket);



	socket.on('data', function(data){
		console.log('Data from client: ' + data);
		connectedClients.map(function(cClient){
			cClient.write(data);
		});
	});
});

server.listen(6000)
```

At this point we have written a small socket server that sends all incoming messages (data) to all connected sockets. Now you should have a good idea about what node.js is, specially the single threaded and asynchronous part. It is very important to get good understanding of this part if you are going to write node.js programs. Node.js is mainly collections of libraries that are aimed to write network applications.

Though we have been doing some coding in node.js none of them are web services. So lets look into web services in node.js.

## Web service in node.js

It is really easy to write a HTTP web service in node.js. Below is a code that shows one example of this. In it we are using the `HTTP` package.

```javascript
var http = require('http');

var server = http.createServer(function(request, response){
	response.writeHead(200, {'content-type': 'text/plain'});
	response.end('Hello World!');
});

server.listen(7000);
```

As you probably notice this is very simular coding as we did earlier when we build the socket server. And the *HTTP* package is based on *NET* package that we were using when we build the socket server. The createServer function in the HTTP package takes in a callback that has two parameters request and response (often called req and res).

Lets go through what happens when there is a request at port 7000. Then the network package takes control and the HTTP package receives the request. Headers are parsed. We can read the request and we can write to the response that is sent back to the client.

Congratulations, you have written a HTTP server. It really does not do much. When he gets a request, he answers with status 200, and the body 'Hello World!'. Of course alot of code is in the **HTTP** library but by using it we have a very simple HTTP server.

Lets save the code and run it in node.js. (just as before `node filename`).

Once the server is up and running you can test it with curl:

	curl http://localhost:7000

or in browser by the url:

	http://localhost:7000

In both cases you should get the response `Hello World!`

This works just as the socket server. We create a server. It takes in a callback and we answer the callback.

Lets try sending multiple requests at the same time. Todo that we use [ab](http://httpd.apache.org/docs/2.2/programs/ab.html).

We still have our HTTP server running. Open up a terminal window and execute the following function

	ab -n 100 -c 100 http://127.0.0.1:7000/

>Note: There are two things I like to point out to you.
>1. You need to use `127.0.0.1` instead of `localhost`, this is some localhost problem.
>2. It is very important to have the `/` at the end. If you don't the `ab` command won't work.

`-n 100` means that we will send 100 requests and `-c 100` means that they will be sent at the same time (parallel). And `http://127.0.0.1:7000` means that all the requests will be sent to localhost port 7000. The terminal window should show you this message.

```bash
Server Software:        
Server Hostname:        127.0.0.1
Server Port:            7000

Document Path:          /
Document Length:        0 bytes

Concurrency Level:      100
Time taken for tests:   0.024 seconds
Complete requests:      100
Failed requests:        0
Write errors:           0
Total transferred:      0 bytes
HTML transferred:       0 bytes
Requests per second:    4146.45 [#/sec] (mean)
Time per request:       24.117 [ms] (mean)
Time per request:       0.241 [ms] (mean, across all concurrent requests)
Transfer rate:          0.00 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.0      0       0
Processing:    14   15   1.0     15      18
Waiting:        0    0   0.0      0       0
Total:         14   15   1.0     15      18

Percentage of the requests served within a certain time (ms)
  50%     15
  66%     16
  75%     16
  80%     16
  90%     17
  95%     17
  98%     17
  99%     18
 100%     18 (longest request)
 ```

This message has some information for us. Like how much time it took to send all the requests `Time taken for tests:   0.024 seconds` and time per request `Time per request: 0.241 [ms]` and much more.

Lets add a delay function to our HTTP server . We can think of this delay as the time it takes our HTTP server to get data from a database. In our example below we have added 2 sec. delay. See code below:

```javascript
var http = require('http');

var server = http.createServer(function(request, response){
	response.writeHead(200, {'conent-type': 'test/plain'});
	response.write('Hallo\n');
	setTimeout(function(){
		response.end('This is fake message from fake database');
	}, 2000);
});

server.listen(7000);
```
If we save and run this code with node and then curl our HTTP server and see the response header by adding -i to our curl request, as this:

```curl
curl -i http://localhost:7000
```
The response we get back is like this:

```
HTTP/1.1 200 OK
conent-type: text/plain
Date: Sat, 08 Nov 2014 11:04:52 GMT
Connection: keep-alive
Transfer-Encoding: chunked

Hallo
```

And then after 2 secs get added:

```
This is fake message from fake database
```

There are two things in the header response that we should take a close look at:

1. The first one is that the connection is keep alive [`Connection: keep-alive`](http://en.wikipedia.org/wiki/HTTP_persistent_connection)
2. Is that the transfer encoding method is chunked [`Transfer-Encoding: chunked`](http://en.wikipedia.org/wiki/Chunked_transfer_encoding)

Remember that node.js is single threaded. By having the connection keep alive and the transfer encoding chunked makes it possible for node.js to handle multible requests.

Now that we have changed our HTTP server (added our fake fetch to database). Lets do multible requests again with ab. See command and response that we got in terminal window:

```terminal
☁  2014-T-514-VEFT [master] ab -n 100 -c 100 http://127.0.0.1:7000/
This is ApacheBench, Version 2.3 <$Revision: 655654 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking 127.0.0.1 (be patient).....done


Server Software:        
Server Hostname:        127.0.0.1
Server Port:            7000

Document Path:          /
Document Length:        45 bytes

Concurrency Level:      100
Time taken for tests:   2.027 seconds
Complete requests:      100
Failed requests:        0
Write errors:           0
Total transferred:      14500 bytes
HTML transferred:       4500 bytes
Requests per second:    49.33 [#/sec] (mean)
Time per request:       2026.990 [ms] (mean)
Time per request:       20.270 [ms] (mean, across all concurrent requests)
Transfer rate:          6.99 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        1    4   2.0      4       8
Processing:  2005 2013   3.2   2014    2018
Waiting:        3    9   3.9     10      15
Total:       2013 2017   1.6   2018    2019

Percentage of the requests served within a certain time (ms)
  50%   2018
  66%   2018
  75%   2018
  80%   2018
  90%   2018
  95%   2018
  98%   2019
  99%   2019
 100%   2019 (longest request)
```

Look at the time it took to do this test `Time taken for tests:   2.027 seconds`. This is only 2 sec. more it took before we added the 2 sec. delay. Note that we are making 100 requests all at the same time.
How much time will it take if we make 200 requests in two chunks. 100 requests at the same time and when finished there there will be another 100 requests sent immediately. Can you assume how much time it will take?

Lets try it.

```terminal
ab -n 200 -c 100 http://127.0.0.1:7000/
```

We get in the Terminal window:

```terminal
☁  2014-T-514-VEFT [master] ab -n 200 -c 100 http://127.0.0.1:7000/
This is ApacheBench, Version 2.3 <$Revision: 655654 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking 127.0.0.1 (be patient)
Completed 100 requests
Completed 200 requests
Finished 200 requests


Server Software:        
Server Hostname:        127.0.0.1
Server Port:            7000

Document Path:          /
Document Length:        45 bytes

Concurrency Level:      100
Time taken for tests:   4.049 seconds
Complete requests:      200
Failed requests:        0
Write errors:           0
Total transferred:      29000 bytes
HTML transferred:       9000 bytes
Requests per second:    49.40 [#/sec] (mean)
Time per request:       2024.334 [ms] (mean)
Time per request:       20.243 [ms] (mean, across all concurrent requests)
Transfer rate:          6.99 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    3   2.3      2       9
Processing:  2003 2014   5.3   2014    2023
Waiting:        1   10   3.2     11      15
Total:       2004 2017   5.2   2016    2024

Percentage of the requests served within a certain time (ms)
  50%   2016
  66%   2020
  75%   2020
  80%   2024
  90%   2024
  95%   2024
  98%   2024
  99%   2024
 100%   2024 (longest request)
```
See the total time for these 200 requests is 4 sec. `Time taken for tests:   4.049 seconds`

Node.js is handling this concurrent requests easily although it is single threaded but by now you know why.
