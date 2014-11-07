>Before we begin I want that to be noted that I work on OSX.  So the following examples have not been tested in __linux__.  All parts that are specially about __linux__ are written in the best of my knowledge.  For example how to download node.js on linux.  It would be good if someone using linux would confirm that the following (download and examples) works on linux.  
Also I assume that you are familiar with javascript.  If you are not I requment that you take some tutorials in javascript and get familiar with it before you start here. For example on [w3shcools.com](http://www.w3schools.com/js/). 
Thank you very much.


# Node.js

[Node.js](http://nodejs.org/) is an open source, cross-platform runtime environment for server-side and networking applications. Node.js applications are written in JavaScript, and can be run within the Node.js runtime on OS X, Microsoft Windows, Linux and FreeBSD.  You can read more about node.js on node.js homepage [http://nodejs.org/](http://nodejs.org/) and on [wikipedia](http://en.wikipedia.org/wiki/Node.js).

## How to install Node.js

To install node.js on OSX, open a terminal window and type

	brew install node

*Remember it is a good idea to do __brew update__ before you do brew install to ensure your Homebrew is up to date.

on linux, type in terminal

	sudo apt-get install nodejs

*It is a good idea to do **sudo apt-get update** before you do sudo apt-get install to ensure your apt-get is up to date.*

[npm](https://www.npmjs.org/) is node.js package manager.  The abbrebiate npm stands for Node Packaged Modules but often talked about node package manager.  npm is simular to pip in python.

 [npm](https://www.npmjs.org/) installs automatically on OSX when __brew install node__ is executed.  But on linux it must be install separately by executing the following command in Terminal window.

	sudo apt-get install nodejs npm

## Nodejs shell

Node.js has a command line shell.  Also called nodejs terminal or [REPL](http://nodejs.org/api/repl.html).  To activate it execute the following command in terminal

	node

and the prompt in terminal should change (something like this)

```nodejs
☁  ~  node
>
```

Inside this shell you can write javascript code.  Here below we write a simple function that takes in a number and doubles it.

```javascript
> function double(value){return value * 2;}
```
Then we run our function in the nodejs shell be executing the command

```javascript
> double(2.3)
```

And the terminal window will show

```nodejs
> function double(value){return value * 2;}
undefined
> double(2.3)
4.6
> 
```

Node.js shell is a interactive javascript shell that is mainly used to test statments.  The main power of node.js is when we write a javascript file and run it with node.js.  Lets start with writing a very simple __helloWorld.js__ file and run it with node.js.  Open your favorite text editor and write the following.

```javascript
console.log('Hello World!');
```

Now save this file as **helloWorld.js** on your computer (in any directory you like).

Lets run **helloWorld.js** from a terminal window.  You open the terminal window and go to the directory where the file is saved.  In my case I made a directory on my desktop called `nodejsTest`, so in my terminal window I write `cd ~desktop/nodejsTest/` and execute it.
To run the file with node.js you execute

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

## Node.js is single threadid and asynchronous

Ok, now we have been playing with very easy stuff.  It is time to dive in and try to understand one very interesting part of node.js, that many have had hard time understanding. That is **node.js is single threadid and asynchronous**.

Lets write a little code and try to understand this.  We will use the [`setTimeout()`](http://www.w3schools.com/jsref/met_win_settimeout.asp) function.

```javascript
setTimeout(function(){
	console.log('Nirvana BEST');
}, 3000);

console.log('Hello world!');
```

What will be written to the Terminal window first **`Nirvanda BEST`** or **`Hello world!`**.  What do you think? And why?

Lets save the file as `helloTest.js` in the same directory as before.  And run it be executing `node helloTest.js` in Terminal window (ofcourse in the right directory).  The Terminal window will show

```bash
☁  nodejsTest  node helloTest.js 
Hello world!
Nirvana BEST
```

Did you guess right?  If you did, Good job.  Many programmers that are beginners with node.js think that the above code runs like this

```javascript
1. The program starts to execute the setTimeout function
	* In it we tell it to wait for 3 sec. So the programs stops and wait for 3 sec.  
2. After these 3 sec. it should run the console.log('Nirvana BEST'); line.
3. Then it will run the console.log('hello world!'); line
```

Why does the code not run like that?
The reason is because **node.js** is asynchronous.  What really happends is this.

```javascript
1. The program starts to execute the setTimeout function
	* In it we tell it to wait for 3 sec. 
	* What happens now is that Node.js has a task that will be ran in 3 sec.
	** it plases this task somewere and continues with the code.  That is it runs the console.log('hello world!'); line
	** after 3 sec. Node.js will receive a interrupt and when it recives this interrupt it will run the console.log('Nirvana BEST'); line.
```

This is because node.js is asynchronus.  This is really the pattern in node.js code it will never stop.  It will allways continue to run.  You define tasks and callback but node.js will allways continue to run.  This means that there is really no way to stop in node.js code.  Ofcourse you can do something like we did above with setTimeout() or a callback.  But node.js will not stop even if you do it will keep on going and when it receives the interrupt it will run the task or callback.

The reason for this behavior is because node.js is single threadid.  It is not possible to created threads in node.js.  And node.js is collectons of libraries that are focused on networking.

Don't worry if you are not getting this right away.  Lets take another example.  Lets change the code above like this, using the [setInterval](http://www.w3schools.com/jsref/met_win_setinterval.asp) function.

```javascript
setInterval(function(){
	console.log('Nirvana BEST');
}, 3000);

console.log('Hello world!');
```
What happens know, can you assume it before we run the code?  Guess, and then save and run it and see whats happens. (I am assuming that by now you know how to save and run node.js code, because we have done that afew times here above).  The terminal window should show something like this.

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

So this code will never stop running.  Lets add another Interval to this code, like this.

```javascript
setInterval(function(){
	console.log('Nirvana BEST');
}, 3000);

setInterval(function(){
	console.log('Hello world!');
}, 4000);

```

Note that we are loggin different text and also have different times.  What will happen now? Guess and then run the code.
The terminal window is showing something like this.

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
This program will run forever. Note that Node.js is single threadid, still we are getting seperate tasks running at the same time.  It looks like we have two threads but we just have one.  This is because of the interrupt I talked about here above.



TODO
