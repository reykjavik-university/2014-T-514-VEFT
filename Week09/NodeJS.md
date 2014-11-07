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
	* in it we tell it to wait for 3 sec. So the programs stops and wait for 3 sec.  
2. After these 3 sec. it should run the console.log('Nirvana BEST'); line.
3. Then it will run the console.log('hello world!'); line
```

 Lets write a write a 






TODO

node shell

more on npm?